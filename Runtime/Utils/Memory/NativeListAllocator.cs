using Unity.Collections;

public struct NativeListAllocator<T> : IPersistentNativeDataAllocator<NativeList<T>> where T : unmanaged
{
    private int _length;

    public NativeListAllocator(int length) {
        _length = length;
    }

    public NativeList<T> Allocate() {
        NativeList<T> list = new NativeList<T>(_length, Allocator.Persistent);
        GlobalNativeDataManager.Register(list);
        return list;
    }
}
