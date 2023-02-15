using System.Collections.Concurrent;

namespace ToDoApplication.Models
{
    public class Repository : IRepository
    {
        private static List<ToDoTask> toDoTasks = new List<ToDoTask>();

        public IEnumerable<ToDoTask> GetAll()
        {
            return toDoTasks;
        }

        public void Add(ToDoTask item)
        {
            if (toDoTasks.Count == 0)
            {
                item.Id = 1;
            }
            else
            {
                var maxId = toDoTasks.Max(t => t.Id);
                item.Id = maxId + 1;
            }
            toDoTasks.Add(item);
        }

        public ToDoTask Find(long id)
        {
            ToDoTask item = toDoTasks.Find(T => T.Id == id);

            return item;
        }

        public ToDoTask Remove(long id)
        {
            ToDoTask item = toDoTasks.Find(T => T.Id == id);
            if (item != null)
            {
                toDoTasks.Remove(item);
            }
            return item;
        }

        public Messages Update(long id, ToDoTask item)
        {
            ToDoTask task = toDoTasks.Find(T => T.Id == id);
            if (task == null)
            {
                return Messages.InvalidId;
            }
            task.Name = item.Name;
            task.Priority = item.Priority;
            task.Status = item.Status;
            return Messages.Success;
        }

        public Messages ValidateToDoTask(ToDoTask task, string operation = "create")
        {
            if (task == null)
            {
                return Messages.IsNullValue;
            }
            if (String.IsNullOrEmpty(task.Name))
            {
                return Messages.NameCannotEmpty;
            }
            if (!Enum.IsDefined(typeof(StatusTypes), task.Status))
            {
                return Messages.InvalidStatus;
            }
            if (!Enum.IsDefined(typeof(PriorityTypes), task.Priority))
            {
                return Messages.InvalidPriority;
            }

            return Messages.Success;
        }
        public void ClearAllData()
        {
            toDoTasks.Clear();
        }
    }
}
