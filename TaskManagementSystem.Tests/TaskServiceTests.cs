global using Moq;
using TaskManagementSystem.Services;
using TaskManagementSystem.Data;

namespace TaskManagementSystem.Tests
{
    [TestFixture]
    public class TaskServiceTests
    {
        private TaskService _taskService = null!;
        private Mock<ITaskRepository> _mockRepository = null!;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<ITaskRepository>();
            _taskService = new TaskService(_mockRepository.Object);
        }

        [Test]
        public void AddTask_ShouldAddTask_WhenNameIsValid()
        {
            string taskName = "New Task";
            
            _taskService.AddTask(taskName);
            
            _mockRepository.Verify(repo => repo.AddTask(It.Is<TaskItem>(t => t.Name == taskName)), Times.Once);
        }

        [Test]
        public void GetTasks_ShouldReturnAllTasks()
        {
            var tasks = new List<TaskItem> { new TaskItem { Id = 1, Name = "Test Task", IsCompleted = false } };
            _mockRepository.Setup(repo => repo.GetAllTasks()).Returns(tasks);

            var result = _taskService.GetTasks();
            
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Test Task"));
        }

        [Test]
        public void MarkTaskAsCompleted_ShouldSetTaskToCompleted()
        {
            _mockRepository.Setup(repo => repo.MarkTaskAsCompleted(1)).Returns(true);

            bool result = _taskService.MarkTaskAsCompleted(1);
            
            Assert.That(result, Is.True);
            _mockRepository.Verify(repo => repo.MarkTaskAsCompleted(1), Times.Once);
        }

        [Test]
        public void DeleteTask_ShouldRemoveTask()
        {
            _mockRepository.Setup(repo => repo.DeleteTask(1)).Returns(true);

            bool result = _taskService.DeleteTask(1);
            
            Assert.That(result, Is.True);
            _mockRepository.Verify(repo => repo.DeleteTask(1), Times.Once);
        }

        [Test]
        public void UpdateTask_ShouldModifyExistingTask()
        {
            string updatedName = "Updated Task";
            
            _mockRepository.Setup(repo => repo.UpdateTask(1, updatedName)).Returns(true);

            bool result = _taskService.UpdateTask(1, updatedName);
            
            Assert.That(result, Is.True);
            _mockRepository.Verify(repo => repo.UpdateTask(1, updatedName), Times.Once);
        }
    }
}
