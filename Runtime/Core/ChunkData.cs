using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

public class ChunkData
{
    public NativeList<ChunkUpdate> ChunkUpdates;
    public NativeList<ChunkUpdate> FilteredChunkUpdates;
    public NativeList<bool> ChunkUniformState;

    public Dictionary<int3, Chunk> ChunkMap;
    public LinearOctree ChunkTree;
    public List<int3> ChunksToRemove;
    public List<Chunk> PendingChunks;
    public List<Mesh[]> MeshesToClear;

    public ChunkData(WorldSettings worldSettings) {
        int rootDepth = Mathf.RoundToInt(
            Mathf.Log(worldSettings.WorldSize / worldSettings.ChunkSize, 2));

        ChunkMap = new Dictionary<int3, Chunk>();
        ChunkTree = new LinearOctree(
            new NativeParallelHashMapAllocator<ulong, OctreeNode>().Allocate(),
            int3.zero,
            worldSettings.WorldSize,
            rootDepth);
        PendingChunks = new List<Chunk>();
        ChunksToRemove = new List<int3>();
        MeshesToClear = new List<Mesh[]>();

        ChunkUpdates = new NativeListAllocator<ChunkUpdate>().Allocate();
        FilteredChunkUpdates = new NativeListAllocator<ChunkUpdate>().Allocate();
        ChunkUniformState = new NativeListAllocator<bool>().Allocate();
    }
}
