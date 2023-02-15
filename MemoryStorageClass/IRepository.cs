namespace ToDoApplication.Models
{
    public interface IRepository
    {
        void Add(ToDoTask item);
        IEnumerable<ToDoTask> GetAll();
        ToDoTask Find(long id);
        Messages Update(long id,ToDoTask item);
        ToDoTask Remove(long id);
        Messages ValidateToDoTask(ToDoTask item, string operation = "create");
    }
}
