using Unity.Mathematics;

public static class CollisionJobFactory
{
    public static CollisionJob Create(WorldState worldState,int3 chunkPos) {
        var chunk = worldState.ChunkData.ChunkMap[chunkPos];
        var chunkMeshes = chunk.Meshes;
        var neighbourMask = chunk.NeighboursMask;

        var job = new CollisionJob();
        job.bBakeMainMesh = chunkMeshes[0].GetIndexCount(0) > 0;
        job.bBakeLeftTransitionMesh = chunkMeshes[1].GetIndexCount(0) > 0 && (neighbourMask & 1) > 0;
        job.bBakeDownTransitionMesh = chunkMeshes[2].GetIndexCount(0) > 0 && (neighbourMask & 2) > 0;
        job.bBakeBackTransitionMesh = chunkMeshes[3].GetIndexCount(0) > 0 && (neighbourMask & 4) > 0;
        job.bBakeRightTransitionMesh = chunkMeshes[4].GetIndexCount(0) > 0 && (neighbourMask & 8) > 0;
        job.bBakeUpTransitionMesh = chunkMeshes[5].GetIndexCount(0) > 0 && (neighbourMask & 16) > 0;
        job.bBakeForwardTransitionMesh = chunkMeshes[6].GetIndexCount(0) > 0 && (neighbourMask & 32) > 0;

        job.MainMeshId = chunkMeshes[0].GetInstanceID();
        job.LeftTransitionMeshId = chunkMeshes[1].GetInstanceID();
        job.DownTransitionMeshId = chunkMeshes[2].GetInstanceID();
        job.BackTransitionMeshId = chunkMeshes[3].GetInstanceID();
        job.RightTransitionMeshId = chunkMeshes[4].GetInstanceID();
        job.UpTransitionMeshId = chunkMeshes[5].GetInstanceID();
        job.ForwardTransitionMeshId = chunkMeshes[6].GetInstanceID();

        return job;
    }

}
