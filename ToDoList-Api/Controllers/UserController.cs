using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoList_Api.Data;

namespace ToDoList_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(JwtOptions JwtOptions) : ControllerBase
    {

        [HttpPost]
        [Route("auth")]
        public ActionResult<string> Authenticate([FromBody] Users user)
        {

            var takenHandler = new JwtSecurityTokenHandler();

            var tokenDiscriptor = new SecurityTokenDescriptor
            {
                Issuer = JwtOptions.Issure,
                Audience = JwtOptions.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.SigningKey)),
                    SecurityAlgorithms.HmacSha256),
                Subject= new ClaimsIdentity(new Claim[] { 
                    new( ClaimTypes.NameIdentifier , user.Username),
                    //new(ClaimTypes.NameIdentifier , user.Password),
                    new(ClaimTypes.NameIdentifier , user.Email),

                }) ,
                Expires = DateTime.UtcNow.AddMinutes(JwtOptions.lifetime)
            };

            var securityToken = takenHandler.CreateToken(tokenDiscriptor);
            var accesstaken = takenHandler.WriteToken(securityToken);
           

            return new JsonResult(accesstaken);

        }
    }
}
