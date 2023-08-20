#define SHOW_OCTREE_DEBUG_GIZMOS

using System.Collections.Generic;
using Unity.Assertions;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

public struct MesherTaskData
{
    public int3 ChunkPos;
    public int3 ChunkMin;
    public int LOD;
    public int NeighboursMask;

    public MesherJob MesherJob;
    public JobHandle Handle;
}

public class MesherTask : ITask
{
    private WorldSettings _worldSettings;
    private WorldState _worldState;
    private GeneratorSettings _generatorSettings;
    private List<MesherTaskData> _chunkTasks;

    public MesherTask(
        WorldSettings settings, 
        WorldState worldState, 
        GeneratorSettings generatorSettings) 
    {
        _worldSettings = settings;
        _worldState = worldState;
        _generatorSettings = generatorSettings;
        _chunkTasks = new List<MesherTaskData>();
    }

    public void End() { 
        foreach(var task in _chunkTasks) {
            task.Handle.Complete();
        }
    }

    public void Execute() {
        for (int i = _chunkTasks.Count - 1; i >= 0; i--) {
            var task = _chunkTasks[i];
            if (task.Handle.IsCompleted) {
                task.Handle.Complete();
                _chunkTasks.RemoveAt(i);
                FinishChunkTask(task);
            }
        }
    }

    void StartChunkTask(ChunkUpdate update) {
        if (update.UpdateType == ChunkUpdateType.Remove) {
            if (_worldState.ChunkData.ChunkMap.ContainsKey(update.ChunkPosition)) { 
                _worldState.ChunkData.ChunksToRemove.Add(update.ChunkPosition);
            }

#if SHOW_OCTREE_DEBUG_GIZMOS
            SceneDebug.RemoveDebugWireCubeFromScene(update.ChunkPosition.ToVector3Int());
#endif
        }
        else if (update.UpdateType == ChunkUpdateType.Create) {

#if SHOW_OCTREE_DEBUG_GIZMOS

            if(update.UpdateType == ChunkUpdateType.Create) {
                SceneDebug.AddDebugWireCubeToScene(update.ChunkPosition.ToVector3Int(), new DebugWireCube {
                    Position = update.ChunkPosition.ToVector3Int(),
                    Size = Vector3Int.one * (_worldSettings.ChunkSize << update.LOD),
                    Color = Color.white
                });
            }
#endif

            var mesherJob = MesherJobFactory.Create(
                _worldSettings, 
                _worldState, 
                _generatorSettings, 
                in update);

            var task = new MesherTaskData();
            task.ChunkPos = update.ChunkPosition;
            task.ChunkMin = CoordinateUtilities.GetChunkMin(update.ChunkPosition, _worldSettings.ChunkSize, update.LOD);
            task.LOD = update.LOD;
            task.NeighboursMask = update.NeighboursMask;
            task.MesherJob = mesherJob;

            var mesherHandle = task.MesherJob.Schedule();
            task.Handle = mesherHandle;

            _chunkTasks.Add(task);
        }
    }

    void FinishChunkTask(MesherTaskData task) {
        var chunkPos = task.ChunkPos;
        Assert.IsFalse(_worldState.ChunkData.ChunkMap.ContainsKey(chunkPos));

        var chunk = new Chunk();
        chunk.Position = chunkPos;
        chunk.LOD = task.LOD;
        chunk.NeighboursMask = task.NeighboursMask;

        var actor = _worldState.Pools.ActorPool.Get();
        actor.transform.position = task.ChunkMin.ToVector3();

        chunk.Actor = actor;
        chunk.MeshData = task.MesherJob.Mesher.MeshData;

        _worldState.ChunkData.ChunkMap.Add(chunkPos, chunk);

        _worldState.ChunkData.FilteredChunkUpdates.Add(new ChunkUpdate()
        {
            ChunkPosition = chunkPos,
            LOD = task.LOD,
            NeighboursMask = task.NeighboursMask,
            UpdateType = ChunkUpdateType.Update
        });
    }

    public bool IsDone() {
        return _chunkTasks.Count <= 0;
    }

    public void Start() {
        int updatesCount = _worldState.ChunkData.FilteredChunkUpdates.Length;

        for (int i = updatesCount - 1; i >= 0; i--) {
            var chunkUpdate = _worldState.ChunkData.FilteredChunkUpdates[i];
            StartChunkTask(chunkUpdate);
        }
    }
}
