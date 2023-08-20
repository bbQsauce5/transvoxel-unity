using Unity.Mathematics;

public static class CoordinateUtilities
{
    public static int3 GetChunkMin(int3 chunkPosition, int chunkSize, int LOD) {
        int chunkExtents = (chunkSize >> 1) << LOD;
        int3 chunkMin = chunkPosition - chunkExtents;
        return chunkMin;
    }
}
