using Unity.Mathematics;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct VertexData
{
    public float3 Position;
    public float3 Normal;
    public float4 Materials;
    public float4 Blend;
}