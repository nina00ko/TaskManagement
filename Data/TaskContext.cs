using Microsoft.EntityFrameworkCore;

public class TaskDbContext : DbContext
{
    public DbSet<TaskItem> Tasks { get; set; } 

    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

    public TaskDbContext() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=tasks.db");
        }
    }
}