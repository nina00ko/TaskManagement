namespace TaskManagementSystem.Domain
{
    public class TaskItem
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public bool IsCompleted { get; set; }

        public TaskItem(int id, string name)
        {
            Id = id;
            Name = name;
            IsCompleted = false;
        }

        public TaskItem() { }
    }
}