using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

public enum ChunkUpdateType
{
    Remove,
    Create,
    Update // update transition
}

public struct ChunkUpdate
{
    public ChunkUpdateType UpdateType;
    public int3 ChunkPosition;
    public int LOD;
    public int NeighboursMask;
}

public struct ChunkUpdateComparer : IComparer<ChunkUpdate>
{
    public int Compare(ChunkUpdate x, ChunkUpdate y)
    {
        return x.LOD < y.LOD ? 1 : -1;
    }
}

[BurstCompile]
public struct UpdatesJob : IJob
{
    public LinearOctree Octree;
    public float3 TargetPosition;

    public NativeList<ChunkUpdate> ChunkUpdates;
    public NativeParallelHashSet<ulong> ActiveNodes;

    public NativeParallelHashMap<int3, int> ActiveNodesNeighbours;
    public NativeParallelHashMap<int3, int> ChunkUpdatesMap;
    
    public void Execute()
    {
        GetChunkUpdates(Octree.RootNode);
        BuildTransitionMasks();
        ChunkUpdates.Sort(new ChunkUpdateComparer());
    }

    void BuildTransitionMasks()
    {
        var directions = GetTransitionDirections();

        IntBox worldBox = new IntBox(Octree.RootPosition, Octree.RootNode.Extents);

        for(int i = ChunkUpdates.Length - 1; i >= 0; --i)
        {
            var update = ChunkUpdates[i];

            if(update.UpdateType == ChunkUpdateType.Remove) {
                continue;
            }

            int neighboursMask = 0;
            int halfLeafSize = Octree.LeafSize >> 1;

            for(int j = 0; j < directions.Length; ++j) {
                int3 pos = update.ChunkPosition + directions[j] * ((halfLeafSize << update.LOD) + 5);

                if(!worldBox.Contains(pos)) {
                    continue;
                }

                var otherNode = Octree.GetNodeAt(pos);

                // todo refactor, this is way too complicated

                int neighbourBit = 1 << j;
                int neighbourBitOpposite = ((neighbourBit << 3) | (neighbourBit >> 3)) & 0b111111;

                if(otherNode.Depth < update.LOD) {
                    neighboursMask |= neighbourBit;
                }

                int desiredOtherNeighbourBit = otherNode.Depth > update.LOD ? neighbourBitOpposite : 0;

                int otherNeighboursMask = ActiveNodesNeighbours[otherNode.Position];
                int isolatedOtherNeighbourBit = otherNeighboursMask & neighbourBitOpposite;
                 
                bool bMismatch = isolatedOtherNeighbourBit != desiredOtherNeighbourBit;

                if (bMismatch) {
                    otherNeighboursMask ^= neighbourBitOpposite; 

                    if(!ChunkUpdatesMap.ContainsKey(otherNode.Position))
                    {
                        var transitionUpdate = new ChunkUpdate
                        {
                            ChunkPosition = otherNode.Position,
                            LOD = otherNode.Depth,
                            UpdateType = ChunkUpdateType.Update,
                            NeighboursMask = otherNeighboursMask
                        };
                        ChunkUpdates.Add(transitionUpdate);
                        ChunkUpdatesMap.Add(transitionUpdate.ChunkPosition, ChunkUpdates.Length - 1);
                    }
                    else
                    {
                        var updateIndex = ChunkUpdatesMap[otherNode.Position];
                        var otherNodeUpdate = ChunkUpdates[updateIndex];
                        otherNodeUpdate.NeighboursMask = otherNeighboursMask;
                        ChunkUpdates[updateIndex] = otherNodeUpdate;
                    }

                    ActiveNodesNeighbours[otherNode.Position] = otherNeighboursMask;
                }
            }

            ActiveNodesNeighbours[update.ChunkPosition] = neighboursMask;

            update.NeighboursMask = neighboursMask;
            ChunkUpdates[i] = update;
        }
    }

    NativeArray<int3> GetTransitionDirections() {
        NativeArray<int3> directions = new NativeArray<int3>(6, Allocator.Temp);

        for (int i = 0; i < 6; i++) {
            int idx = 1 << i;

            directions[i] = new int3(
                (idx & 1) * -1 + ((idx & 8) >> 3) * 1,
                ((idx & 2) >> 1) * -1 + ((idx & 16) >> 4) * 1,
                ((idx & 4) >> 2) * -1 + ((idx & 32) >> 5) * 1);
        }

        return directions;
    }

    void GetChunkUpdates(OctreeNode fromNode)
    {
        if (CanRender(fromNode))
        {
            if (!ActiveNodes.Contains(fromNode.LocCode))
            {
                ChunkUpdate update = new ChunkUpdate
                {
                    UpdateType = ChunkUpdateType.Create,
                    ChunkPosition = fromNode.Position,
                    LOD = fromNode.Depth
                };

                if (Octree.NodeHasChildren(fromNode))
                {
                    AddMergedLeavesUpdates(fromNode);
                }

                ChunkUpdates.Add(update);
                ChunkUpdatesMap.Add(update.ChunkPosition, ChunkUpdates.Length - 1);
                ActiveNodes.Add(fromNode.LocCode);
                ActiveNodesNeighbours.Add(update.ChunkPosition, 0);
            }
        }
        else
        {
            if(ActiveNodes.Contains(fromNode.LocCode))
            {
                ChunkUpdate update = new ChunkUpdate
                {
                    UpdateType = ChunkUpdateType.Remove,
                    ChunkPosition = fromNode.Position,
                    LOD = fromNode.Depth
                };

                ChunkUpdates.Add(update);
                ActiveNodes.Remove(fromNode.LocCode);
                ActiveNodesNeighbours.Remove(fromNode.Position);
            }

            if(!Octree.NodeHasChildren(fromNode))
            {
                Octree.SplitNode(fromNode);
            }

            for(uint i = 0; i < 8; ++i)
            {
                GetChunkUpdates(Octree.GetChild(fromNode, i));
            }
        }
    }

    void AddMergedLeavesUpdates(OctreeNode fromNode)
    {
        for (uint i = 0; i < 8; ++i)
        {
            OctreeNode child = Octree.GetChild(fromNode, i);
            if (Octree.NodeHasChildren(child))
            {
                AddMergedLeavesUpdates(child);
            }
            else
            {
                ChunkUpdate update = new ChunkUpdate
                {
                    UpdateType = ChunkUpdateType.Remove,
                    ChunkPosition = child.Position,
                    LOD = child.Depth
                };

                ChunkUpdates.Add(update);
                ActiveNodes.Remove(child.LocCode);
                ActiveNodesNeighbours.Remove(child.Position);
            }

            if (!Octree.RemoveNode(child.LocCode))
            {
                Log("Removing child with loc code: " + child.LocCode + " failed!");
            }
        }
    }

    private bool CanRender(OctreeNode node) {
        if(node.Depth == 0) {
            return true;
        }
        var nodePos = node.Position;

        float distX = math.abs(nodePos.x - TargetPosition.x);
        float distY = math.abs(nodePos.y - TargetPosition.y);
        float distZ = math.abs(nodePos.z - TargetPosition.z);

        float minDist = math.max(distX, math.max(distY, distZ));

        float compareDist = Octree.LeafSize * 1.5f * (1 << node.Depth);

        return minDist > compareDist;
    }

    [BurstDiscard]
    private void Log(string str) {
        Debug.LogError(str);
    }
}
