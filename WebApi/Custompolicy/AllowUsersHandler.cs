using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Diagnostics;
using System.Diagnostics;

namespace WebApi.Custompolicy
{
    public class AllowUsersHandler : AuthorizationHandler<AllowUserPolicy>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AllowUserPolicy requirement)
        {
            
            var claims = context.User.Claims.ToList();
         
          

            var name = claims.Where(p => p.Type == "groups").Where(d => (d.Value == "YG-WCDTR-USERS" || d.Value == "PG8-TFS-All-Projxect-Admins")).FirstOrDefault()?.Value; ;

           

            if (name == null)
            {
                Debug.WriteLine("Fail! Unauthorized!");
                context.Fail();
            }
            else
            {   
                Debug.WriteLine(name.ToString());
                context.Succeed(requirement);
                
            }
            return Task.CompletedTask;
        }
    }
}
