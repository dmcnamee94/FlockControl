using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project.Models.Repositories;
using project.Models;

namespace project.Controllers
{
    public class MealController : Controller
    {
        private IMealRepository repository = null;
        public MealController()
        {
            this.repository = new MealRepository();
        }

        public MealController(IMealRepository repository)
        {
            this.repository = repository;
        }

        // GET: Meal
        public ActionResult Index()
        {
            List<mealproduct> model = (List<mealproduct>)repository.SelectAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            mealproduct existing = repository.SelectByID(id);
            return View(existing);
        }

        [HttpGet]
        public ActionResult Create()
        {
            mealproduct suplymodel = new mealproduct();
            using (DBContext DB = new DBContext())
            {
                suplymodel.SupplierCollection = DB.mealsupplier.ToList<mealsupplier>();
            }
            return View(suplymodel);
        }

        [HttpPost]
        public ActionResult Create(mealproduct obj)
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
            mealproduct existing = repository.SelectByID(id);
            using (DBContext DB = new DBContext())
            {
                existing.SupplierCollection = DB.mealsupplier.ToList<mealsupplier>();
            }

            return View(existing);
        }

        [HttpPost]
        public ActionResult Edit(mealproduct obj)
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
            mealproduct existing = repository.SelectByID(id);
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