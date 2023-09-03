using System;
using Unity.Collections;

public struct NativeParallelHashMapAllocator<TKey, TValue> : IPersistentNativeDataAllocator<NativeParallelHashMap<TKey, TValue>> 
    where TKey : unmanaged, IEquatable<TKey>
    where TValue : unmanaged
{
    private int _capacity;

    public NativeParallelHashMapAllocator(int capacity) {
        _capacity = capacity;
    }

    public NativeParallelHashMap<TKey, TValue> Allocate() {
        NativeParallelHashMap<TKey, TValue> hashMap = new NativeParallelHashMap<TKey, TValue>(_capacity, Allocator.Persistent);
        GlobalNativeDataManager.Register(hashMap);
        return hashMap;
    }
}
