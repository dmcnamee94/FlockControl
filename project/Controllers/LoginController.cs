using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project.Models.Repositories;
using project.Models;
using System.Data.Entity.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web.Security;
using System.Data.SqlClient;

namespace project.Controllers
{
    public class LoginController : Controller
    {
        private IUserRepository repository = null;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public LoginController()
        {
            this.repository = new UserRepository();

        }

        public LoginController(IUserRepository repository )
        {
            this.repository = repository;
            
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpPost]
        public ActionResult Autherize(project.Models.usertable userModel)
        {
            using (UserDataContext db = new UserDataContext())
            {
               
                var userDetails = db.usertables.Where(x => x.Username == userModel.Username && x.Password == userModel.Password ).FirstOrDefault();
               

                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Wrong username or password";
                    return View("Index", userModel);
                }


                else
                {
                    var roledetails = userDetails.Role;

                    if (roledetails == "Admin")
                    {
                        Session["userID"] = userDetails.userid;
                        Session["userName"] = userDetails.Username;
                        Session["firstname"] = userDetails.firstname;
                        Session["secondname"] = userDetails.secondname;
                        Session["DOB"] = userDetails.DOB;
                        Session["Address"] = userDetails.Address;
                        Session["Postcode"] = userDetails.Postcode;
                        Session["Telephone"] = userDetails.Telephone;
                        Session["Email"] = userDetails.Email;
                        Session["AdminIsLoggedIn"] = true;
                        return RedirectToAction("Index", "Portal");
                    }


                   else
                    {
                        Session["userID"] = userDetails.userid;
                        Session["userName"] = userDetails.Username;
                        Session["firstname"] = userDetails.firstname;
                        Session["secondname"] = userDetails.secondname;
                        Session["DOB"] = userDetails.DOB;
                        Session["Address"] = userDetails.Address;
                        Session["Postcode"] = userDetails.Postcode;
                        Session["Telephone"] = userDetails.Telephone;
                        Session["Email"] = userDetails.Email;
                        Session["AdminIsLoggedIn"] = false;
                        return RedirectToAction("Index", "Employee");
                    }

                }
            }
              
        }

        public ActionResult LogOut()
        {
            int userId = (int)Session["userID"];
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        // GET: Login
        public ActionResult Index()
        {
            if (Session["AdminIsLogedIn"] != null)
            {
                return RedirectToAction("Index", "Portal");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            usertable existing = repository.SelectByID(id);
            return View(existing);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        
        public  ActionResult Create(usertable obj)
        {
            if (ModelState.IsValid)
            { 
         

             // check valid state
                repository.Insert(obj);
                repository.Save();
                return RedirectToAction("Index");
            }

            else // not valid so redisplay
            {
                return View(obj);
            }

        }

        [HttpGet, ActionName("Edit")]
        public ActionResult ConfirmEdit(int id)
        {
            usertable existing = repository.SelectByID(id);
            return View(existing);
        }

        [HttpPost]
        public ActionResult Edit(usertable obj)
        {
            if (ModelState.IsValid)
            { // check valid state
                repository.Update(obj);
                repository.Save();
                return RedirectToAction("Index");
            }
            else // not valid so redisplay
            {
                return View(obj);
            }
        }

        [HttpGet, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            usertable existing = repository.SelectByID(id);
            return View(existing);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            repository.Delete(id);
            repository.Save();
            return RedirectToAction("Index");
        }
    }
}