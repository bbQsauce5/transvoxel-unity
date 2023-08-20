using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public static class UniformityJobFactory
{
    public static UniformityJob Create(WorldState worldState, int3 chunkPosition, int index) {
        var job = new UniformityJob();
        job.ChunksUniformState = worldState.ChunkData.ChunkUniformState;
        job.DensityData = worldState.DensityData.GetDataUnchecked(chunkPosition);
        job.Index = index;

        return job;
    }
}
