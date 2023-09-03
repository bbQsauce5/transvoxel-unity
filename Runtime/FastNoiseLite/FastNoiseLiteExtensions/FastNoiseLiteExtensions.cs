using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FastNoiseLiteExtensions 
{
    public static void CopyFrom(this ref FastNoiseLiteStruct fastNoiseStruct, FastNoiseLiteInstance instance) {
        fastNoiseStruct.SetSeed(instance.seed);
        fastNoiseStruct.SetFrequency(instance.frequency);
        fastNoiseStruct.SetNoiseType(instance.noiseType);
        fastNoiseStruct.SetRotationType3D(instance.rotationType3D);
        fastNoiseStruct.SetFractalType(instance.fractalType);
        fastNoiseStruct.SetFractalOctaves(instance.octaves);
        fastNoiseStruct.SetFractalLacunarity(instance.lacunarity);
        fastNoiseStruct.SetFractalGain(instance.gain);
        fastNoiseStruct.SetFractalWeightedStrength(instance.weightedStrength);
        fastNoiseStruct.SetFractalPingPongStrength(instance.pingPongStrength);
        fastNoiseStruct.SetCellularDistanceFunction(instance.cellularDistanceFunction);
        fastNoiseStruct.SetCellularReturnType(instance.cellularReturnType);
        fastNoiseStruct.SetCellularJitter(instance.cellularJitterModifier);
        fastNoiseStruct.SetDomainWarpType(instance.domainWarpType);
        fastNoiseStruct.SetDomainWarpAmp(instance.domainWarpAmp);
    }

    public static FastNoiseLiteStruct GetStruct(this FastNoiseLiteInstance instance) {
        var fastNoiseStruct = new FastNoiseLiteStruct();
        fastNoiseStruct.CopyFrom(instance);
        return fastNoiseStruct;
    }
}
