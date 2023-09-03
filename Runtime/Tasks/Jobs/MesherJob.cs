using Unity.Burst;
using Unity.Jobs;

[BurstCompile]
public struct MesherJob : IJob
{
    public TransvoxelMesher Mesher;

    public void Execute() {
        Mesher.Poligonise();
        Mesher.PoligoniseTransitions();
    }
}
