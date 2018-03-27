using project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace project
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null || authCookie.Value == "")
                return;

            FormsAuthenticationTicket authTicket;
            try
            {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch
            {
                return;
            }

            // retrieve roles from UserData
            string[] roles = authTicket.UserData.Split(';');

            if (Context.User != null)
                Context.User = new GenericPrincipal(Context.User.Identity, roles);
        }

       // void Application_PostAuthenticateRequest(object sender, EventArgs e)
        //{
         //   var ctx = HttpContext.Current;
          //  if (ctx.Request.IsAuthenticated)
           // {
           //     string[] roles = LookupRolesForUser(ctx.User.Identity.Name);
             //   var newUser = new GenericPrincipal(ctx.User.Identity, roles);
             //   ctx.User = Thread.CurrentPrincipal = newUser;
            //}
       // }

      //  private string[] LookupRolesForUser(string username)
      //  {
          //  using (DBContext db = new DBContext())
          //  {
             //   usertable user = db.usertables.FirstOrDefault(u => u.Username.Equals(username, StringComparison.CurrentCultureIgnoreCase) || u.Email.Equals(username, StringComparison.CurrentCultureIgnoreCase));
        //
             //   var roles = from ur in db.UserRoles
                //            from r in db.Roles
                 //           where ur.UserID == r.RoleID
                  //          select r.Name;
               // if (roles != null)
               //     return roles.ToArray();
               // else
                //    return new string[] { }; ;
           // }
        //}
    }
}
