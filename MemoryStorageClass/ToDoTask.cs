using System.ComponentModel.DataAnnotations;

namespace ToDoApplication.Models
{
    public enum StatusTypes
    {
        NotStarted = 1,
        InProgress = 2,
        Completed = 3
    }
    public enum PriorityTypes
    {
        LessImportant = 1,
        Important = 2,
        VeryImportant = 3
    }

    public class ToDoTask
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public PriorityTypes Priority { get; set; }
        public StatusTypes Status { get; set; }
        public ToDoTask()
        {
            Status = StatusTypes.NotStarted;
            Priority = PriorityTypes.Important;
        }
    }
}
