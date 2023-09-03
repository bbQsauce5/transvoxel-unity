using Unity.Burst;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

[BurstCompile]
public struct CollisionJob : IJob
{
    public bool bBakeMainMesh;
    public bool bBakeLeftTransitionMesh;
    public bool bBakeDownTransitionMesh;
    public bool bBakeBackTransitionMesh;
    public bool bBakeRightTransitionMesh;
    public bool bBakeUpTransitionMesh;
    public bool bBakeForwardTransitionMesh;

    public int MainMeshId;
    public int LeftTransitionMeshId;
    public int DownTransitionMeshId;
    public int BackTransitionMeshId;
    public int RightTransitionMeshId;
    public int UpTransitionMeshId;
    public int ForwardTransitionMeshId;

    public int3 ChunkPosDebug;

    public void Execute() {
        var options =
            MeshColliderCookingOptions.CookForFasterSimulation |
            MeshColliderCookingOptions.WeldColocatedVertices |
            MeshColliderCookingOptions.UseFastMidphase |
            MeshColliderCookingOptions.EnableMeshCleaning;

        if (bBakeMainMesh) {
            Physics.BakeMesh(MainMeshId, false, options);
        }

        if (bBakeLeftTransitionMesh) {
            Physics.BakeMesh(LeftTransitionMeshId, false, options);
        }

        if (bBakeDownTransitionMesh) {
            Physics.BakeMesh(DownTransitionMeshId, false, options);
        }

        if (bBakeBackTransitionMesh) {
            Physics.BakeMesh(BackTransitionMeshId, false, options);
        }

        if (bBakeRightTransitionMesh) {
            Physics.BakeMesh(RightTransitionMeshId, false, options);
        }

        if (bBakeUpTransitionMesh) {
            Physics.BakeMesh(UpTransitionMeshId, false, options);
        }

        if (bBakeForwardTransitionMesh) {
            Physics.BakeMesh(ForwardTransitionMeshId, false, options);
        }
    }
}
