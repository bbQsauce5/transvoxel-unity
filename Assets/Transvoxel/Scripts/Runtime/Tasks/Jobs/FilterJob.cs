using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

[BurstCompile]
public struct FilterJob : IJob
{
    public NativeList<ChunkUpdate> ChunkUpdates;
    public NativeList<ChunkUpdate> FilteredChunkUpdates;
    public NativeList<bool> ChunkUniformState;

    public void Execute() {
        for (int i = 0; i < ChunkUpdates.Length; i++) {
            var chunkUpdate = ChunkUpdates[i];
            if (chunkUpdate.UpdateType != ChunkUpdateType.Create || !ChunkUniformState[i]) {
                FilteredChunkUpdates.Add(ChunkUpdates[i]);
            }
        }
    }
}
