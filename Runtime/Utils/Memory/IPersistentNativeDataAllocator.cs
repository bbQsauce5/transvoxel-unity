using System;

public interface IPersistentNativeDataAllocator<T> where T : IDisposable
{
    T Allocate();
}
