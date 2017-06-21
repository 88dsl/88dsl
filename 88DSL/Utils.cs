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

    public static class Utils
    {
        public static LoginViewModel GetUserInfo()
        {

            LoginViewModel rn = new LoginViewModel();


            String tmpCookie = System.Web.HttpContext.Current.Request[FormsAuthentication.FormsCookieName];


            if (!string.IsNullOrEmpty(tmpCookie))
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(tmpCookie);

                if (ticket != null)
                {

                    String userData = ticket.UserData;

                    if (!string.IsNullOrWhiteSpace(userData))
                    {
                        String[] tmp = userData.Split(',');
                        if (tmp != null && tmp.Length >= 4)
                        {

                            rn.UserId = tmp[0];
                            rn.UserName = tmp[1];
                            rn.UserRole = tmp[2];
                            rn.Balance = tmp[3];

                        }
                    }


                }
            }

            return rn;
        }
    }
}
