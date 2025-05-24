using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ToDoList_Api.Authorization;
using ToDoList_Api.Data;
using ToDoList_Api.DTOs;
using ToDoList_Api.Models;


namespace ToDoList_Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class TaskController(ApplicationDBContext _dBContext) : ControllerBase
    {


        /// <summary>
        /// Get all tasks for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user whose tasks will be retrieved</param>
        /// <returns>List of tasks belonging to specific  user</returns>
        /// <response code="200">Returns the list of tasks.</response>
        /// <response code="500">If there was a server-side error.</response>
  
        [HttpGet]
        [Route("")]
        [CheckPermission(Permission.ReadTasks)]
        public ActionResult<IEnumerable<Tasks>> GetTasks() {

            // Get the user ID from the claims
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            try
            {
                var tasks = _dBContext.Set<Tasks>()
                                      .Where(t => t.UserId == userId)
                                      .ToList();

                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        /// <summary>
        /// Creates a new task.
        /// </summary>
        /// <param name="task">The task object to create.</param>
        /// <returns>The created task object.</returns>
        /// <response code="200">Task created successfully.</response>
        /// <response code="400">If the task object is null or invalid.</response>
        [HttpPost]
        [Route("")]
        [CheckPermission(Permission.CreatTask)]
        public ActionResult<int> CreateTask(TaskDTO taskDTO) {
            
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (taskDTO == null)
            {
                return BadRequest("Task cannot be null.");
            }
            if (string.IsNullOrEmpty(taskDTO.Title))
            {
                return BadRequest("Task title cannot be empty.");
            }

            var task = new Tasks
            {
                Title = taskDTO.Title,
                Description = taskDTO.Description,
                UserId = userId // Assign the user ID from the claims
            };


            try { task.Id = 0;
            _dBContext.Set<Tasks>().Add(task);
            _dBContext.SaveChanges();
            return Ok(task);
            }
       
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }


        /// <summary>
        /// Delete task by its ID.
        /// </summary>
        /// <param name="id">The ID of the task to delete.</param>
        /// <returns> A success message or not found. </returns>
        /// <response code="200">Task deleted successfully.</response>
        /// <response code="404">If the task is not found.</response>

        [HttpDelete]
        [Route("{id}")]
        [CheckPermission(Permission.DeleteTask)]
        public ActionResult DeleteTask(int id) {
          var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var task = _dBContext.Set<Tasks>().FirstOrDefault(i => i.Id == id && i.UserId == userId);
            if (task == null) {
                return NotFound("Task not found");
            }
            else {
                _dBContext.Set<Tasks>().Remove(task);
                _dBContext.SaveChanges();
                return Ok("Task Deleted sucsessfully");
            }
        }


        /// <summary>
        /// Updates an existing task.
        /// </summary>
        /// <param name="task">The task object with updated data.</param>
        /// <returns>The updated task object.</returns>
        /// <response code="200">Task updated successfully.</response>
        /// <response code="400">If the task is not found or invalid.</response>

        [HttpPut]
        [Route("task")]
        [CheckPermission(Permission.EditTask)]
        public ActionResult UpdateTask(TaskDTO taskDTO , int id) {

            var userId = int.Parse(User.FindFirst(ClaimTypes.Name).Value); 

            var exist_task = _dBContext.Set<Tasks>().FirstOrDefault(i=> i.Id == id && i.UserId == userId );
            if (exist_task != null) {
                exist_task.Description = taskDTO.Description;
                exist_task.Title = taskDTO.Title;
                _dBContext.Set<Tasks>().Update(exist_task);
                _dBContext.SaveChanges();
                return Ok(taskDTO);
            }
            else
            {
                return NoContent();
            }
        }





    }
}
