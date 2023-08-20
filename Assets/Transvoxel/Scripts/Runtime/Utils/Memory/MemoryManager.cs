public class MemoryManager
{
    public void Init() {
        GlobalBlobData<TransvoxelTablesBlobAsset>
            .AllocateBlobData(new TransvoxelTablesBlobDataAllocator());
    }

    public void Free() {
        GlobalBlobData<TransvoxelTablesBlobAsset>
            .GetInstance()
            .Dispose();

        GlobalNativeDataManager.Free();
    }
}
