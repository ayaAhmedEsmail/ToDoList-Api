using ToDoList_Api.Models;

namespace ToDoList_Api.Specifications
{
    public class TaskWithUserSpecification : BaseSpecification<Tasks>
    {
        
        public TaskWithUserSpecification(int taskId, int userId) : base(x => x.Id == taskId && x.Id == userId)
        {
            AddInclude(x => x.UserId);
        }
    }
}
