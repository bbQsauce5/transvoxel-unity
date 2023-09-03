using System.Collections.Generic;

public interface ITask
{
    void Start();
    void Execute();
    void End();
    bool IsDone();
}

public class LoopingTaskScheduler
{
    private Queue<ITask> _taskQueue;

    public void StartTasks(params ITask[] tasks) {
        _taskQueue = new Queue<ITask>();
        foreach(var task in tasks) {
            _taskQueue.Enqueue(task);
        }
        StartTask(
            _taskQueue.Peek());
    }

    public void ProcessTasks() {
        var task = _taskQueue.Peek();
        task.Execute();
        if (task.IsDone()) {
            task.End();
            _taskQueue.Enqueue(
                _taskQueue.Dequeue());
            StartTask(
                _taskQueue.Peek());
        }
    }

    public void EndTasks() {
        var task = _taskQueue.Peek();
        task.End();
        _taskQueue.Clear();
    }

    private void StartTask(ITask task) {
        task.Start();
    }
}
