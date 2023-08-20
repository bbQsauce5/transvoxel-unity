public static class MaterialJobFactory
{
    public static MaterialJob Create(WorldSettings worldSettings, WorldState worldState, GeneratorSettings generatorSettings, in ChunkUpdate update) {
        var job = new MaterialJob();
        job.Generator = generatorSettings.GetMaterialGenerator();
        job.MeshData = worldState.ChunkData.ChunkMap[update.ChunkPosition].MeshData;
        job.ChunkMin = CoordinateUtilities.GetChunkMin(update.ChunkPosition, worldSettings.ChunkSize, update.LOD);

        return job;
    }
}
