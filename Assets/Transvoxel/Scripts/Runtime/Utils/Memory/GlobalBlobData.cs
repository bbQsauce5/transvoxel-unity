using Unity.Entities;
using UnityEngine;

public static class GlobalBlobData<T> where T : unmanaged
{
    private static BlobAssetReference<T> _data;
    private static bool _created = false;
    public static BlobAssetReference<T> GetInstance() {
        if(!_created) {
            Debug.LogError("Blob Data of type: " + typeof(T) + " is not yet allocated.");
        }
        return _data;
    }

    public static BlobAssetReference<T> AllocateBlobData<K>(K allocator) where K : struct, IBlobDataAllocator<BlobAssetReference<T>>
    {
        if (_created) {
            Debug.LogError("Blob Data of type: " + typeof(T) + " is already allocated.");
            return _data;
        }

        _data = allocator.Allocate();
        _created = true;

        return _data;
    }
}
