using System.Collections;
using System.Collections.Generic;
using Unity.Assertions;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

struct MeshCopyTaskData 
{
    public int3 ChunkPos;
    public JobHandle Handle;
    public MeshCopyJob MeshCopyJob;
}

public class MeshCopyTask : ITask
{
    private WorldState _worldState;
    private List<MeshCopyTaskData> _chunkTasks;

    public MeshCopyTask(WorldState worldState) {
        _worldState = worldState;
        _chunkTasks = new List<MeshCopyTaskData>();
    }

    public void End() {
        foreach (var task in _chunkTasks) {
            task.Handle.Complete();
        }
    }

    public void Execute() {
        for (int i = _chunkTasks.Count - 1; i >= 0; i--) {
            var task = _chunkTasks[i];
            if (task.Handle.IsCompleted) {
                task.Handle.Complete();
                _chunkTasks.RemoveAt(i);
                FinishMeshCopyTask(task);
            }
        }
    }

    void FinishMeshCopyTask(in MeshCopyTaskData task) {
        var chunkData = _worldState.ChunkData;
        var pools = _worldState.Pools;
        var chunk = chunkData.ChunkMap[task.ChunkPos];

        // it's a transition only task, chunk already had meshes
        // we don't want to clear them immediately
        if (chunk.Meshes != null) {
            chunkData.MeshesToClear.Add(chunk.Meshes);
        }

        var meshes = pools.MeshPool.Get();
        Mesh.ApplyAndDisposeWritableMeshData(task.MeshCopyJob.MeshArray, meshes);

        // todo: calculate  mesh bounds in job!
        foreach (var mesh in meshes) {
            mesh.RecalculateBounds();
        }
        chunk.Meshes = meshes;

        chunkData.PendingChunks.Add(chunk);
    }

    void StartMeshCopyTask(in ChunkUpdate chunkUpdate) {
        var job = MeshCopyJobFactory.Create(_worldState, in chunkUpdate);

        // update neighbours mask, otherwise it will use the one at the time of Creation of this chunk 
        _worldState.ChunkData.ChunkMap[chunkUpdate.ChunkPosition].NeighboursMask = chunkUpdate.NeighboursMask;

        var task = new MeshCopyTaskData();
        task.Handle = job.Schedule();
        task.MeshCopyJob = job;
        task.ChunkPos = chunkUpdate.ChunkPosition;

        _chunkTasks.Add(task);
    }

    public bool IsDone() {
        return _chunkTasks.Count <= 0;
    }

    public void Start() {
        var filteredChunkUpdates = _worldState.ChunkData.FilteredChunkUpdates;
        var chunkMap = _worldState.ChunkData.ChunkMap;
        for (int i = 0; i < filteredChunkUpdates.Length; i++) {
            var update = filteredChunkUpdates[i];
            if (update.UpdateType == ChunkUpdateType.Update && chunkMap.ContainsKey(update.ChunkPosition)) {
                StartMeshCopyTask(in update);
            }
        }
    }
}
