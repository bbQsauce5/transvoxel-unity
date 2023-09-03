using System.Collections.Generic;
using UnityEngine;

public struct DebugWireCube
{
    public Vector3Int Position;
    public Vector3Int Size;
    public Color Color;
}

public class SceneDebug : MonoBehaviour
{
    private static Dictionary<Vector3Int, DebugWireCube> _wireCubes 
        = new Dictionary<Vector3Int, DebugWireCube>();

    private void OnDisable() {
        _wireCubes.Clear();
    }

    public static void AddDebugWireCubeToScene(Vector3Int position, DebugWireCube wireCube) {
        _wireCubes.Add(position, wireCube);
    }

    public static bool RemoveDebugWireCubeFromScene(Vector3Int position) {
        return _wireCubes.Remove(position);
    }

    private void OnDrawGizmos() {
        foreach(var wireCubeKvp in _wireCubes) {
            var wireCube = wireCubeKvp.Value;
            Gizmos.color = wireCube.Color;
            Gizmos.DrawWireCube(wireCube.Position, wireCube.Size);
        }
    }
}
