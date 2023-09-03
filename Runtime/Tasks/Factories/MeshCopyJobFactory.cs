using Unity.Assertions;
using UnityEngine;

public static class MeshCopyJobFactory
{
    public static MeshCopyJob Create(WorldState worldState, in ChunkUpdate update) {
        var chunk = worldState.ChunkData.ChunkMap[update.ChunkPosition];

        Assert.IsTrue(chunk.MeshData.GetMainMeshData().VertexData.IsCreated);

        var meshArray = Mesh.AllocateWritableMeshData(7);

        var job = new MeshCopyJob();
        job.MeshArray = meshArray;
        job.MainMeshData = meshArray[0];
        job.LeftTransitionMeshData = meshArray[1];
        job.DownTransitionMeshData = meshArray[2];
        job.BackTransitionMeshData = meshArray[3];
        job.RightTransitionMeshData = meshArray[4];
        job.UpTransitionMeshData = meshArray[5];
        job.ForwardTransitionMeshData = meshArray[6];
        job.MeshNativeData = chunk.MeshData;
        job.NeighboursMask = update.NeighboursMask;

        return job;
    }
}
