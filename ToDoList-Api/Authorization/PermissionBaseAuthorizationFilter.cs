using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ToDoList_Api.Data;
using ToDoList_Api.Models;

namespace ToDoList_Api.Authorization
{
    public class PermissionBaseAuthorizationFilter(ApplicationDBContext _dbContext): IAuthorizationFilter
    {
       
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var attributes = context.ActionDescriptor.EndpointMetadata.FirstOrDefault(x => x is CheckPermissionAttribute);
            var permissionAttribute = (CheckPermissionAttribute)attributes;

            if (permissionAttribute != null) {

                var claimIdentity = context.HttpContext.User.Identity as ClaimsIdentity;
                   
                if (claimIdentity == null && !claimIdentity.IsAuthenticated) { 
                    context.Result = new ForbidResult("Forbidden");
                }
                else
                {
                    var userId = int.Parse(claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
                    if (userId == null)
                    {
                        context.Result = new ForbidResult("Forbidden");
                    }
                    var permission = _dbContext.Set<UserPermissions>()
                    .Any(p => p.permissionId == permissionAttribute.Permission);
                    if (permission == null)
                    {
                        context.Result = new ForbidResult("Forbidden");
                    }
                }

                
            }
         
        }
    }

}
