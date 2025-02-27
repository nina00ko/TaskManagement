using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data;
using TaskManagementSystem.Services;

class Program
{
    static void Main()
    {
        var optionsBuilder = new DbContextOptionsBuilder<TaskDbContext>();
        optionsBuilder.UseSqlite("Data Source=tasks.db");

        using TaskDbContext context = new TaskDbContext(optionsBuilder.Options);
        context.Database.Migrate(); // Ensure the database is up to date

        if (!context.Tasks.Any())
        {
            context.Tasks.AddRange(
                new TaskItem { Name = "Complete project report", IsCompleted = false },
                new TaskItem { Name = "Review pull requests", IsCompleted = false }
            );
            context.SaveChanges();
        }

        TaskRepository repository = new TaskRepository(context);
        TaskService taskService = new TaskService(repository);

        repository.TaskChanged += (_, message) => Console.WriteLine($"[Event] {message}");

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Task Management System");
            Console.WriteLine("1. View Tasks");
            Console.WriteLine("2. Add New Task");
            Console.WriteLine("3. Mark Task as Completed");
            Console.WriteLine("4. Delete Task");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            string? input = Console.ReadLine();
            Console.Clear();

            switch (input)
            {
                case "1":
                    var tasks = taskService.GetTasks();
                    if (tasks.Count == 0)
                    {
                        Console.WriteLine("No tasks available.");
                    }
                    else
                    {
                        foreach (var task in tasks)
                        {
                            Console.WriteLine($"ID: {task.Id}, Name: {task.Name}, Completed: {task.IsCompleted}");
                        }
                    }
                    break;

                case "2":
                    Console.Write("Enter task name: ");
                    string? taskName = Console.ReadLine();
                    taskService.AddTask(taskName ?? "");
                    Console.WriteLine("Task added.");
                    break;

                case "3":
                    Console.Write("Enter task ID to mark as completed: ");
                    if (int.TryParse(Console.ReadLine(), out int completeId))
                    {
                        if (taskService.MarkTaskAsCompleted(completeId))
                            Console.WriteLine("Task marked as completed.");
                        else
                            Console.WriteLine("Task not found.");
                    }
                    break;

                case "4":
                    Console.Write("Enter task ID to delete: ");
                    if (int.TryParse(Console.ReadLine(), out int deleteId))
                    {
                        if (taskService.DeleteTask(deleteId))
                            Console.WriteLine("Task deleted.");
                        else
                            Console.WriteLine("Task not found.");
                    }
                    break;

                case "5":
                    Console.WriteLine("Exiting...");
                    return;

                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }
    }
}
