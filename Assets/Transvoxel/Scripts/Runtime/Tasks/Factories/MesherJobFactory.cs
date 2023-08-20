public static class MesherJobFactory
{
    public static MesherJob Create(
        WorldSettings worldSettings, 
        WorldState worldState, 
        GeneratorSettings generatorSettings, 
        in ChunkUpdate update)
    {
        var mesher = new TransvoxelMesher();
        mesher.Generator = generatorSettings.GetDensityGenerator();
        mesher.ChunkMin = CoordinateUtilities.GetChunkMin(update.ChunkPosition, worldSettings.ChunkSize, update.LOD);
        mesher.ChunkSize = worldSettings.ChunkSize;
        mesher.DensityData = worldState.DensityData.GetDataUnchecked(update.ChunkPosition);
        mesher.MeshData = worldState.Pools.MeshDataContainerPool.Get();
        mesher.LOD = update.LOD;
        mesher.NeighboursMask = update.NeighboursMask;
        mesher.TablesRef = GlobalBlobData<TransvoxelTablesBlobAsset>.GetInstance();

        var mesherJob = new MesherJob();
        mesherJob.Mesher = mesher;

        return mesherJob;
    }
}
