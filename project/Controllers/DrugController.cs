using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project.Models.Repositories;
using project.Models;

namespace project.Controllers
{
    public class DrugController : Controller
    {

        private IDrugRepository repository = null;
        public DrugController()
        {
            this.repository = new DrugRepository();
        }

        public DrugController(IDrugRepository repository)
        {
            this.repository = repository;
        }

        // GET: Drug
        public ActionResult Index()
        {
            List<drug> model = (List<drug>)repository.SelectAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            drug existing = repository.SelectByID(id);
            return View(existing);
        }

        [HttpGet]
        public ActionResult Create()
        {
            drug drugmodel = new drug();
            using (DBContext DB = new DBContext())
            {
                drugmodel.DrugSupplierCollection = DB.drugsupplier.ToList<drugsupplier>();
            }
            return View(drugmodel);
        }

        [HttpPost]
        public ActionResult Create(drug obj)
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
            drug existing = repository.SelectByID(id);

            using (DBContext DB = new DBContext())
            {
                existing.DrugSupplierCollection = DB.drugsupplier.ToList<drugsupplier>();
            }

            return View(existing);
        }

        [HttpPost]
        public ActionResult Edit(drug obj)
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
            drug existing = repository.SelectByID(id);
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