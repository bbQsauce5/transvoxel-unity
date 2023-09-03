using Unity.Mathematics;
using UnityEngine;

public static class Int3Extensions
{
    public static Vector3 ToVector3(this int3 value) => new Vector3(value.x, value.y, value.z);
    public static Vector3Int ToVector3Int(this int3 value) => new Vector3Int(value.x, value.y, value.z);
}
