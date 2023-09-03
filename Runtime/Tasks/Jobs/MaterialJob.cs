using Unity.Burst;
using Unity.Jobs;
using Unity.Mathematics;

[BurstCompile]
public struct MaterialJob : IJob
{
    public MeshNativeDataContainer MeshData;
    public MaterialGenerator Generator;
    public int3 ChunkMin;

    public void Execute() {
        GenerateMaterialsForMesh(MeshData.GetMainMeshData());
        GenerateMaterialsForMesh(MeshData.GetLeftTransitionMeshData());
        GenerateMaterialsForMesh(MeshData.GetRightTransitionMeshData());
        GenerateMaterialsForMesh(MeshData.GetForwardTransitionMeshData());
        GenerateMaterialsForMesh(MeshData.GetBackTransitionMeshData());
        GenerateMaterialsForMesh(MeshData.GetUpTransitionMeshData());
        GenerateMaterialsForMesh(MeshData.GetDownTransitionMeshData());
    }

    void GenerateMaterialsForMesh(MeshNativeData meshNativeData) {
        var vertexDataList = meshNativeData.VertexData;

        for (int i = 0; i < vertexDataList.Length; i++) {
            var vertexData = vertexDataList[i];
            Generator.GetMaterial(ref vertexData, in ChunkMin);
            vertexDataList[i] = vertexData;
        }
    }
}
