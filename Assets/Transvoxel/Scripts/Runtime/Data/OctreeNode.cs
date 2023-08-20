using Unity.Mathematics;

public struct OctreeNode
{
    public int3 Position;
    public int Extents;
    public int Depth;

    public ulong LocCode;
}