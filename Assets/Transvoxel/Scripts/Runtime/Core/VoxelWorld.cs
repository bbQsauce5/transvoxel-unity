using System.Collections;
using UnityEngine;

public class VoxelWorld : MonoBehaviour
{
    [SerializeField]
    private WorldSettings _worldSettings;

    [SerializeField]
    private GeneratorSettings _generatorSettings;

    private WorldState _worldState;
    private LoopingTaskScheduler _scheduler;
    private MemoryManager _memoryManager;

    void Awake() {
        InitData();
    }

    void Start() {
        StartTasks();
    }

    void OnDisable() {
        EndTasks();
    }

    void InitData() {
        _memoryManager = new MemoryManager();
        _memoryManager.Init();

        _worldState = new WorldState(_worldSettings);

        _scheduler = new LoopingTaskScheduler();
    }

    void StartTasks() {
        var chunkUpdatesTask = new UpdatesTask(
            _worldSettings,
            _worldState);

        var densityGenerationTask = new DensityTask(
            _worldSettings,
            _worldState,
            _generatorSettings);

        var chunkUniformStateTask = new UniformityTask(
            _worldState);

        var filterChunkUpdatesTask = new FilterTask(
            _worldState);

        var chunkGenerationTask = new MesherTask(
            _worldSettings,
            _worldState,
            _generatorSettings);

        var chunkMaterialTask = new MaterialTask(
            _worldSettings,
            _worldState, 
            _generatorSettings);

        var copyMeshTask = new MeshCopyTask(_worldState);

        var collisionTask = new CollisionTask(_worldState);

        _scheduler.StartTasks(
            chunkUpdatesTask,
            densityGenerationTask,
            chunkUniformStateTask,
            filterChunkUpdatesTask,
            chunkGenerationTask,
            chunkMaterialTask,
            copyMeshTask,
            collisionTask);
    }

    void EndTasks() {
        _scheduler.EndTasks();
        _memoryManager.Free();
    }

    void Update() {
        _scheduler.ProcessTasks();
    }
}

