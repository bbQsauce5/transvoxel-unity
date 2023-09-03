using Unity.Collections;
using UnityEngine;

public class Pools
{
    public PoolSimple<ChunkActor> ActorPool;
    public PoolSimple<MeshNativeDataContainer> MeshDataContainerPool;
    public PoolSimple<NativeArray<float>> DensityDataPool;
    public PoolSimple<Mesh[]> MeshPool;

    public Pools(WorldSettings worldSettings) {
        ActorPool = new PoolSimple<ChunkActor>(100, () => {
            var chunkInstance = GameObject.Instantiate<ChunkActor>(worldSettings.ChunkActor);
            chunkInstance.gameObject.SetActive(false);
            return chunkInstance;
        });

        MeshDataContainerPool = new PoolSimple<MeshNativeDataContainer>(100, () => {
            return new MeshNativeDataAllocator().Allocate();
        });

        DensityDataPool = new PoolSimple<NativeArray<float>>(100, () => {
            var densitySize = worldSettings.ChunkSize + 3;
            return new NativeArrayAllocator<float>(
                densitySize * densitySize * densitySize, NativeArrayOptions.UninitializedMemory).Allocate();
        });

        MeshPool = new PoolSimple<Mesh[]>(100, () => {
            Mesh[] meshes = new Mesh[7];
            for (int i = 0; i < 7; i++) {
                meshes[i] = new Mesh();
                meshes[i].MarkDynamic();
            }
            return meshes;
        });
    }
}
