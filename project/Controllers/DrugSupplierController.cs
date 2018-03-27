using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project.Models.Repositories;
using project.Models;

namespace project.Controllers
{
    public class DrugSupplierController : Controller
    {
        private IDrugSupplierRepository repository = null;
        public DrugSupplierController()
        {
            this.repository = new DrugSupplierRepository();
        }

        public DrugSupplierController(IDrugSupplierRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Suppliers()
        {
            return View();
        }
        // GET: DrugSupplier
        public ActionResult Index()
        {
            List < drugsupplier > model = (List<drugsupplier>)repository.SelectAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            drugsupplier existing = repository.SelectByID(id);
            return View(existing);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(drugsupplier obj)
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
            drugsupplier existing = repository.SelectByID(id);
            return View(existing);
        }

        [HttpPost]
        public ActionResult Edit(drugsupplier obj)
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
            drugsupplier existing = repository.SelectByID(id);
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