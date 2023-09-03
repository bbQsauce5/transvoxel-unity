using Unity.Jobs;

public class UpdatesTask : ITask
{
    private WorldSettings _worldSettings;
    private UpdatesJob _chunkUpdatesJob;
    private JobHandle _chunkUpdatesJobHandle;

    public UpdatesTask(WorldSettings settings, WorldState worldState) {
        _worldSettings = settings;
        _chunkUpdatesJob = UpdatesJobFactory.Create(worldState);
    }

    public void End() {
        _chunkUpdatesJobHandle.Complete();
    }

    public void Execute() { }

    public bool IsDone() {
        return _chunkUpdatesJobHandle.IsCompleted;
    }

    public void Start() {
        _chunkUpdatesJob.TargetPosition = _worldSettings.Target.position;
        _chunkUpdatesJob.ChunkUpdates.Clear();
        _chunkUpdatesJob.ChunkUpdatesMap.Clear();

        _chunkUpdatesJobHandle = _chunkUpdatesJob.Schedule();
    }
}