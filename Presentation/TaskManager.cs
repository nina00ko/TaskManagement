using TaskManagementSystem.Services;

namespace TaskManagementSystem.Presentation
{
    public class TaskManager
    {
        private readonly TaskService _service;

        public TaskManager(TaskService service)
        {
            _service = service;
        }
    }
}