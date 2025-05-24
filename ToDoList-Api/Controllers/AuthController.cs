using Microsoft.AspNetCore.Mvc;
using ToDoList_Api.Data;
using ToDoList_Api.DTOs;
using ToDoList_Api.Services;

namespace ToDoList_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController (ApplicationDBContext _dbContext,TokenService userToken): ControllerBase
    {
        /// <summary>
        /// Authenticates a user and returns a JWT token.
        /// </summary>
        /// <param name="dTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public ActionResult<string> Login(LoginDTO dTO )
        {
            var user=_dbContext.Set<Users>() .Where(u => u.Username == dTO.Username && u.Password == dTO.Password).FirstOrDefault();
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }
            // Here you would typically generate a JWT token or session for the user
            var token = userToken.GenerateJwtToken(user);

            return Ok(new { token });
        }

        [HttpPost]
        [Route("register")]
        public ActionResult<string> Register(RegisterDTO registerDTO)
        {
            var user = new Users
            {
               
                Username = registerDTO.Username,
                Email = registerDTO.Email,
                Password = registerDTO.Password
            };

            user.Id = 0;

            if (user == null)
            {
                return BadRequest("User cannot be null.");
            }
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Username and password are required.");
            }
            if (_dbContext.Set<Users>().Any(u => u.Username == user.Username))
            {
                return BadRequest("Username already exists.");
            }
            _dbContext.Set<Users>().Add(user);
            _dbContext.SaveChanges();
            return Ok("User registered successfully.");
        }
    }
}
