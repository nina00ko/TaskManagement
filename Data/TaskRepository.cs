namespace TaskManagementSystem.Data
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _context;

        public event EventHandler<string>? TaskChanged;

        public TaskRepository(TaskDbContext context)
        {
            _context = context;
        }

        public void AddTask(TaskItem task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
            TaskChanged?.Invoke(this, $"Task '{task.Name}' was added.");
        }

        public List<TaskItem> GetAllTasks()
        {
            return _context.Tasks.ToList();
        }

        public TaskItem? GetTaskById(int id)
        {
            return _context.Tasks.FirstOrDefault(t => t.Id == id);
        }

        public bool MarkTaskAsCompleted(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.IsCompleted = true;
                _context.SaveChanges();
                TaskChanged?.Invoke(this, $"Task '{task.Name}' marked as completed.");
                return true;
            }
            return false;
        }

        public bool DeleteTask(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
                TaskChanged?.Invoke(this, $"Task '{task.Name}' was deleted.");
                return true;
            }
            return false;
        }

        public bool UpdateTask(int id, string newName)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.Name = newName;
                _context.SaveChanges();
                TaskChanged?.Invoke(this, $"Task '{task.Name}' was updated.");
                return true;
            }
            return false;
        }
    }
}
