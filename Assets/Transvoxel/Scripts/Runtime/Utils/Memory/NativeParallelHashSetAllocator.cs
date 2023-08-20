using System;
using Unity.Collections;

public struct NativeParallelHashSetAllocator<TKey> : IPersistentNativeDataAllocator<NativeParallelHashSet<TKey>>
    where TKey : unmanaged, IEquatable<TKey>
{
    private int _capacity;

    public NativeParallelHashSetAllocator(int capacity = 512) {
        _capacity = capacity;
    }

    public NativeParallelHashSet<TKey> Allocate() {
        NativeParallelHashSet<TKey> hashSet = new NativeParallelHashSet<TKey>(_capacity, Allocator.Persistent);
        GlobalNativeDataManager.Register(hashSet);
        return hashSet;
    }
}
