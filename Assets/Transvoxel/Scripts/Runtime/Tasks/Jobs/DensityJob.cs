using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

[BurstCompile]
public struct DensityJob : IJobParallelFor
{
    public DensityGenerator Generator;
    public int Size;
    public int Step;
    public int StartX;
    public int StartY;
    public int StartZ;

    public NativeArray<float> DensityData;
    
    public void Execute(int index)
    {
        int z = index % Size;
        int y = (index / Size) % Size;
        int x = index / (Size * Size);

        DensityData[index] = Generator.GetValue(StartX + x * Step, StartY + y * Step, StartZ + z * Step);
    }
}
