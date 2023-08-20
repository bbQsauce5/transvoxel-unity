using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine.Rendering;
using static UnityEngine.Mesh;

[BurstCompile]
public struct MeshCopyJob : IJob
{
    public MeshNativeDataContainer MeshNativeData;
    public MeshDataArray MeshArray;
    public MeshData MainMeshData;
    public MeshData LeftTransitionMeshData;
    public MeshData DownTransitionMeshData;
    public MeshData BackTransitionMeshData;
    public MeshData RightTransitionMeshData;
    public MeshData UpTransitionMeshData;
    public MeshData ForwardTransitionMeshData;
    public int NeighboursMask;

    public void Execute() {

        MainMeshData = MeshArray[0];
        LeftTransitionMeshData = MeshArray[1];
        DownTransitionMeshData = MeshArray[2];
        BackTransitionMeshData = MeshArray[3];
        RightTransitionMeshData = MeshArray[4];
        UpTransitionMeshData = MeshArray[5];
        ForwardTransitionMeshData = MeshArray[6];

        CopyToMeshData(MeshNativeData.GetMainMeshData(), MainMeshData);
        CopyToMeshData(MeshNativeData.GetLeftTransitionMeshData(), LeftTransitionMeshData);
        CopyToMeshData(MeshNativeData.GetDownTransitionMeshData(), DownTransitionMeshData);
        CopyToMeshData(MeshNativeData.GetBackTransitionMeshData(), BackTransitionMeshData);
        CopyToMeshData(MeshNativeData.GetRightTransitionMeshData(), RightTransitionMeshData);
        CopyToMeshData(MeshNativeData.GetUpTransitionMeshData(), UpTransitionMeshData);
        CopyToMeshData(MeshNativeData.GetForwardTransitionMeshData(), ForwardTransitionMeshData);
    }

    void CopyToMeshData(MeshNativeData fromMeshNativeData, MeshData toMeshData) {
        var vertAttributes = new NativeArray<VertexAttributeDescriptor>(4, Allocator.Temp);
        vertAttributes[0] = new VertexAttributeDescriptor(VertexAttribute.Position, VertexAttributeFormat.Float32);
        vertAttributes[1] = new VertexAttributeDescriptor(VertexAttribute.Normal, VertexAttributeFormat.Float32);
        vertAttributes[2] = new VertexAttributeDescriptor(VertexAttribute.TexCoord1, VertexAttributeFormat.Float32, 4);
        vertAttributes[3] = new VertexAttributeDescriptor(VertexAttribute.TexCoord2, VertexAttributeFormat.Float32, 4);

        toMeshData.SetVertexBufferParams(fromMeshNativeData.VertexData.Length, vertAttributes);

        vertAttributes.Dispose();

        var vertices = toMeshData.GetVertexData<VertexData>();
        for (int i = 0; i < fromMeshNativeData.VertexData.Length; i++) {
            vertices[i] = fromMeshNativeData.VertexData[i];
        }

        for (int i = 0; i < fromMeshNativeData.SecondaryVertices.Length; ++i) {
            var secondaryVert = fromMeshNativeData.SecondaryVertices[i];

            if ((secondaryVert.VertexMask & NeighboursMask) == secondaryVert.VertexMask) {
                var vertData = vertices[secondaryVert.VertexIndex];
                vertData.Position = secondaryVert.Position;
                vertices[secondaryVert.VertexIndex] = vertData;
            }
        }

        var fromMeshNativeDataIndices = fromMeshNativeData.Indices;
        var indexList = new NativeList<uint>(fromMeshNativeDataIndices.Length, Allocator.Temp);

        // skip invalid triangles (when two vertices are approximately at the same position or the vertices are colinear)
        for (int i = 0; i < fromMeshNativeDataIndices.Length; i += 3) {
            var vertA = vertices[(int)fromMeshNativeDataIndices[i + 0]].Position;
            var vertB = vertices[(int)fromMeshNativeDataIndices[i + 1]].Position;
            var vertC = vertices[(int)fromMeshNativeDataIndices[i + 2]].Position;

            if (!(vertA.Approx(vertB) || vertB.Approx(vertC) || vertC.Approx(vertA)) && 
                !math.cross(vertA - vertB, vertA - vertC).Approx(new float3(0, 0, 0)))
            {
                indexList.Add(fromMeshNativeDataIndices[i + 0]);
                indexList.Add(fromMeshNativeDataIndices[i + 1]);
                indexList.Add(fromMeshNativeDataIndices[i + 2]);
            }
        }

        toMeshData.SetIndexBufferParams(indexList.Length, IndexFormat.UInt32);
        var indices = toMeshData.GetIndexData<uint>();
        for (int i = 0; i < indexList.Length; ++i) {
            indices[i] = indexList[i];
        }

        toMeshData.subMeshCount = 1;
        toMeshData.SetSubMesh(0, new SubMeshDescriptor(0, indices.Length));
    }
}
