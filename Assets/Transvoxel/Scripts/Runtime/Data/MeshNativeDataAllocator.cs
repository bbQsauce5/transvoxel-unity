using Unity.Collections;

struct MeshNativeDataAllocator : IPersistentNativeDataAllocator<MeshNativeDataContainer>
{
    public MeshNativeDataContainer Allocate() {
        var meshDataContainer = new MeshNativeDataContainer(
            AllocateMeshData(),
            AllocateMeshData(),
            AllocateMeshData(),
            AllocateMeshData(),
            AllocateMeshData(),
            AllocateMeshData(),
            AllocateMeshData());

        GlobalNativeDataManager.Register(meshDataContainer);
        return meshDataContainer;
    }

    MeshNativeData AllocateMeshData() {
        var meshData = new MeshNativeData();
        meshData.VertexData = new NativeList<VertexData>(Allocator.Persistent); 
        meshData.Indices = new NativeList<uint>(Allocator.Persistent);
        meshData.SecondaryVertices = new NativeList<SecondaryVertexData>(Allocator.Persistent);
        return meshData;
    }
}
