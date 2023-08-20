using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;

public class DensityData
{
    private Dictionary<int3, NativeArray<float>> _dataByChunkPosition
        = new Dictionary<int3, NativeArray<float>>();

    public NativeArray<float> GetDataUnchecked(int3 chunkPosition) {
        return _dataByChunkPosition[chunkPosition];
    }

    public bool RemoveData(int3 chunkPosition) {
        return _dataByChunkPosition.Remove(chunkPosition);
    }

    public void StoreDataUnchecked(int3 chunkPosition, NativeArray<float> densityData) {
        _dataByChunkPosition.Add(chunkPosition, densityData);
    }
}
