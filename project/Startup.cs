using Microsoft.Owin;
using Owin;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using project.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

[assembly: OwinStartupAttribute(typeof(project.Startup))]
namespace project
{
    public partial class Startup 
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
           
        }

       
    }
}
