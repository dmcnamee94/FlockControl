using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project.Models.Repositories;
using project.Models;

namespace project.Controllers
{
    public class LambMedController : Controller
    {
        private Ilambmed repository = null;

        public LambMedController()
        {
            this.repository = new LambmedRepository();
        }

        public LambMedController(Ilambmed repository)
        {
            this.repository = repository;
        }

        // GET: LambMed
        [HttpGet]
        public ActionResult Index(string searchBy, string search)
        {
            DBContext db = new DBContext();
            if (searchBy == "Issue")
            {
                return View(db.lambmed.Where(x => x.Issue.StartsWith(search)).ToList());

            }

            else if (searchBy == "TagNo")
            {

                try
                {
                    int TagNo = Convert.ToInt32(search);
                    return View(db.lambmed.Where(x => x.TagNo == TagNo || search == null).ToList());
                }
                catch (FormatException )
                {
                    List<lambmed> Model = (List<lambmed>)repository.SelectAll();
                    return View(Model);
                }

            }

            else if (searchBy == "Name")
            {

                return View(db.lambmed.Where(x => x.Name.StartsWith(search)).ToList());

            }

            List<lambmed> model = (List<lambmed>)repository.SelectAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult EIndex(string searchBy, string search)
        {
            DBContext db = new DBContext();
            if (searchBy == "Issue")
            {
                return View(db.lambmed.Where(x => x.Issue.StartsWith(search)).ToList());

            }

            else if (searchBy == "TagNo")
            {

                try
                {
                    int TagNo = Convert.ToInt32(search);
                    return View(db.lambmed.Where(x => x.TagNo == TagNo || search == null).ToList());
                }
                catch (FormatException)
                {
                    List<lambmed> Model = (List<lambmed>)repository.SelectAll();
                    return View(Model);
                }

            }

            else if (searchBy == "Name")
            {

                return View(db.lambmed.Where(x => x.Name.StartsWith(search)).ToList());

            }

            List<lambmed> model = (List<lambmed>)repository.SelectAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            lambmed existing = repository.SelectByID(id);
            return View(existing);
        }

        [HttpGet]
        public ActionResult EDetails(int id)
        {
            lambmed existing = repository.SelectByID(id);
            return View(existing);
        }

        [HttpGet]
        public ActionResult Create()
        {
            lambmed sheepmodel = new lambmed();
            using (DBContext DB = new DBContext())
            {
                sheepmodel.LambCollection = DB.lamb.ToList<lamb>();
                sheepmodel.DrugCollection = DB.drug.ToList<drug>();
            }
            return View(sheepmodel);
        }

        [HttpPost]
        public ActionResult Create(lambmed obj)
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

        [HttpGet]
        public ActionResult ECreate()
        {
            lambmed sheepmodel = new lambmed();
            using (DBContext DB = new DBContext())
            {
                sheepmodel.LambCollection = DB.lamb.ToList<lamb>();
                sheepmodel.DrugCollection = DB.drug.ToList<drug>();
            }
            return View(sheepmodel);
        }

        [HttpPost]
        public ActionResult ECreate(lambmed obj)
        {
            if (ModelState.IsValid)
            { // check valid state
                repository.Insert(obj);
                repository.Save();
                return RedirectToAction("EIndex");
            }
            else // not valid so redisplay
            {
                return View(obj);
            }
        }

        [HttpGet, ActionName("Edit")]
        public ActionResult ConfirmEdit(int id)
        {
            lambmed existing = repository.SelectByID(id);
            using (DBContext DB = new DBContext())
            {
                existing.LambCollection = DB.lamb.ToList<lamb>();
                existing.DrugCollection = DB.drug.ToList<drug>();
            }
            return View(existing);
        }

        [HttpPost]
        public ActionResult Edit(lambmed obj)
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
        public ActionResult EConfirmEdit(int id)
        {
            lambmed existing = repository.SelectByID(id);
            using (DBContext DB = new DBContext())
            {
                existing.LambCollection = DB.lamb.ToList<lamb>();
                existing.DrugCollection = DB.drug.ToList<drug>();
            }
            return View(existing);
        }

        [HttpPost]
        public ActionResult EEdit(lambmed obj)
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

        [HttpGet, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            lambmed existing = repository.SelectByID(id);
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