using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

struct UniformityTaskData
{
    public ChunkUpdate ChunkUpdate;
    public UniformityJob UniformStateJob;
    public JobHandle Handle;
}

public class UniformityTask : ITask
{
    private WorldState _worldState;
    private List<UniformityTaskData> _chunkUniformStateTasks;

    public UniformityTask(WorldState worldState) {
        _worldState = worldState;
        _chunkUniformStateTasks = new List<UniformityTaskData>();
    }

    public void End() {
        foreach (var task in _chunkUniformStateTasks) {
            task.Handle.Complete();
        }
    }

    public void Execute() {
        for (int i = _chunkUniformStateTasks.Count - 1; i >= 0; i--) {
            var task = _chunkUniformStateTasks[i];
            if (task.Handle.IsCompleted) {
                _chunkUniformStateTasks.RemoveAt(i);
                FinishTask(task);
            }
        }
    }

    public bool IsDone() {
        return _chunkUniformStateTasks.Count <= 0;
    }

    void FinishTask(in UniformityTaskData task) {
        task.Handle.Complete();
        var uniformState = _worldState.ChunkData.ChunkUniformState;
        var index = task.UniformStateJob.Index;
        if (uniformState[index]) {
            var densityDataStorage = _worldState.DensityData;
            _worldState.Pools.DensityDataPool.Add(
                densityDataStorage.GetDataUnchecked(task.ChunkUpdate.ChunkPosition));
            densityDataStorage.RemoveData(task.ChunkUpdate.ChunkPosition);
        }
    }

    public void Start() {
        var chunkUpdates = _worldState.ChunkData.ChunkUpdates;
        var chunkUpdatesNum = chunkUpdates.Length;
        var chunkUniformState = _worldState.ChunkData.ChunkUniformState;
        if (chunkUniformState.Length < chunkUpdatesNum) {
            chunkUniformState.ResizeUninitialized(chunkUpdatesNum);
        }
        for (int i = 0; i < chunkUpdatesNum; i++) {
            var chunkUpdate = chunkUpdates[i];
            if (chunkUpdate.UpdateType == ChunkUpdateType.Create) { 
                var task = new UniformityTaskData();
                task.ChunkUpdate = chunkUpdates[i];
                task.UniformStateJob = UniformityJobFactory.Create(_worldState, task.ChunkUpdate.ChunkPosition, i);
                task.Handle = task.UniformStateJob.Schedule();
                _chunkUniformStateTasks.Add(task);
            }
        }
    }
}
