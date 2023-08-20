using Unity.Collections;

public struct NativeArrayAllocator<T> : IPersistentNativeDataAllocator<NativeArray<T>> where T : unmanaged
{
    private int _length;
    private NativeArrayOptions _options;

    public NativeArrayAllocator(int length, NativeArrayOptions options = NativeArrayOptions.ClearMemory) {
        _length = length;
        _options = options;
    }

    public NativeArray<T> Allocate() {
        NativeArray<T> array = new NativeArray<T>(_length, Allocator.Persistent, _options);
        GlobalNativeDataManager.Register(array);
        return array;
    }
}
