using System;
using System.Collections.Generic;
using ToDoApplication.Models;

namespace ToDoTest
{
    public class RepositoryTest
    {
        [Fact]
        public void GetAll_ShouldReturnAllTasks()
        {
            
            Repository repository = new Repository();
            repository.ClearAllData();
            List<ToDoTask> expectedTasks = new List<ToDoTask>();
            // Arrange
            expectedTasks.Add(new ToDoTask { Id = 1, Name = "Task 1", Priority = PriorityTypes.Important, Status = StatusTypes.NotStarted });
            expectedTasks.Add(new ToDoTask { Id = 2, Name = "Task 2", Priority = PriorityTypes.VeryImportant, Status = StatusTypes.InProgress });
            repository.Add(expectedTasks[0]);
            repository.Add(expectedTasks[1]);

            // Act
            var result = repository.GetAll();

            // Assert
            Assert.Equal(expectedTasks, result);
        }
        [Fact]
        public void Add_ShouldAddTaskToToDoTask()
        {
            Repository repository = new Repository();
            repository.ClearAllData();
            // Arrange
            ToDoTask todo = new ToDoTask
            {
                Id = 1,
                Name = "Task 1",
                Priority = PriorityTypes.VeryImportant,
                Status = StatusTypes.NotStarted
            };

            // Act
            repository.Add(todo);
            var result = repository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(todo.Id, result.ElementAt(0).Id);
            Assert.Equal(todo.Name, result.ElementAt(0).Name);
            Assert.Equal(todo.Priority, result.ElementAt(0).Priority);
            Assert.Equal(todo.Status, result.ElementAt(0).Status);
        }
        [Fact]
        public void Find_ShouldReturnCorrectTask()
        {
            Repository repository = new Repository();
            repository.ClearAllData();
            // Arrange
            ToDoTask expectedTask = new ToDoTask { Id = 1, Name = "Task 1", Priority = PriorityTypes.Important, Status = StatusTypes.NotStarted };
            repository.Add(expectedTask);

            // Act
            ToDoTask actualTask = repository.Find(1);

            // Assert
            Assert.Equal(expectedTask, actualTask);
        }
        [Fact]
        public void Remove_ShouldReturnRemovedTask()
        {
            Repository repository = new Repository();
            repository.ClearAllData();
            // Arrange
            ToDoTask task1 = new ToDoTask { Id = 1, Name = "Task 1", Priority = PriorityTypes.Important, Status = StatusTypes.NotStarted };
            ToDoTask task2 = new ToDoTask { Id = 2, Name = "Task 2", Priority = PriorityTypes.VeryImportant, Status = StatusTypes.InProgress };
            repository.Add(task1);
            repository.Add(task2);
            int countBeforeRemoval = repository.GetAll().Count();

            // Act
            ToDoTask removedTask = repository.Remove(1);

            // Assert
            int countAfterRemoval = repository.GetAll().Count();
            Assert.Equal(task1, removedTask);
            Assert.Equal(countBeforeRemoval - 1, countAfterRemoval);
        }
        [Fact]
        public void Update_ShouldUpdateTask()
        {
            Repository repository = new Repository();
            repository.ClearAllData();
            // Arrange
            ToDoTask task1 = new ToDoTask { Id = 1, Name = "Task 1", Priority = PriorityTypes.Important, Status = StatusTypes.NotStarted };
            ToDoTask task2 = new ToDoTask { Id = 2, Name = "Task 2", Priority = PriorityTypes.VeryImportant, Status = StatusTypes.InProgress };
            repository.Add(task1);
            repository.Add(task2);
            ToDoTask itemToUpdate = new ToDoTask { Id = 1, Name = "Updated Task", Priority = PriorityTypes.LessImportant, Status = StatusTypes.Completed };

            // Act
            repository.Update(1, itemToUpdate);
            ToDoTask updatedTask = repository.Find(1);

            // Assert
            Assert.Equal(itemToUpdate.Name, updatedTask.Name);
            Assert.Equal(itemToUpdate.Priority, updatedTask.Priority);
            Assert.Equal(itemToUpdate.Status, updatedTask.Status);
        }
        [Fact]
        public void ValidateToDoTask_ShouldReturnCorrectValidationResult()
        {
            // Arrange
            Repository repository = new Repository();
            repository.ClearAllData();

            // Act & Assert
            Assert.Equal(Messages.IsNullValue.Value, repository.ValidateToDoTask(null).Value);

            ToDoTask task = new ToDoTask
            {
                Id = 1,
                Name = "",
                Priority = PriorityTypes.VeryImportant,
                Status = (StatusTypes)4,
            };
            Assert.Equal(Messages.NameCannotEmpty.Value, repository.ValidateToDoTask(task).Value);

            task = new ToDoTask
            {
                Id = 2,
                Name = "Task 2",
                Priority = PriorityTypes.VeryImportant,
                Status = (StatusTypes)4,
            };
            Assert.Equal(Messages.InvalidStatus.Value, repository.ValidateToDoTask(task).Value);

            task = new ToDoTask
            {
                Id = 2,
                Name = "Task 2",
                Priority = (PriorityTypes)4,
                Status = StatusTypes.InProgress
            };
            Assert.Equal(Messages.InvalidPriority.Value, repository.ValidateToDoTask(task).Value);

        }

    }
}