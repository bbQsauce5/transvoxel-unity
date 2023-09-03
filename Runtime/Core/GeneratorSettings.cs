using UnityEngine;

[System.Serializable]
public class GeneratorSettings
{
    [Header("Density")]
    [SerializeField] FastNoiseLiteInstance HeightmapNoise;
    [SerializeField] FastNoiseLiteInstance DensityNoise;
    [SerializeField] float HeightmapStrength;
    [SerializeField] float DensityStrength;

    [Header("Materials")]
    [SerializeField] FastNoiseLiteInstance StonesNoise;
    [SerializeField] [Range(0,10)] float GrassStrength;
    [SerializeField] [Range(0,1)] float GrassAmount;
    [SerializeField] [Range(0,1)] float StoneThreshold;

    public DensityGenerator GetDensityGenerator() {

        return new DensityGenerator(
            HeightmapNoise.GetStruct(), 
            DensityNoise.GetStruct(), 
            HeightmapStrength, 
            DensityStrength);
    }

    public MaterialGenerator GetMaterialGenerator() { 
        return new MaterialGenerator(
            StonesNoise.GetStruct(),
            GrassStrength,
            GrassAmount,
            StoneThreshold);
    }
}
