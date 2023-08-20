public static class FilterJobFactory
{
    public static FilterJob Create(WorldState worldState) {
        var filterChunkUpdatesJob = new FilterJob();
        filterChunkUpdatesJob.ChunkUpdates = worldState.ChunkData.ChunkUpdates;
        filterChunkUpdatesJob.ChunkUniformState = worldState.ChunkData.ChunkUniformState;
        filterChunkUpdatesJob.FilteredChunkUpdates = worldState.ChunkData.FilteredChunkUpdates;

        return filterChunkUpdatesJob;
    }
}
