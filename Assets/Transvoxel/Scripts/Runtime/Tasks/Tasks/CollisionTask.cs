using System.Collections.Generic;
using Unity.Jobs;
using Unity.Mathematics;

struct CollisionTaskData
{
    public CollisionJob Job;
    public JobHandle Handle;
    public int3 ChunkPos;
}

public class CollisionTask : ITask
{
    private WorldState _worldState;
    private List<CollisionTaskData> _tasks;

    public CollisionTask(WorldState worldState) {
        _worldState = worldState;
        _tasks = new List<CollisionTaskData>();
    }

    public void End() {
        foreach (var task in _tasks) {
            task.Handle.Complete();
        }
        ChunkUtils.RemoveOldChunks(_worldState);
        ChunkUtils.EnablePendingChunks(_worldState);
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

    void StartCollisionTask(in ChunkUpdate update) {
        var chunkPos = update.ChunkPosition;

        var task = new CollisionTaskData();
        task.Job = CollisionJobFactory.Create(_worldState, chunkPos);
        task.Job.ChunkPosDebug = chunkPos;
        task.Handle = task.Job.Schedule();
        task.ChunkPos = chunkPos;


        _tasks.Add(task);
    }

    public bool IsDone() {
        return _tasks.Count <= 0;
    }

    public void Start() {
        var filteredChunkUpdates = _worldState.ChunkData.FilteredChunkUpdates;
        var chunkMap = _worldState.ChunkData.ChunkMap;
        for (int i = filteredChunkUpdates.Length - 1; i >= 0; i--) {
            var update = filteredChunkUpdates[i];
            if (update.UpdateType == ChunkUpdateType.Update && chunkMap.ContainsKey(update.ChunkPosition)) {
                StartCollisionTask(in update);
            }
        }
        filteredChunkUpdates.Clear();
    }
}