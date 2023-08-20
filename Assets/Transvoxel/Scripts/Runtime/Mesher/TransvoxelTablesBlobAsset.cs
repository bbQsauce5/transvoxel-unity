using Unity.Entities;
using Unity.Mathematics;

public struct TransvoxelRegularCellData
{
    public byte GeometryCounts;
    public BlobArray<byte> VertexIndex;

    public long GetVertexCount() {
        return (GeometryCounts >> 4);
    }

    public long GetTriangleCount() {
        return (GeometryCounts & 0x0F);
    }
}

public struct TransvoxelTablesBlobAsset
{
    // Regular marching cubes tables
    public BlobArray<int3> RegularCornerOffset;
    public BlobArray<byte> RegularCellClass;
    public BlobArray<TransvoxelRegularCellData> RegularCellData;
    public BlobArray<BlobArray<ushort>> RegularVertexData;

    // Transitions marching cubes tables
    public BlobArray<int3> TransitionCornerOffset;
    public BlobArray<byte> TransitionCellClass;
    public BlobArray<TransvoxelRegularCellData> TransitionRegularCellData;
    public BlobArray<byte> TransitionCornerData;
    public BlobArray<BlobArray<ushort>> TransitionVertexData;
}