using Unity.Mathematics;
using UnityEngine;

public static class Vector3IntExtensions
{
    public static int3 ToInt3(this Vector3Int vector3Int) {
        return new int3(vector3Int.x, vector3Int.y, vector3Int.z);
    }
}
