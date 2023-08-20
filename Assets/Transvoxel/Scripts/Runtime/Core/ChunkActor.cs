using UnityEngine;

public class ChunkActor : MonoBehaviour
{
    [SerializeField] private MeshFilter MainFilter;
    [SerializeField] private MeshFilter[] TransitionFilters;
    [SerializeField] private MeshCollider MainCollider;
    [SerializeField] private MeshCollider[] TransitionColliders;

    public void SetMeshes(Mesh[] meshes, int transitionFlags) {
        if (meshes[0].GetIndexCount(0) > 0) { 
            MainFilter.mesh = meshes[0];
        }
        for (int i = 0; i < 6; i++) {
            var mesh = meshes[i + 1];
            if (mesh.GetIndexCount(0) > 0 && (transitionFlags & (1 << i)) > 0) { 
                TransitionFilters[i].mesh = meshes[i + 1];
            }
        }
    }

    public void ClearMeshFilters() {
        MainFilter.mesh = null;
        for (int i = 0; i < 6; i++) {
            TransitionFilters[i].mesh = null;
        }
    }

    public void SetColliders(Mesh[] meshes, int transitionFlags) {
        if (meshes[0].GetIndexCount(0) > 0) {
            MainCollider.sharedMesh = meshes[0];
        }
        for (int i = 0; i < 6; i++) {
            var mesh = meshes[i + 1];
            if (mesh.GetIndexCount(0) > 0 && (transitionFlags & (1 << i)) > 0) {
                TransitionColliders[i].sharedMesh = mesh;
            }
        }
    }

    public void ClearColliders() {
        MainCollider.sharedMesh = null;
        for (int i = 0; i < 6; i++) {
            TransitionColliders[i].sharedMesh = null;
        }
    }
}
