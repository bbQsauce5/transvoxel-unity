using Unity.Mathematics;

public static class UpdatesJobFactory
{
    public static UpdatesJob Create(WorldState worldState) {
        var chunkUpdatesJob = new UpdatesJob();
        chunkUpdatesJob.Octree = worldState.ChunkData.ChunkTree;
        chunkUpdatesJob.ChunkUpdates = worldState.ChunkData.ChunkUpdates;
        chunkUpdatesJob.ChunkUpdatesMap = new NativeParallelHashMapAllocator<int3, int>().Allocate();
        chunkUpdatesJob.ActiveNodes = new NativeParallelHashSetAllocator<ulong>().Allocate();
        chunkUpdatesJob.ActiveNodesNeighbours = new NativeParallelHashMapAllocator<int3, int>().Allocate();

        return chunkUpdatesJob;
    }
}
