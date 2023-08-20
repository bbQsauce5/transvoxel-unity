using System;
using System.Collections.Generic;
using Unity.Assertions;

public static class GlobalNativeDataManager
{
    private static List<IDisposable> _data = new List<IDisposable>();

    public static T Allocate<T>(IPersistentNativeDataAllocator<T> allocator) where T : IDisposable {
        T data = allocator.Allocate();
        _data.Add(data);
        return data;
    }

    public static void Register<T>(T allocation) where T : IDisposable {
        Assert.IsFalse(_data.Contains(allocation));

        _data.Add(allocation);
    }

    public static void Free() {
        foreach(var data in _data) {
            data.Dispose();
        }
    }
    
}
