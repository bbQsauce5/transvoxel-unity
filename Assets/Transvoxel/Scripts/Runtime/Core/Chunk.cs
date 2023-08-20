using Unity.Mathematics;
using UnityEngine;

public class Chunk
{
    public ChunkActor Actor;
    public Mesh[] Meshes;

    public MeshNativeDataContainer MeshData;
    public int3 Position;
    public int LOD;
    public int NeighboursMask;
}
