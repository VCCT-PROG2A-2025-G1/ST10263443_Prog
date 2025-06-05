namespace chatBotLibrary
{
    public class TaskItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Reminder { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class TaskManager
    {
        private List<TaskItem> tasks = new List<TaskItem>();

        public IReadOnlyList<TaskItem> Tasks => tasks;

        public void AddTask(string title, string description, DateTime? reminder = null)
        {
            tasks.Add(new TaskItem { Title = title, Description = description, Reminder = reminder });
            // You can add events or notifications here to update UI or chatbot
        }

        public void RemoveTask(TaskItem task) => tasks.Remove(task);

        public void MarkComplete(TaskItem task) => task.IsCompleted = true;
    }
}
