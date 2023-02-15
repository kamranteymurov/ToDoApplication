namespace ToDoApplication.Models
{
    public class Messages
    {
        private Messages(string value) { Value = value; }

        public string Value { get; private set; }

        public static Messages Success { get { return new Messages("Success."); } }
        public static Messages Error { get { return new Messages("Error."); } }
        public static Messages NameCannotEmpty { get { return new Messages("Name of the task cannot be empty."); } }
        public static Messages IsNullValue { get { return new Messages("IsNull exception."); } }
        public static Messages TaskAddedSuccessfully { get { return new Messages("Task added successfully."); } }
        public static Messages TaskUpdatedSuccessfully { get { return new Messages("Task updated successfully."); } }
        public static Messages TaskRemovedSuccessfully { get { return new Messages("Task removed successfully."); } }
        public static Messages InvalidId { get { return new Messages("ID not found."); } }
        public static Messages NegativeId { get { return new Messages("ID should be a positive number."); } }
        public static Messages UnCompletedTask { get { return new Messages("Task cannot deleted if it is not completed."); } }
        public static Messages IdShouldBeUnique { get { return new Messages("ID should be unique."); } }
        public static Messages InvalidStatus { get { return new Messages("Invalid status. Status can be only 'not_started', 'in_progress', or 'completed'."); } }
        public static Messages InvalidPriority { get { return new Messages("Invalid priority. Priority can be only 'important', 'very_important', or 'not_important'."); } }
        public static Messages TaskNotFound { get { return new Messages("Task not found."); } }

        public override string ToString()
        {
            return Value;
        }
    }
}
