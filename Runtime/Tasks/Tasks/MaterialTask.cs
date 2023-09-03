using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

struct MaterialTaskData
{
    public MaterialJob MaterialJob;
    public JobHandle Handle;
    public int3 ChunkPos; 
}

public class MaterialTask : ITask
{
    private WorldSettings _worldSettings;
    private WorldState _worldState;
    private GeneratorSettings _generatorSettings;
    private List<MaterialTaskData> _tasks;

    public MaterialTask(
        WorldSettings worldSettings, 
        WorldState worldState, 
        GeneratorSettings generatorSettings) 
    {
        _worldSettings = worldSettings;
        _worldState = worldState;
        _generatorSettings = generatorSettings;
        _tasks = new List<MaterialTaskData>();
    }

    public void End() {
        foreach(var task in _tasks) {
            task.Handle.Complete();
        }
    }

    public void Execute() {
        for (int i = _tasks.Count - 1; i >= 0; i--) {
            var task = _tasks[i];
            if (task.Handle.IsCompleted) {
                task.Handle.Complete();
                _tasks.RemoveAt(i);
            }
        }
    }

    public bool IsDone() {
        return _tasks.Count <= 0;
    }

    void StartMaterialTask(in ChunkUpdate chunkUpdate) {
        var materialJob = MaterialJobFactory.Create(_worldSettings, _worldState, _generatorSettings, in chunkUpdate);

        var task = new MaterialTaskData();
        task.MaterialJob = materialJob;
        task.Handle = materialJob.Schedule();
        task.ChunkPos = chunkUpdate.ChunkPosition;

        _tasks.Add(task);
    }

    public void Start() {
        var filteredChunkUpdates = _worldState.ChunkData.FilteredChunkUpdates;
        var chunkMap = _worldState.ChunkData.ChunkMap;

        for(int i = filteredChunkUpdates.Length -1; i >= 0; i--) {
            var update = filteredChunkUpdates[i];
            if(update.UpdateType == ChunkUpdateType.Create && chunkMap.ContainsKey(update.ChunkPosition)) {
                StartMaterialTask(filteredChunkUpdates[i]);
            }
        }
    }
}
