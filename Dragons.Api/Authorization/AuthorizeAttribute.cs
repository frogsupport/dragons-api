using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;

namespace Dragons.Api.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IEnumerable<string> _roles;

        public AuthorizeAttribute(params string[] roles)
        {
            _roles = roles ?? Array.Empty<string>();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            

            // Check if the user has a role assignment for each of the roles
            foreach (var role in _roles) 
            {
                // TODO implement grabbing of roles

                Console.WriteLine("Role is: " + role);
                // If any role is not found then the user is unauthorized
                // context.Result = new ForbidResult();
            }
        }
    }
}
