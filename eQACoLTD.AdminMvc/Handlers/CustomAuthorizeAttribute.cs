﻿using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace eQACoLTD.AdminMvc.Handlers
{
    public class CustomAuthorizeAttribute:AuthorizeAttribute,IAuthorizationFilter 
    {
        public string Permissions { get; set; }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (string.IsNullOrEmpty(Permissions))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var userRoles = context.HttpContext.User.Claims.Where(x => x.Type == "role").SingleOrDefault()?.Value;
            if (userRoles == null || string.IsNullOrEmpty(userRoles))
            {
                context.Result=new UnauthorizedResult();
                return;
            }
            var requirementRoles = Permissions.Split(",");
            bool isContain = false;
            foreach (var r in requirementRoles)
            {
                if (userRoles.Contains(r)) isContain = true;
            }
            if (!isContain)
            {
                context.Result=new UnauthorizedResult();
                return;
            }
            return;
        }
    }
}