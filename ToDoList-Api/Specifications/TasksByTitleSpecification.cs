using System.Linq.Expressions;
using ToDoList_Api.Models;

namespace ToDoList_Api.Specifications
{
    public class TasksByTitleSpecification : BaseSpecification<Tasks>
    {
        public TasksByTitleSpecification(int userID, string taskTitle) 
            : base(t=>t.Title.Contains(taskTitle)&& t.UserId==userID)
        {
            AddInclude(t => t.UserId);
        }
    }
}
