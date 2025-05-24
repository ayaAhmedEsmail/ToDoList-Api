namespace ToDoList_Api.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }

    }
}
