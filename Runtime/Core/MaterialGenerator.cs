using Unity.Mathematics;


public struct MaterialGenerator
{
    private FastNoiseLiteStruct _stoneNoise;
    private float _grassBlendStrength;
    private float _grassAmount;
    private float _stoneThreshold;

    public MaterialGenerator(
        FastNoiseLiteStruct stoneNoise,
        float grassStrength,
        float grassAmount,
        float stoneThreshold) 
    {
        _stoneNoise = stoneNoise;
        _grassBlendStrength = grassStrength;
        _grassAmount = grassAmount;
        _stoneThreshold = stoneThreshold;
    }

    public void GetMaterial(ref VertexData vertexData, in int3 chunkMin) {
        var vertexPos = chunkMin + vertexData.Position;

        var x = vertexPos.x;
        var y = vertexPos.y;
        var z = vertexPos.z;

        float dot = _grassAmount + math.dot(vertexData.Normal, new float3(0, 1, 0));
        float topValue = dot > 0 ? math.pow(dot, _grassBlendStrength) : 0;

        float topMask = 1 - topValue;

        float stoneValue = _stoneNoise.GetNoise(x, y, z);
        stoneValue += 1;
        stoneValue *= 0.5f;

        stoneValue = stoneValue > _stoneThreshold ? 1 : 0;

        float noiseMask = 1 - stoneValue;

        vertexData.Materials = new float4(0, 1, 2, 0);

        vertexData.Blend.x = math.clamp(topValue, 0, 1);
        vertexData.Blend.y = math.clamp(math.min(topMask, noiseMask), 0, 1);
        vertexData.Blend.z = math.clamp(math.min(stoneValue, topMask), 0, 1);
        vertexData.Blend.w = 0.01f;  // UV scale
    }
}
