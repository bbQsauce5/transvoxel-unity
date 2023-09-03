using Unity.Mathematics;

public struct IntBox
{
    public int3 Min;
    public int3 Max;

    public IntBox(int3 min, int3 max)
    {
        Min = min;
        Max = max;
    }

    public IntBox(int3 Position, int Extents)
    {
        Min = Position - Extents;
        Max = Position + Extents;
    }

    public bool Contains(int x, int y, int z)
    {
        return (x >= Min.x && x < Max.x)
            && (y >= Min.y && y < Max.y)
            && (z >= Min.z && z < Max.z);
    }

    public bool Contains(int3 p)
    {
        return Contains(p.x, p.y, p.z);
    }

    public bool Intersects(IntBox other)
    {
        return (Min.x < other.Max.x && Max.x > other.Min.x)
            && (Min.y < other.Max.y && Max.y > other.Min.y)
            && (Min.z < other.Max.z && Max.z > other.Min.z);
    }
}