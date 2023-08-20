using Unity.Assertions;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

public struct LinearOctree
{
    public NativeParallelHashMap<ulong, OctreeNode> NodeMap;
    public int3 RootPosition;
    public int RootSize;
    public int RootDepth;
    public int LeafSize;

    public LinearOctree(
        NativeParallelHashMap<ulong, OctreeNode> nodeMap, 
        int3 rootPos, 
        int rootSize, 
        int rootDepth)
    {
        NodeMap = nodeMap;
        RootPosition = rootPos;
        RootSize = rootSize;
        RootDepth = rootDepth;
        LeafSize = RootSize >> RootDepth;

        CreateRootNode();
    }

    public void CreateRootNode() {
        // Root node is already created!
        Assert.IsFalse(NodeMap.ContainsKey(1));

        OctreeNode rootNode = new OctreeNode {
            Depth = RootDepth,
            LocCode = 1,
            Extents = RootSize / 2
        };

        NodeMap.Add(rootNode.LocCode, rootNode);
    }

    public OctreeNode RootNode => NodeMap[1];

    public bool NodeHasChildren(OctreeNode node) => NodeMap.ContainsKey(node.LocCode << 3);

    public OctreeNode GetChild(OctreeNode node, uint index) => NodeMap[(node.LocCode << 3) | index];

    public OctreeNode GetNodeAt(int3 position) {
        OctreeNode currentNode = RootNode;

        while(NodeHasChildren(currentNode)) {
            uint index = ((position.x > currentNode.Position.x) ? 1u : 0u)
                       + ((position.y > currentNode.Position.y) ? 2u : 0u)
                       + ((position.z > currentNode.Position.z) ? 4u : 0u);

            currentNode = GetChild(currentNode, index);
        }

        return currentNode;
    }

    public bool RemoveNode(ulong locCode) => NodeMap.Remove(locCode);

    public void SplitNode(OctreeNode node) {
        // Cannot split if it has children already
        Assert.IsFalse(NodeMap.ContainsKey(node.LocCode << 3));

        // Cannot split if it's already a leaf
        Assert.IsTrue(node.Depth > 0);

        int childDepth = node.Depth - 1;
        int childExtents = node.Extents >> 1;

        for(uint i = 0; i < 8; i++) {
            OctreeNode child;
            child.Depth = childDepth;
            child.Extents = childExtents;
            child.LocCode = (node.LocCode << 3) | i;
            child.Position = node.Position + new int3(
                childExtents * ((i & 1) > 0 ? 1 : -1),
                childExtents * ((i & 2) > 0 ? 1 : -1),
                childExtents * ((i & 4) > 0 ? 1 : -1));

            NodeMap.Add(child.LocCode, child);
        }
    }
}
