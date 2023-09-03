using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "FastNoiseLiteInstance", menuName = "FastNoiseLite/CreateInstance")]
public class FastNoiseLiteInstance : ScriptableObject
{
    [Header("Generic Settings")]
    [SerializeField] public int seed = 1337;
    [SerializeField] public float frequency = 0.01f;
    [SerializeField] public FastNoiseLiteStruct.NoiseType noiseType = FastNoiseLiteStruct.NoiseType.OpenSimplex2;
    [SerializeField] public FastNoiseLiteStruct.RotationType3D rotationType3D = FastNoiseLiteStruct.RotationType3D.None;

    [Header("Fractal Settings")]
    [SerializeField] public FastNoiseLiteStruct.FractalType fractalType = FastNoiseLiteStruct.FractalType.None;
    [SerializeField] public int octaves = 3;
    [SerializeField] public float lacunarity = 2.0f;
    [SerializeField] public float gain = 0.5f;
    [SerializeField] public float weightedStrength = 0.0f;
    [SerializeField] public float pingPongStrength = 2.0f;

    [Header("Cellular Settings")]
    [SerializeField] public FastNoiseLiteStruct.CellularDistanceFunction cellularDistanceFunction = FastNoiseLiteStruct.CellularDistanceFunction.EuclideanSq;
    [SerializeField] public FastNoiseLiteStruct.CellularReturnType cellularReturnType = FastNoiseLiteStruct.CellularReturnType.Distance;
    [SerializeField] public float cellularJitterModifier = 1.0f;

    [Header("Domain Warp Settings")]
    [SerializeField] public FastNoiseLiteStruct.DomainWarpType domainWarpType = FastNoiseLiteStruct.DomainWarpType.OpenSimplex2;
    [SerializeField] public float domainWarpAmp = 1.0f;

    private FastNoiseLiteStruct _fastNoiseLite;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float GetNoise(float x, float y) {
        return _fastNoiseLite.GetNoise(x, y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float GetNoise(float x, float y, float z) {
        return _fastNoiseLite.GetNoise(x, y, z);
    }

    public void ApplySettings() {
        _fastNoiseLite.SetSeed(seed);
        _fastNoiseLite.SetFrequency(frequency);
        _fastNoiseLite.SetNoiseType(noiseType);
        _fastNoiseLite.SetRotationType3D(rotationType3D);
        _fastNoiseLite.SetFractalType(fractalType);
        _fastNoiseLite.SetFractalOctaves(octaves);
        _fastNoiseLite.SetFractalLacunarity(lacunarity);
        _fastNoiseLite.SetFractalGain(gain);
        _fastNoiseLite.SetFractalWeightedStrength(weightedStrength);
        _fastNoiseLite.SetFractalPingPongStrength(pingPongStrength);
        _fastNoiseLite.SetCellularDistanceFunction(cellularDistanceFunction);
        _fastNoiseLite.SetCellularReturnType(cellularReturnType);
        _fastNoiseLite.SetCellularJitter(cellularJitterModifier);
        _fastNoiseLite.SetDomainWarpType(domainWarpType);
        _fastNoiseLite.SetDomainWarpAmp(domainWarpAmp);
    }

    private void OnEnable() {
        ApplySettings();
    }
}
