using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project.Models.Repositories;
using project.Models;

namespace project.Controllers
{
    public class UserController : Controller
    {
        private INewUserRepository repository = null;

        public UserController()
        {
            this.repository = new NewUserRepository();
        }

        public UserController(INewUserRepository repository)
        {
            this.repository = repository;
        }


        // GET: User
        public ActionResult Index()
        {
            DBContext db = new DBContext();

            return View(db.usertables.Where(x => x.Role == "Employee").ToList());
            
            
            //List<usertable> model = (List<usertable>)repository.SelectAll();
               // return View(model); 
            
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

        [HttpPost]
        public ActionResult Create(usertable obj)
        {
         

            if (ModelState.IsValid)
            { // check valid state
                
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