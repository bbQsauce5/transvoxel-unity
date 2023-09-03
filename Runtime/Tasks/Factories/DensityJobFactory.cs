using Unity.Mathematics;

public static class DensityJobFactory
{
    public static DensityJob Create(
        WorldSettings worldSettings,
        WorldState worldState,
        GeneratorSettings generatorSettings, 
        in ChunkUpdate update)
    {
        int LOD = update.LOD;
        int LODScale = 1 << LOD;
        int chunkExtents = (worldSettings.ChunkSize >> 1) << LOD;
        int3 chunkMin = update.ChunkPosition - chunkExtents;
        int3 startSamplePos = chunkMin - new int3(1, 1, 1) * LODScale;

        int densitySize = worldSettings.ChunkSize + 3;
        var densityData = worldState.Pools.DensityDataPool.Get();

        var densityJob = new DensityJob();
        densityJob.DensityData = densityData;
        densityJob.Size = densitySize;
        densityJob.Step = LODScale;
        densityJob.StartX = startSamplePos.x;
        densityJob.StartY = startSamplePos.y;
        densityJob.StartZ = startSamplePos.z;
        densityJob.Generator = generatorSettings.GetDensityGenerator();

        return densityJob;
    }

}
