using System;
using Unity.Collections;

public struct MeshNativeDataContainer : IDisposable
{
    MeshNativeData _mainMeshData;
    MeshNativeData _leftTransitionMeshData;
    MeshNativeData _downTransitionMeshData;
    MeshNativeData _backTransitionMeshData;
    MeshNativeData _rightTransitionMeshData;
    MeshNativeData _upTransitionMeshData;
    MeshNativeData _forwardTransitionMeshData;

    public MeshNativeDataContainer(
        MeshNativeData mainMeshData,
        MeshNativeData leftTransitionMeshData,
        MeshNativeData downTransitionMeshData,
        MeshNativeData backTransitionMeshData,
        MeshNativeData rightTransitionMeshData,
        MeshNativeData upTransitionMeshData,
        MeshNativeData forwardTransitionMeshData)
    {
        _mainMeshData = mainMeshData;
        _leftTransitionMeshData = leftTransitionMeshData;
        _downTransitionMeshData = downTransitionMeshData;
        _backTransitionMeshData = backTransitionMeshData;
        _rightTransitionMeshData = rightTransitionMeshData;
        _upTransitionMeshData = upTransitionMeshData;
        _forwardTransitionMeshData = forwardTransitionMeshData;
    }

    public MeshNativeData GetMainMeshData() => _mainMeshData;
    public MeshNativeData GetBackTransitionMeshData() => _backTransitionMeshData;
    public MeshNativeData GetDownTransitionMeshData() => _downTransitionMeshData;
    public MeshNativeData GetForwardTransitionMeshData() => _forwardTransitionMeshData;
    public MeshNativeData GetLeftTransitionMeshData() => _leftTransitionMeshData;
    public MeshNativeData GetRightTransitionMeshData() => _rightTransitionMeshData;
    public MeshNativeData GetUpTransitionMeshData() => _upTransitionMeshData;
    
    public bool HasAnyData() {
        return _mainMeshData.VertexData.Length > 0 ||
               _backTransitionMeshData.VertexData.Length > 0 ||
               _downTransitionMeshData.VertexData.Length > 0 ||
               _forwardTransitionMeshData.VertexData.Length > 0 ||
               _leftTransitionMeshData.VertexData.Length > 0 ||
               _rightTransitionMeshData.VertexData.Length > 0 ||
               _upTransitionMeshData.VertexData.Length > 0;
    }

    public void Dispose() {
        _mainMeshData.Dispose();
        _leftTransitionMeshData.Dispose();
        _downTransitionMeshData.Dispose();
        _backTransitionMeshData.Dispose();
        _rightTransitionMeshData.Dispose();
        _upTransitionMeshData.Dispose();
        _forwardTransitionMeshData.Dispose();
    }

    public void Clear() {
        _mainMeshData.Clear();
        _leftTransitionMeshData.Clear();
        _downTransitionMeshData.Clear();
        _backTransitionMeshData.Clear();
        _rightTransitionMeshData.Clear();
        _upTransitionMeshData.Clear();
        _forwardTransitionMeshData.Clear();
    }
}