using Microsoft.AspNetCore.Mvc;
using ToDoList_Api.Data;


namespace ToDoList_Api.Controllers
{

    public class TaskController( ApplicationDBContext _dBContext) : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public ActionResult<IEnumerable<Tasks>> GetTasks() {
            
            var task = _dBContext.Set<Tasks>().ToList();
            return Ok(task);
        }



    }
}
