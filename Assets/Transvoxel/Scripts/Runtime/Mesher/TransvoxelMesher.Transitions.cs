using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Assertions;

public partial struct TransvoxelMesher
{
    private enum TransitionDirection
    {
        XMin,
        YMin,
        ZMin,
        XMax,
        YMax,
        ZMax,
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private int3 FaceToLocalSpace(
        TransitionDirection direction,
        int chunkSize,
        int x,
        int y,
        int z)
    {
        switch (direction)
        {
            case TransitionDirection.XMin:
                return new int3(z, x, y);
            case TransitionDirection.XMax:
                return new int3(chunkSize - z, y, x);
            case TransitionDirection.YMin:
                return new int3(y, z, x);
            case TransitionDirection.YMax:
                return new int3(x, chunkSize - z, y);
            case TransitionDirection.ZMin:
                return new int3(x, y, z);
            case TransitionDirection.ZMax:
                return new int3(y, x, chunkSize - z);
            default:
                return new int3(x, y, z);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private float3 FaceToLocalSpace(
        TransitionDirection direction,
        int chunkSize,
        float x,
        float y,
        float z)
    {
        switch (direction)
        {
            case TransitionDirection.XMin:
                return new float3(z, x, y);
            case TransitionDirection.XMax:
                return new float3(chunkSize - z, y, x);
            case TransitionDirection.YMin:
                return new float3(y, z, x);
            case TransitionDirection.YMax:
                return new float3(x, chunkSize - z, y);
            case TransitionDirection.ZMin:
                return new float3(x, y, z);
            case TransitionDirection.ZMax:
                return new float3(y, x, chunkSize - z);
            default:
                return new float3(x, y, z);
        }
    }

    public void PoligoniseTransitions() {
        PoligoniseTransition(MeshData.GetLeftTransitionMeshData(), TransitionDirection.XMin);
        PoligoniseTransition(MeshData.GetDownTransitionMeshData(), TransitionDirection.YMin);
        PoligoniseTransition(MeshData.GetBackTransitionMeshData(), TransitionDirection.ZMin);
        PoligoniseTransition(MeshData.GetRightTransitionMeshData(), TransitionDirection.XMax);
        PoligoniseTransition(MeshData.GetUpTransitionMeshData(), TransitionDirection.YMax);
        PoligoniseTransition(MeshData.GetForwardTransitionMeshData(), TransitionDirection.ZMax);
    }

    private void PoligoniseTransition(MeshNativeData transitionMeshData, TransitionDirection direction) {
        int padding = 1;
        int LODScale = 1 << LOD;

        var _trCurrentCache = new NativeArray<int>(ChunkSize * 10, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
        var _trPreviousCache = new NativeArray<int>(ChunkSize * 10, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
        var _trVertexIndices = new NativeArray<int>(36, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
        var _trCellValues = new NativeArray<float>(13, Allocator.Temp, NativeArrayOptions.UninitializedMemory);

        var vertexData = transitionMeshData.VertexData;
        var indices = transitionMeshData.Indices;
        var secondaryVerts = transitionMeshData.SecondaryVertices;

        ref TransvoxelTablesBlobAsset tables = ref TablesRef.Value;

        for (int y = 0; y < ChunkSize; y++)
        {
            for (int x = 0; x < ChunkSize; x++)
            {
                int3 pos0 = FaceToLocalSpace(direction, ChunkSize, x,     y,     0) + padding;
                int3 pos2 = FaceToLocalSpace(direction, ChunkSize, x + 1, y,     0) + padding;
                int3 pos6 = FaceToLocalSpace(direction, ChunkSize, x,     y + 1, 0) + padding;
                int3 pos8 = FaceToLocalSpace(direction, ChunkSize, x + 1, y + 1, 0) + padding;

                float3 pos1 = ChunkMin + FaceToLocalSpace(direction, ChunkSize, x + 0.5f, y,        0) * LODScale;
                float3 pos3 = ChunkMin + FaceToLocalSpace(direction, ChunkSize, x,        y + 0.5f, 0) * LODScale;
                float3 pos4 = ChunkMin + FaceToLocalSpace(direction, ChunkSize, x + 0.5f, y + 0.5f, 0) * LODScale;
                float3 pos5 = ChunkMin + FaceToLocalSpace(direction, ChunkSize, x + 1.0f, y + 0.5f, 0) * LODScale;
                float3 pos7 = ChunkMin + FaceToLocalSpace(direction, ChunkSize, x + 0.5f, y + 1.0f, 0) * LODScale;

                _trCellValues[0] = GetDensityValue(pos0);
                _trCellValues[2] = GetDensityValue(pos2);
                _trCellValues[6] = GetDensityValue(pos6);
                _trCellValues[8] = GetDensityValue(pos8);

                _trCellValues[1] = Generator.GetValue(pos1);
                _trCellValues[3] = Generator.GetValue(pos3);
                _trCellValues[4] = Generator.GetValue(pos4);
                _trCellValues[5] = Generator.GetValue(pos5);
                _trCellValues[7] = Generator.GetValue(pos7);

                _trCellValues[0x9] = _trCellValues[0];
                _trCellValues[0xA] = _trCellValues[2];
                _trCellValues[0xB] = _trCellValues[6];
                _trCellValues[0xC] = _trCellValues[8];

                int caseCode = ((_trCellValues[0] < 0 ? 1 : 0)
                              | (_trCellValues[1] < 0 ? 2 : 0)
                              | (_trCellValues[2] < 0 ? 4 : 0)
                              | (_trCellValues[5] < 0 ? 8 : 0)
                              | (_trCellValues[8] < 0 ? 16 : 0)
                              | (_trCellValues[7] < 0 ? 32 : 0)
                              | (_trCellValues[6] < 0 ? 64 : 0)
                              | (_trCellValues[3] < 0 ? 128 : 0)
                              | (_trCellValues[4] < 0 ? 256 : 0));

                _trCurrentCache[0 * ChunkSize + x] = -1;
                _trCurrentCache[1 * ChunkSize + x] = -1;
                _trCurrentCache[2 * ChunkSize + x] = -1;
                _trCurrentCache[7 * ChunkSize + x] = -1;

                if (caseCode == 0 || caseCode == 511) {
                    continue;
                }

                int cacheValidator = ((x != 0 ? 0b01 : 0)
                                    | (y != 0 ? 0b10 : 0));

                int cellClass = tables.TransitionCellClass[caseCode];
                ref var edgeCodes = ref tables.TransitionVertexData[caseCode];
                ref var cellData = ref tables.TransitionRegularCellData[cellClass & 0x7F];

                long cellVertCount = cellData.GetVertexCount();

                for (int i = 0; i < cellVertCount; ++i)
                {
                    ushort edgeCode = edgeCodes[i];

                    ushort cornerIdx0 = (ushort)((edgeCode >> 4) & 0x0F);
                    ushort cornerIdx1 = (ushort)(edgeCode & 0x0F);

                    float density0 = _trCellValues[cornerIdx0];
                    float density1 = _trCellValues[cornerIdx1];

                    byte cacheIdx = (byte)((edgeCode >> 8) & 0x0F);
                    byte cacheDir = (byte)(edgeCode >> 12);

                    if (density1 == 0)
                    {
                        byte trCornerData = tables.TransitionCornerData[cornerIdx1];
                        cacheDir = (byte)((trCornerData >> 4) & 0x0F);
                        cacheIdx = (byte)(trCornerData & 0x0F);
                    }
                    else if (density0 == 0)
                    {
                        byte trCornerData = tables.TransitionCornerData[cornerIdx0];
                        cacheDir = (byte)((trCornerData >> 4) & 0x0F);
                        cacheIdx = (byte)(trCornerData & 0x0F);
                    }

                    bool bVertexCacheable = (cacheDir & cacheValidator) == cacheDir;
                    int vertexIndex = -1;

                    int cachePosX = x - (cacheDir & 1);

                    var selectedCacheDock = (cacheDir & 2) > 0 ? _trPreviousCache : _trCurrentCache;

                    if (bVertexCacheable)
                    {
                        vertexIndex = selectedCacheDock[cacheIdx * ChunkSize + cachePosX];
                    }

                    if (!bVertexCacheable || vertexIndex == -1)
                    {
                        float3 vertex;
                        float3 normal;
                        vertexIndex = vertexData.Length;

                        int vertBoundaryMask = 0;
                        bool bIsLowResFace = cacheIdx > 6;

                        if (density0 == 0 || density1 == 0)
                        {
                            int cornerIdx = density0 == 0 ? cornerIdx0 : cornerIdx1;

                            int3 cornerOffset = tables.TransitionCornerOffset[cornerIdx];

                            vertex = FaceToLocalSpace(direction, ChunkSize, x + cornerOffset.x * 0.5f, y + cornerOffset.y * 0.5f, 0);

                            if (bIsLowResFace)
                            {
                                // todo avoid division, use 2 CornerOffset arrays?
                                int3 vertLocalPos = FaceToLocalSpace(direction, ChunkSize, x + cornerOffset.x / 2, y + cornerOffset.y / 2, 0);

                                int locX = vertLocalPos.x;
                                int locY = vertLocalPos.y;
                                int locZ = vertLocalPos.z;

                                vertBoundaryMask = ((locX == 0 ? 1 : 0)
                                                  | (locY == 0 ? 2 : 0)
                                                  | (locZ == 0 ? 4 : 0)
                                                  | (locX == ChunkSize ? 8 : 0)
                                                  | (locY == ChunkSize ? 16 : 0)
                                                  | (locZ == ChunkSize ? 32 : 0));

                                locX += padding;
                                locY += padding;
                                locZ += padding;

                                normal = new float3(GetDensityValue(locX - 1, locY, locZ) - GetDensityValue(locX + 1, locY, locZ),
                                                    GetDensityValue(locX, locY - 1, locZ) - GetDensityValue(locX, locY + 1, locZ),
                                                    GetDensityValue(locX, locY, locZ - 1) - GetDensityValue(locX, locY, locZ + 1));
                            }
                            else
                            {
                                float wX = ChunkMin.x + vertex.x * LODScale;
                                float wY = ChunkMin.y + vertex.y * LODScale;
                                float wZ = ChunkMin.z + vertex.z * LODScale;

                                normal = new float3(Generator.GetValue(new float3(wX - 1, wY, wZ)) - Generator.GetValue(new float3(wX + 1, wY, wZ)),
                                                    Generator.GetValue(new float3(wX, wY - 1, wZ)) - Generator.GetValue(new float3(wX, wY + 1, wZ)),
                                                    Generator.GetValue(new float3(wX, wY, wZ - 1)) - Generator.GetValue(new float3(wX, wY, wZ + 1)));
                            }

                            if (cacheDir == 8)
                            {
                                _trCurrentCache[cacheIdx * ChunkSize + x] = vertexIndex;
                            }
                            else if (bVertexCacheable)
                            {
                                selectedCacheDock[cacheIdx * ChunkSize + cachePosX] = vertexIndex;
                            }
                        }
                        else
                        {
                            int3 cornerOffset0 = tables.TransitionCornerOffset[cornerIdx0];
                            int3 cornerOffset1 = tables.TransitionCornerOffset[cornerIdx1];

                            float3 corner0Copy = FaceToLocalSpace(direction, ChunkSize, x + cornerOffset0.x * 0.5f, y + cornerOffset0.y * 0.5f, 0);
                            float3 corner1Copy = FaceToLocalSpace(direction, ChunkSize, x + cornerOffset1.x * 0.5f, y + cornerOffset1.y * 0.5f, 0);

                            int subEdges = bIsLowResFace ? LOD : LOD - 1;

                            for (int j = 0; j < subEdges; ++j)
                            {
                                float3 midPointLocalPos = (corner0Copy + corner1Copy) * 0.5f;

                                float3 midPointWorldPos = ChunkMin + midPointLocalPos * LODScale;

                                float midPointDensity = Generator.GetValue(midPointWorldPos);

                                if (Mathf.Sign(midPointDensity) == Mathf.Sign(density0))
                                {
                                    corner0Copy = midPointLocalPos;
                                    density0 = midPointDensity;
                                }
                                else
                                {
                                    corner1Copy = midPointLocalPos;
                                    density1 = midPointDensity;
                                }
                            }

                            float t0 = density1 / (density1 - density0);
                            float t1 = 1 - t0;

                            vertex = corner0Copy * t0 + corner1Copy * t1;
                            float3 normal0, normal1;

                            if (bIsLowResFace)
                            {
                                int3 vert0LocPos = FaceToLocalSpace(direction, ChunkSize, x + cornerOffset0.x / 2, y + cornerOffset0.y / 2, 0);
                                int3 vert1LocPos = FaceToLocalSpace(direction, ChunkSize, x + cornerOffset1.x / 2, y + cornerOffset1.y / 2, 0);

                                int VX0 = vert0LocPos.x;
                                int VY0 = vert0LocPos.y;
                                int VZ0 = vert0LocPos.z;

                                int VX1 = vert1LocPos.x;
                                int VY1 = vert1LocPos.y;
                                int VZ1 = vert1LocPos.z;

                                vertBoundaryMask = ((VX0 == 0 || VX1 == 0 ? 1 : 0)
                                                  | (VY0 == 0 || VY1 == 0 ? 2 : 0)
                                                  | (VZ0 == 0 || VZ1 == 0 ? 4 : 0)
                                                  | (VX0 == ChunkSize || VX1 == ChunkSize ? 8 : 0)
                                                  | (VY0 == ChunkSize || VY1 == ChunkSize ? 16 : 0)
                                                  | (VZ0 == ChunkSize || VZ1 == ChunkSize ? 32 : 0));

                                VX0 += padding;
                                VY0 += padding;
                                VZ0 += padding;
                                    
                                VX1 += padding;
                                VY1 += padding;
                                VZ1 += padding;

                                normal0 = new float3(GetDensityValue(VX0 - 1, VY0, VZ0) - GetDensityValue(VX0 + 1, VY0, VZ0),
                                                     GetDensityValue(VX0, VY0 - 1, VZ0) - GetDensityValue(VX0, VY0 + 1, VZ0),
                                                     GetDensityValue(VX0, VY0, VZ0 - 1) - GetDensityValue(VX0, VY0, VZ0 + 1));

                                normal1 = new float3(GetDensityValue(VX1 - 1, VY1, VZ1) - GetDensityValue(VX1 + 1, VY1, VZ1),
                                                     GetDensityValue(VX1, VY1 - 1, VZ1) - GetDensityValue(VX1, VY1 + 1, VZ1),
                                                     GetDensityValue(VX1, VY1, VZ1 - 1) - GetDensityValue(VX1, VY1, VZ1 + 1));
                            }
                            else
                            {
                                float3 vert0LocPos = FaceToLocalSpace(direction, ChunkSize, x + cornerOffset0.x * 0.5f, y + cornerOffset0.y * 0.5f, 0);
                                float3 vert1LocPos = FaceToLocalSpace(direction, ChunkSize, x + cornerOffset1.x * 0.5f, y + cornerOffset1.y * 0.5f, 0);

                                // world coordinates
                                float wX0 = ChunkMin.x + vert0LocPos.x * LODScale;
                                float wY0 = ChunkMin.y + vert0LocPos.y * LODScale;
                                float wZ0 = ChunkMin.z + vert0LocPos.z * LODScale;

                                float wX1 = ChunkMin.x + vert1LocPos.x * LODScale;
                                float wY1 = ChunkMin.y + vert1LocPos.y * LODScale;
                                float wZ1 = ChunkMin.z + vert1LocPos.z * LODScale;

                                normal0 = new float3(Generator.GetValue(new float3(wX0 - 1, wY0, wZ0)) - Generator.GetValue(new float3(wX0 + 1, wY0, wZ0)),
                                                     Generator.GetValue(new float3(wX0, wY0 - 1, wZ0)) - Generator.GetValue(new float3(wX0, wY0 + 1, wZ0)),
                                                     Generator.GetValue(new float3(wX0, wY0, wZ0 - 1)) - Generator.GetValue(new float3(wX0, wY0, wZ0 + 1)));

                                normal1 = new float3(Generator.GetValue(new float3(wX1 - 1, wY1, wZ1)) - Generator.GetValue(new float3(wX1 + 1, wY1, wZ1)),
                                                     Generator.GetValue(new float3(wX1, wY1 - 1, wZ1)) - Generator.GetValue(new float3(wX1, wY1 + 1, wZ1)),
                                                     Generator.GetValue(new float3(wX1, wY1, wZ1 - 1)) - Generator.GetValue(new float3(wX1, wY1, wZ1 + 1)));
                            }

                            normal = normal0 + normal1;

                            if (cacheDir == 8)
                            {
                                _trCurrentCache[cacheIdx * ChunkSize + x] = vertexIndex;
                            }
                            else if (bVertexCacheable && cacheDir != 4)
                            {
                                selectedCacheDock[cacheIdx * ChunkSize + cachePosX] = vertexIndex;
                            }
                        }

                        normal = math.normalize(normal);

                        // if vertex lies on the low resolution face of the transition cell
                        if (vertBoundaryMask > 0)
                        {
                            float3 delta = float3.zero;

                            if ((vertBoundaryMask & 1) == 1 && vertex.x < 1)
                            {
                                delta.x = 1 - vertex.x;
                            }
                            else if ((vertBoundaryMask & 8) == 8 && vertex.x > (ChunkSize - 1))
                            {
                                delta.x = (ChunkSize - 1) - vertex.x;
                            }

                            if ((vertBoundaryMask & 2) == 2 && vertex.y < 1)
                            {
                                delta.y = 1 - vertex.y;
                            }
                            else if ((vertBoundaryMask & 16) == 16 && vertex.y > (ChunkSize - 1))
                            {
                                delta.y = (ChunkSize - 1) - vertex.y;
                            }

                            if ((vertBoundaryMask & 4) == 4 && vertex.z < 1)
                            {
                                delta.z = 1 - vertex.z;
                            }
                            else if ((vertBoundaryMask & 32) == 32 && vertex.z > (ChunkSize - 1))
                            {
                                delta.z = (ChunkSize - 1) - vertex.z;
                            }

                            delta *= TRANSITION_CELL_WIDTH_PERCENTAGE;

                            float3 vertexSecondaryPos = vertex + new float3(
                                    (1 - normal.x * normal.x) * delta.x - normal.y * normal.x * delta.y - normal.z * normal.x * delta.z,
                                       -normal.x * normal.y * delta.x + (1 - normal.y * normal.y) * delta.y - normal.z * normal.y * delta.z,
                                       -normal.x * normal.z * delta.x - normal.y * normal.z * delta.y + (1 - normal.z * normal.z) * delta.z);

                            secondaryVerts.Add(new SecondaryVertexData()
                            {
                                Position = vertexSecondaryPos * LODScale,
                                VertexMask = (ushort)vertBoundaryMask,
                                VertexIndex = (ushort)vertexIndex
                            });
                        }

                        vertexData.Add(new VertexData
                        {
                            Position = vertex * LODScale,
                            Normal = normal
                        });
                    }

                    _trVertexIndices[i] = vertexIndex;
                }

                long indexCount = cellData.GetTriangleCount() * 3;
                ref var cellIndices = ref cellData.VertexIndex;

                bool bFlipWinding = (cellClass & 0x80) > 0;

                for (int i = 0; i < indexCount; i += 3)
                {
                    int ia = _trVertexIndices[cellIndices[i + 0]];
                    int ib = _trVertexIndices[cellIndices[i + 1]];
                    int ic = _trVertexIndices[cellIndices[i + 2]];

                    if (!bFlipWinding)
                    {
                        indices.Add((uint)ic);
                        indices.Add((uint)ib);
                        indices.Add((uint)ia);
                    }
                    else
                    {
                        indices.Add((uint)ia);
                        indices.Add((uint)ib);
                        indices.Add((uint)ic);
                    }
                }
            }
            var temp = _trCurrentCache;
            _trCurrentCache = _trPreviousCache;
            _trPreviousCache = temp;
        }
    }
}
