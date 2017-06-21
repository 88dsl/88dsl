using _88DSL.Models;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

[assembly: OwinStartupAttribute(typeof(_88DSL.Startup))]
namespace _88DSL
{
    [System.AttributeUsage(System.AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public sealed class RoleAuthorizationAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {


            LoginViewModel data = Utils.GetUserInfo();


            if (data!=null &&!string.IsNullOrEmpty(data.UserRole))
             {
                if (Roles.IndexOf(data.UserRole)>=0)
                {
                    return;
                }
            }

             filterContext.Result = new RedirectResult("~/Account/Login");



        }
    }
}
