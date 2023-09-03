using System.Runtime.CompilerServices;
using Unity.Mathematics;

public struct DensityGenerator
{
    private FastNoiseLiteStruct _heightMapNoise;
    private FastNoiseLiteStruct _noise3D;
    private float _heightMapStrength;
    private float _noise3DStrength;

    public DensityGenerator(
        FastNoiseLiteStruct heightMapNoise, 
        FastNoiseLiteStruct noise3D, 
        float heightmapStrength, 
        float noise3dStrength)
    {
        _heightMapNoise = heightMapNoise;
        _noise3D = noise3D;
        _heightMapStrength = heightmapStrength;
        _noise3DStrength = noise3dStrength;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float GetValue(float3 position) {
        return GetValue(position.x, position.y, position.z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float GetValue(float x, float y, float z)
    {
        var plane = -y;
        var heightMap = _heightMapNoise.GetNoise(x, z) * _heightMapStrength;
        var noise3D = _noise3D.GetNoise(x, y, z) * _noise3DStrength;

        return plane + heightMap + noise3D;
    }
}
