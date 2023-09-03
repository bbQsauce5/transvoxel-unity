using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Entities;
using System.Runtime.CompilerServices;

public partial struct TransvoxelMesher
{
    [ReadOnly]
    public NativeArray<float> DensityData;
    public DensityGenerator Generator;

    public MeshNativeDataContainer MeshData;

    public int3 ChunkMin;
    public int LOD;
    public int NeighboursMask;
    public int ChunkSize;

    public BlobAssetReference<TransvoxelTablesBlobAsset> TablesRef;

    private const float TRANSITION_CELL_WIDTH_PERCENTAGE = 0.5f;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    float GetDensityValue(int x, int y, int z) {
        int DensitySize = ChunkSize + 3;
        return DensityData[x * DensitySize * DensitySize + y * DensitySize + z];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    float GetDensityValue(int3 position) {
        return GetDensityValue(position.x, position.y, position.z);
    }

    public void Poligonise() {
        int padding = 1;

        int LODScale = 1 << LOD;

        ref TransvoxelTablesBlobAsset tables = ref TablesRef.Value;

        var _currentCache = new NativeArray<int>(ChunkSize * ChunkSize * 4, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
        var _previousCache = new NativeArray<int>(ChunkSize * ChunkSize * 4, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
        var _vertexIndices = new NativeArray<int>(16, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
        var _cellValues = new NativeArray<float>(8, Allocator.Temp, NativeArrayOptions.UninitializedMemory);

        var vertexData = MeshData.GetMainMeshData().VertexData;
        var indices = MeshData.GetMainMeshData().Indices;
        var secondaryVerts = MeshData.GetMainMeshData().SecondaryVertices;

        for (int y = 0; y < ChunkSize; y++)
        {
            for (int z = 0; z < ChunkSize; z++)
            {
                for (int x = 0; x < ChunkSize; x++)
                {
                    var cellPos = new int3(x, y, z);

                    for (int i = 0; i < 8; ++i) {
                        int3 voxelPosition = cellPos + new int3(padding, padding, padding) + tables.RegularCornerOffset[i];
                        _cellValues[i] = GetDensityValue(voxelPosition.x, voxelPosition.y, voxelPosition.z);
                    }

                    int caseCode = ((_cellValues[0] < 0 ? 0x01 : 0)
                                  | (_cellValues[1] < 0 ? 0x02 : 0)
                                  | (_cellValues[2] < 0 ? 0x04 : 0)
                                  | (_cellValues[3] < 0 ? 0x08 : 0)
                                  | (_cellValues[4] < 0 ? 0x10 : 0)
                                  | (_cellValues[5] < 0 ? 0x20 : 0)
                                  | (_cellValues[6] < 0 ? 0x40 : 0)
                                  | (_cellValues[7] < 0 ? 0x80 : 0));

                    _currentCache[x * ChunkSize * 4 + z * 4] = -1;

                    if (caseCode == 0 || caseCode == 255) {
                        continue;
                    }

                    int cacheValidator = ((cellPos.x != 0 ? 0x01 : 0)
                                        | (cellPos.z != 0 ? 0x02 : 0)
                                        | (cellPos.y != 0 ? 0x04 : 0));

                    int cellClass = tables.RegularCellClass[caseCode];
                    ref var edgeCodes = ref tables.RegularVertexData[caseCode];
                    ref var cellData = ref tables.RegularCellData[cellClass];

                    long cellVertCount = cellData.GetVertexCount();

                    for (int i = 0; i < cellVertCount; ++i)
                    {
                        ushort edgeCode = edgeCodes[i];

                        ushort cornerIdx0 = (ushort)((edgeCode >> 4) & 0x0F);
                        ushort cornerIdx1 = (ushort)(edgeCode & 0x0F);

                        float density0 = _cellValues[cornerIdx0];
                        float density1 = _cellValues[cornerIdx1];

                        byte cacheIdx = (byte)((edgeCode >> 8) & 0x0F);
                        byte cacheDir = (byte)(edgeCode >> 12);

                        if (density1 == 0)
                        {
                            cacheDir = (byte)(cornerIdx1 ^ 7);
                            cacheIdx = 0;
                        }
                        else if (density0 == 0)
                        {
                            cacheDir = (byte)(cornerIdx0 ^ 7);
                            cacheIdx = 0;
                        }

                        bool bVertexCacheable = (cacheDir & cacheValidator) == cacheDir;
                        int vertexIndex = -1;

                        int cachePosX = x - (cacheDir & 1);
                        int cachePosZ = z - ((cacheDir >> 1) & 1);

                        var selectedCacheDock = ((cacheDir >> 2) & 1) == 1 ? _previousCache : _currentCache;

                        if (bVertexCacheable)
                        {
                            vertexIndex = selectedCacheDock[cachePosX * ChunkSize * 4 + cachePosZ * 4 + cacheIdx];
                        }

                        if (!bVertexCacheable || vertexIndex == -1)
                        {
                            float3 vertex;
                            float3 normal;

                            vertexIndex = vertexData.Length;

                            int vertBoundaryMask = 0;

                            if (cacheIdx == 0)
                            {
                                int3 cornerOffset = density0 == 0 
                                    ? tables.RegularCornerOffset[cornerIdx0]
                                    : tables.RegularCornerOffset[cornerIdx1];

                                int vertPosX = x + cornerOffset.x;
                                int vertPosY = y + cornerOffset.y;
                                int vertPosZ = z + cornerOffset.z;

                                vertex = new int3(vertPosX, vertPosY, vertPosZ);

                                if (LOD > 0)
                                {
                                    vertBoundaryMask = ((vertPosX == 0 ? 1 : 0)
                                                      | (vertPosY == 0 ? 2 : 0)
                                                      | (vertPosZ == 0 ? 4 : 0)
                                                      | (vertPosX == ChunkSize ? 8 : 0)
                                                      | (vertPosY == ChunkSize ? 16 : 0)
                                                      | (vertPosZ == ChunkSize ? 32 : 0));
                                }

                                vertPosX += padding;
                                vertPosY += padding;
                                vertPosZ += padding;

                                normal = new Vector3(GetDensityValue(vertPosX - 1, vertPosY, vertPosZ) - GetDensityValue(vertPosX + 1, vertPosY, vertPosZ),
                                                     GetDensityValue(vertPosX, vertPosY - 1, vertPosZ) - GetDensityValue(vertPosX, vertPosY + 1, vertPosZ),
                                                     GetDensityValue(vertPosX, vertPosY, vertPosZ - 1) - GetDensityValue(vertPosX, vertPosY, vertPosZ + 1));

                                if (bVertexCacheable)
                                {
                                    selectedCacheDock[cachePosX * ChunkSize * 4 + cachePosZ * 4 + cacheIdx] = vertexIndex;
                                }
                            }
                            else
                            {
                                int3 vertLocalPos0 = cellPos + tables.RegularCornerOffset[cornerIdx0];
                                int3 vertLocalPos1 = cellPos + tables.RegularCornerOffset[cornerIdx1];

                                float3 vert0Copy = vertLocalPos0;
                                float3 vert1Copy = vertLocalPos1;

                                for (int j = 0; j < LOD; ++j)
                                {
                                    float3 midPointLocalPos = (vert0Copy + vert1Copy) * 0.5f;
                                    float3 midPointWorldPos = ChunkMin + midPointLocalPos * LODScale;
                                    float midPointDensity = Generator.GetValue(midPointWorldPos);

                                    if (math.sign(midPointDensity) == math.sign(density0))
                                    {
                                        vert0Copy = midPointLocalPos;
                                        density0 = midPointDensity;
                                    }
                                    else
                                    {
                                        vert1Copy = midPointLocalPos;
                                        density1 = midPointDensity;
                                    }
                                }

                                float t0 = density1 / (density1 - density0);
                                float t1 = 1 - t0;

                                vertex = vert0Copy * t0 + vert1Copy * t1;

                                int vertPosX0 = vertLocalPos0.x;
                                int vertPosY0 = vertLocalPos0.y;
                                int vertPosZ0 = vertLocalPos0.z;

                                int vertPosX1 = vertLocalPos1.x;
                                int vertPosY1 = vertLocalPos1.y;
                                int vertPosZ1 = vertLocalPos1.z;

                                if (LOD > 0)
                                {
                                    vertBoundaryMask = (((vertPosX0 == 0 || vertPosX1 == 0) ? 1 : 0)
                                                      | ((vertPosY0 == 0 || vertPosY1 == 0) ? 2 : 0)
                                                      | ((vertPosZ0 == 0 || vertPosZ1 == 0) ? 4 : 0)
                                                      | ((vertPosX0 == ChunkSize || vertPosX1 == ChunkSize) ? 8 : 0)
                                                      | ((vertPosY0 == ChunkSize || vertPosY1 == ChunkSize) ? 16 : 0)
                                                      | ((vertPosZ0 == ChunkSize || vertPosZ1 == ChunkSize) ? 32 : 0));
                                }

                                vertPosX0 += padding;
                                vertPosY0 += padding;
                                vertPosZ0 += padding;

                                vertPosX1 += padding;
                                vertPosY1 += padding;
                                vertPosZ1 += padding;

                                float3 normal0 = new float3(GetDensityValue(vertPosX0 - 1, vertPosY0, vertPosZ0) - GetDensityValue(vertPosX0 + 1, vertPosY0, vertPosZ0),
                                                            GetDensityValue(vertPosX0, vertPosY0 - 1, vertPosZ0) - GetDensityValue(vertPosX0, vertPosY0 + 1, vertPosZ0),
                                                            GetDensityValue(vertPosX0, vertPosY0, vertPosZ0 - 1) - GetDensityValue(vertPosX0, vertPosY0, vertPosZ0 + 1));

                                float3 normal1 = new float3(GetDensityValue(vertPosX1 - 1, vertPosY1, vertPosZ1) - GetDensityValue(vertPosX1 + 1, vertPosY1, vertPosZ1),
                                                            GetDensityValue(vertPosX1, vertPosY1 - 1, vertPosZ1) - GetDensityValue(vertPosX1, vertPosY1 + 1, vertPosZ1),
                                                            GetDensityValue(vertPosX1, vertPosY1, vertPosZ1 - 1) - GetDensityValue(vertPosX1, vertPosY1, vertPosZ1 + 1));

                                normal = normal0 + normal1;

                                if (cornerIdx1 == 7)
                                {
                                    _currentCache[x * ChunkSize * 4 + z * 4 + cacheIdx] = vertexIndex;
                                }
                            }

                            normal = math.normalize(normal);

                            if (vertBoundaryMask > 0)
                            {
                                float3 Delta = new float3(0, 0, 0);

                                if ((vertBoundaryMask & 1) == 1 && vertex.x < 1)
                                {
                                    Delta.x = 1 - vertex.x;
                                }
                                else if ((vertBoundaryMask & 8) == 8 && vertex.x > (ChunkSize - 1))
                                {
                                    Delta.x = (ChunkSize - 1) - vertex.x;
                                }

                                if ((vertBoundaryMask & 2) == 2 && vertex.y < 1)
                                {
                                    Delta.y = 1 - vertex.y;
                                }
                                else if ((vertBoundaryMask & 16) == 16 && vertex.y > (ChunkSize - 1))
                                {
                                    Delta.y = (ChunkSize - 1) - vertex.y;
                                }

                                if ((vertBoundaryMask & 4) == 4 && vertex.z < 1)
                                {
                                    Delta.z = 1 - vertex.z;
                                }
                                else if ((vertBoundaryMask & 32) == 32 && vertex.z > (ChunkSize - 1))
                                {
                                    Delta.z = (ChunkSize - 1) - vertex.z;
                                }

                                Delta *= TRANSITION_CELL_WIDTH_PERCENTAGE;

                                float3 SecondaryVertPos = vertex + new float3(
                                            (1 - normal.x * normal.x) * Delta.x - normal.y * normal.x * Delta.y - normal.z * normal.x * Delta.z,
                                            -normal.x * normal.y * Delta.x + (1 - normal.y * normal.y) * Delta.y - normal.z * normal.y * Delta.z,
                                            -normal.x * normal.z * Delta.x - normal.y * normal.z * Delta.y + (1 - normal.z * normal.z) * Delta.z);

                                SecondaryVertexData SecondaryVertexData = new SecondaryVertexData();
                                SecondaryVertexData.Position = SecondaryVertPos * LODScale;
                                SecondaryVertexData.VertexMask = (ushort)vertBoundaryMask;
                                SecondaryVertexData.VertexIndex = (ushort)vertexIndex;
                                secondaryVerts.Add(SecondaryVertexData);
                            }

                            vertexData.Add(new VertexData
                            {
                                Position = vertex * LODScale,
                                Normal = normal
                            });
                        }
                        _vertexIndices[i] = vertexIndex;
                    }

                    long indexCount = cellData.GetTriangleCount() * 3;
                    ref var cellIndices = ref cellData.VertexIndex;

                    for (int i = 0; i < indexCount; i += 3)
                    {
                        int ia = _vertexIndices[cellIndices[i + 0]];
                        int ib = _vertexIndices[cellIndices[i + 1]];
                        int ic = _vertexIndices[cellIndices[i + 2]];

                        indices.Add((uint)ia);
                        indices.Add((uint)ib);
                        indices.Add((uint)ic);
                    }
                }
            }
            var temp = _currentCache;
            _currentCache = _previousCache;
            _previousCache = temp;
        }
    }
}
