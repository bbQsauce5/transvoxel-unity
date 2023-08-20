using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;

public class FilterTask : ITask
{
    private FilterJob _filterChunkUpdatesJob;
    private JobHandle _jobHandle;

    public FilterTask(WorldState worldState) {
        _filterChunkUpdatesJob = FilterJobFactory.Create(worldState);
    }

    public void End() {
        _jobHandle.Complete();
        _filterChunkUpdatesJob.ChunkUpdates.Clear();
    }

    public void Execute() { }

    public bool IsDone()
    {
        return _jobHandle.IsCompleted;
    }

    public void Start() {
        _jobHandle = _filterChunkUpdatesJob.Schedule();
    }
}
