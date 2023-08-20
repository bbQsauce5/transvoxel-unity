public class WorldState
{
    public ChunkData ChunkData;
    public DensityData DensityData;
    public Pools Pools;

    public WorldState(WorldSettings worldSettings) {
        ChunkData = new ChunkData(worldSettings);
        Pools = new Pools(worldSettings);
        DensityData = new DensityData();
    }
}
