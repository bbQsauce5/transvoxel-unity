using System.Collections.Generic;
using Unity.Jobs;
using Unity.Mathematics;

public struct DensityTaskData 
{
    public int3 ChunkPosition;
    public DensityJob DensityJob;
    public JobHandle Handle;
}

public class DensityTask : ITask
{
    private WorldSettings _worldSettings;
    private WorldState _worldState;
    private GeneratorSettings _generatorSettings;
    private List<DensityTaskData> _densityTasks;

    public DensityTask(
        WorldSettings settings, 
        WorldState worldState, 
        GeneratorSettings generatorSettings)
    {
        _worldSettings = settings;
        _worldState = worldState;
        _generatorSettings = generatorSettings;
        _densityTasks = new List<DensityTaskData>();
    }

    public void End() {
        foreach(var task in _densityTasks) {
            task.Handle.Complete();
        }
    }

    public void Execute() {
        for (int i = _densityTasks.Count - 1; i >= 0; i--) {
            var task = _densityTasks[i];
            if (task.Handle.IsCompleted) {
                _densityTasks.RemoveAt(i);
                FinishTask(task);
            }
        }
    }

    public bool IsDone() {
        return _densityTasks.Count <= 0;
    }

    void FinishTask(DensityTaskData task) {
        task.Handle.Complete();
        _worldState.DensityData.StoreDataUnchecked(task.ChunkPosition, task.DensityJob.DensityData);
    }

    void StartTask(in ChunkUpdate update) {
        if(update.UpdateType == ChunkUpdateType.Create) {

            var densityJob = DensityJobFactory.Create(
                _worldSettings,
                _worldState,
                _generatorSettings, 
                in update);

            int densitySize = _worldSettings.ChunkSize + 3;
            var densityHandle = densityJob.Schedule(densitySize * densitySize * densitySize, densitySize * densitySize);

            var task = new DensityTaskData();
            task.DensityJob = densityJob;
            task.Handle = densityHandle;
            task.ChunkPosition = update.ChunkPosition;

            _densityTasks.Add(task);
        }
    }

    public void Start() {
        int updatesCount = _worldState.ChunkData.ChunkUpdates.Length;

        for (int i = updatesCount - 1; i >= 0; i--) {
            var chunkUpdate = _worldState.ChunkData.ChunkUpdates[i];
            StartTask(chunkUpdate);
        }
    }
}
