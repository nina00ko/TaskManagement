using TaskManagementSystem.Data;

namespace TaskManagementSystem.Services
{
    public class TaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public void AddTask(string taskName)
        {
            var task = new TaskItem { Name = taskName, IsCompleted = false };
            _taskRepository.AddTask(task);
        }

        public List<TaskItem> GetTasks()
        {
            return _taskRepository.GetAllTasks();
        }

        public TaskItem? GetTaskById(int id)
        {
            return _taskRepository.GetTaskById(id);
        }

        public bool MarkTaskAsCompleted(int id)
        {
            return _taskRepository.MarkTaskAsCompleted(id);
        }

        public bool DeleteTask(int id)
        {
            return _taskRepository.DeleteTask(id);
        }

        public bool UpdateTask(int id, string newName)
        {
            return _taskRepository.UpdateTask(id, newName);
        }
    }
}