using System.Collections;
using Unity.Assertions;
using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.Mesh;

public static class ChunkUtils
{
    public static void RemoveOldChunks(WorldState worldState) {
        var chunkData = worldState.ChunkData;
        var chunksToRemove = chunkData.ChunksToRemove;
        var chunkMap = chunkData.ChunkMap;
        var pools = worldState.Pools;

        for (int i = 0; i < chunksToRemove.Count; ++i) {
            var chunkToRemove = chunksToRemove[i];
            var chunk = chunkMap[chunkToRemove];
            chunkMap.Remove(chunkToRemove);

            chunk.MeshData.Clear();
            pools.MeshDataContainerPool.Add(chunk.MeshData);

            chunk.Actor.ClearColliders();
            chunk.Actor.ClearMeshFilters();

            foreach (var mesh in chunk.Meshes) {
                mesh.Clear();
            }
            pools.MeshPool.Add(chunk.Meshes);
            chunk.Meshes = null;

            // todo dither out
            chunk.Actor.gameObject.SetActive(false);
            pools.ActorPool.Add(chunk.Actor);
            chunk.Actor = null;

            pools.DensityDataPool.Add(
                worldState.DensityData.GetDataUnchecked(chunkToRemove));
            worldState.DensityData.RemoveData(chunkToRemove);
        }
        chunksToRemove.Clear();

        var meshesToClearList = chunkData.MeshesToClear;
        for (int i = 0; i < meshesToClearList.Count; i++)
        {
            var meshesToClear = chunkData.MeshesToClear[i];
            for (int j = 0; j < meshesToClear.Length; j++)
            {
                meshesToClear[j].Clear();
            }
            worldState.Pools.MeshPool.Add(meshesToClear);
        }
        meshesToClearList.Clear();
    }

    public static void EnablePendingChunks(WorldState worldState) {
        var chunkData = worldState.ChunkData;
        var pendingChunks = chunkData.PendingChunks;

        for (int i = 0; i < pendingChunks.Count; ++i) {
            var chunk = pendingChunks[i];
            chunk.Actor.gameObject.SetActive(true);
            chunk.Actor.ClearColliders();
            //chunk.Actor.SetTransitionsEnabled(chunk.NeighboursMask);
            chunk.Actor.SetMeshes(chunk.Meshes, chunk.NeighboursMask);
            chunk.Actor.SetColliders(chunk.Meshes, chunk.NeighboursMask);

            // todo dither in
        }
        pendingChunks.Clear();
    }
}
