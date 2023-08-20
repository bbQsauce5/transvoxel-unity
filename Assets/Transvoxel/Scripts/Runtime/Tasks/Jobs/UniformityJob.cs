using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

[BurstCompile]
public struct UniformityJob : IJob
{
    public NativeArray<float> DensityData;

    [NativeDisableContainerSafetyRestriction]
    public NativeList<bool> ChunksUniformState;

    public int Index;

    public void Execute() {
        bool bAllAboveZero = DensityData[0] >= 0;
        bool bAllBelowZero = DensityData[0] < 0;
        for (int i = 1; i < DensityData.Length; i++) {
            var densityValue = DensityData[i];
            bAllAboveZero &= densityValue >= 0;
            bAllBelowZero &= densityValue < 0;

            if (!bAllAboveZero && !bAllBelowZero) {
                break;
            }
        }

        ChunksUniformState[Index] = bAllAboveZero || bAllBelowZero;
    }
}
