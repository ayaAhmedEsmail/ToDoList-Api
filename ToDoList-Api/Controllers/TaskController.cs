using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ToDoList_Api.Data;


namespace ToDoList_Api.Controllers
{

    public class TaskController(ApplicationDBContext _dBContext) : ControllerBase
    {
        [HttpGet]
        [Route("{id}")]
        public ActionResult<IEnumerable<Tasks>> GetTasks(int id) {

            var task = _dBContext.Set<Tasks>().ToList();
            return Ok(task);
        }


        /// <summary>
        /// Create Task 
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]

        public ActionResult<int> CreateTask(Tasks task) {
            if (task == null) {
                return BadRequest();
            }
            _dBContext.Set<Tasks>().Add(task);
            _dBContext.SaveChanges();
            return Ok(task);
        }


        /// <summary>
        /// Delete task 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete]
        [Route("id")]
        public ActionResult DeleteTask(int id) {
            var task = _dBContext.Set<Tasks>().Find(id);
            if (task == null) {
                return BadRequest();
            }
            else {
                _dBContext.Set<Tasks>().Remove(task);
                _dBContext.SaveChanges();
                return Ok("Task Deleted sucsessfully");
            }


        }

        [HttpPut]
        [Route("/task")]
        public ActionResult UpdateTask(Tasks task) { 
            var exist_task = _dBContext.Set<Tasks>().Find(task.Id);
            if (exist_task != null) {
                exist_task.Id = task.Id;
                exist_task.Description = task.Description;
                exist_task.Title = task.Title;
                _dBContext.Set<Tasks>().Update(exist_task);
                _dBContext.SaveChanges();
                return Ok(task);
            }
            else
            {
                return BadRequest("Task not found");
            }
        }





    }
}
