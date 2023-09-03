using Unity.Collections;

public struct MeshNativeData
{
    public NativeList<VertexData> VertexData;
    public NativeList<uint> Indices;
    public NativeList<SecondaryVertexData> SecondaryVertices;

    public void Clear() {
        VertexData.Clear();
        Indices.Clear();
        SecondaryVertices.Clear();
    }

    public void Dispose() {
        VertexData.Dispose();
        Indices.Dispose();
        SecondaryVertices.Dispose();
    }
}