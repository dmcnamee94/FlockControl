using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project.Models.Repositories;
using project.Models;

namespace project.Controllers
{
    public class ManageController : Controller
    {
        private INewUserRepository repository = null;

        public ManageController()
        {
            this.repository = new NewUserRepository();
        }

        public ManageController(INewUserRepository repository)
        {
            this.repository = repository;
        }


        // GET: Manage
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult EIndex()
        {

            return View();
        }


        [HttpGet, ActionName("Edit")]
        public ActionResult ConfirmEdit()
        {
            int userId = (int)Session["userID"];
            usertable existing = repository.SelectByID(userId);
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

        [HttpGet, ActionName("EEdit")]
        public ActionResult EConfirmEdit()
        {
            int userId = (int)Session["userID"];
            usertable existing = repository.SelectByID(userId);
            return View(existing);
        }

        [HttpPost]
        public ActionResult EEdit(usertable obj)
        {
            if (ModelState.IsValid)
            { // check valid state
                repository.Update(obj);
                repository.Save();
                return RedirectToAction("EIndex");
            }
            else // not valid so redisplay
            {
                return View(obj);
            }
        }
    }
}