using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct TransvoxelTablesBlobDataAllocator : IBlobDataAllocator<BlobAssetReference<TransvoxelTablesBlobAsset>>
{
    private static BlobAssetReference<TransvoxelTablesBlobAsset> _transvoxelTables;

    public BlobAssetReference<TransvoxelTablesBlobAsset> Allocate() {
        using (BlobBuilder blobBuilder = new BlobBuilder(Allocator.Temp)) {
            ref TransvoxelTablesBlobAsset transvoxelTables = ref blobBuilder.ConstructRoot<TransvoxelTablesBlobAsset>();

            var regularCornerOffset = blobBuilder.Allocate(ref transvoxelTables.RegularCornerOffset, 8);
            for (int i = 0, count = TransvoxelTables.RegularCornerOffset.Length; i < count; ++i) {
                regularCornerOffset[i] = TransvoxelTables.RegularCornerOffset[i].ToInt3();
            }

            var transitionCornerOffset = blobBuilder.Allocate(ref transvoxelTables.TransitionCornerOffset, 13);
            for (int i = 0, count = TransvoxelTables.TransitionCornerOffset.Length; i < count; ++i) {
                transitionCornerOffset[i] = TransvoxelTables.TransitionCornerOffset[i].ToInt3();
            }

            var regularCellClass = blobBuilder.Allocate(ref transvoxelTables.RegularCellClass, TransvoxelTables.RegularCellClass.Length);
            for (int i = 0, count = TransvoxelTables.RegularCellClass.Length; i < count; ++i) {
                regularCellClass[i] = TransvoxelTables.RegularCellClass[i];
            }

            var regularCellData = blobBuilder.Allocate(ref transvoxelTables.RegularCellData, TransvoxelTables.RegularCellData.Length);
            for (int i = 0, count = TransvoxelTables.RegularCellData.Length; i < count; ++i) {
                var cellData = TransvoxelTables.RegularCellData[i];

                var regCellData = new TransvoxelRegularCellData();
                regCellData.GeometryCounts = cellData.GetGeometryCount();
                var vertexIndex = blobBuilder.Allocate(ref regularCellData[i].VertexIndex, cellData.GetIndices().Length);

                regularCellData[i] = regCellData;

                var indices = cellData.GetIndices();

                for (int j = 0; j < vertexIndex.Length; ++j) {
                    vertexIndex[j] = indices[j];
                }
            }

            var regularVertexData = blobBuilder.Allocate(ref transvoxelTables.RegularVertexData, TransvoxelTables.RegularVertexData.Length);
            for (int i = 0, count = TransvoxelTables.RegularVertexData.Length; i < count; ++i) {
                var caseVertexData = blobBuilder.Allocate(ref regularVertexData[i], TransvoxelTables.RegularVertexData[i].Length);
                for (int j = 0; j < caseVertexData.Length; ++j) {
                    caseVertexData[j] = TransvoxelTables.RegularVertexData[i][j];
                }
            }

            var transitionCellClass = blobBuilder.Allocate(ref transvoxelTables.TransitionCellClass, TransvoxelTables.TransitionCellClass.Length);
            for (int i = 0, count = TransvoxelTables.TransitionCellClass.Length; i < count; ++i) {
                transitionCellClass[i] = TransvoxelTables.TransitionCellClass[i];
            }

            var transitionRegularCellData = blobBuilder.Allocate(ref transvoxelTables.TransitionRegularCellData, TransvoxelTables.TransitionRegularCellData.Length);
            for (int i = 0, count = TransvoxelTables.TransitionRegularCellData.Length; i < count; ++i) {
                var cellData = TransvoxelTables.TransitionRegularCellData[i];

                var regCellData = new TransvoxelRegularCellData();
                regCellData.GeometryCounts = cellData.GetGeometryCount();
                var vertexIndex = blobBuilder.Allocate(ref transitionRegularCellData[i].VertexIndex, cellData.GetIndices().Length);

                transitionRegularCellData[i] = regCellData;

                var indices = cellData.GetIndices();

                for (int j = 0; j < vertexIndex.Length; ++j) {
                    vertexIndex[j] = indices[j];
                }
            }

            var transitionCornerData = blobBuilder.Allocate(ref transvoxelTables.TransitionCornerData, TransvoxelTables.TransitionCornerData.Length);
            for (int i = 0, count = TransvoxelTables.TransitionCornerData.Length; i < count; ++i) {
                transitionCornerData[i] = TransvoxelTables.TransitionCornerData[i];
            }

            var transitionVertexData = blobBuilder.Allocate(ref transvoxelTables.TransitionVertexData, TransvoxelTables.TransitionVertexData.Length);
            for (int i = 0, count = TransvoxelTables.TransitionVertexData.Length; i < count; ++i) {
                var caseVertexData = blobBuilder.Allocate(ref transitionVertexData[i], TransvoxelTables.TransitionVertexData[i].Length);
                for (int j = 0; j < caseVertexData.Length; ++j) {
                    caseVertexData[j] = TransvoxelTables.TransitionVertexData[i][j];
                }
            }

            _transvoxelTables = blobBuilder.CreateBlobAssetReference<TransvoxelTablesBlobAsset>(Allocator.Persistent);
        }

        return _transvoxelTables;
    }
}
