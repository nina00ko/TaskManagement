namespace TaskManagementSystem.Data
{
    public interface ITaskRepository
    {
        void AddTask(TaskItem task);
        List<TaskItem> GetAllTasks();
        TaskItem? GetTaskById(int id);
        bool MarkTaskAsCompleted(int id);
        bool DeleteTask(int id);
        bool UpdateTask(int id, string newName);
    }
}