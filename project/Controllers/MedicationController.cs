using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project.Models.Repositories;
using project.Models;

namespace project.Controllers
{
    public class MedicationController : Controller
    {
        private IMedicationRepository repository = null;

        public MedicationController()
        {
            this.repository = new MedicationRepository();
        }

        public MedicationController(IMedicationRepository repository)
        {
            this.repository = repository;
        }

        // GET: Medication
        [HttpGet]
        public ActionResult Index( string searchBy, string search)
        {
            DBContext db = new DBContext();
            if (searchBy == "Issue")
            {
                return View(db.medications.Where(x => x.Issue.StartsWith(search)).ToList());

            }

            else if (searchBy == "TagNo")
            {

                try
                {
                    int TagNo = Convert.ToInt32(search);
                    return View(db.medications.Where(x => x.sheeptag == TagNo || search == null).ToList());
                }
                catch (FormatException )
                {
                    List<medication> Model = (List<medication>)repository.SelectAll();
                    return View(Model);
                }

            }

            else if (searchBy == "Name")
            {

                return View(db.medications.Where(x => x.Name.StartsWith(search)).ToList());

            }

            List<medication> model = (List<medication>)repository.SelectAll();
            return View(model);
        }

        public ActionResult EIndex(string searchBy, string search)
        {
            DBContext db = new DBContext();
            if (searchBy == "Issue")
            {
                return View(db.medications.Where(x => x.Issue.StartsWith(search)).ToList());

            }

            else if (searchBy == "TagNo")
            {

                try
                {
                    int TagNo = Convert.ToInt32(search);
                    return View(db.medications.Where(x => x.sheeptag == TagNo || search == null).ToList());
                }
                catch (FormatException)
                {
                    List<medication> Model = (List<medication>)repository.SelectAll();
                    return View(Model);
                }

            }

            else if (searchBy == "Name")
            {

                return View(db.medications.Where(x => x.Name.StartsWith(search)).ToList());

            }

            List<medication> model = (List<medication>)repository.SelectAll();
            return View(model);
        }
        [HttpGet]
        public ActionResult menu()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Emenu()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            medication existing = repository.SelectByID(id);
            return View(existing);
        }

        [HttpGet]
        public ActionResult EDetails(int id)
        {
            medication existing = repository.SelectByID(id);
            return View(existing);
        }
        [HttpGet]
        public ActionResult Create()
        {
            medication sheepmodel = new medication();
            using (DBContext DB = new DBContext())
            {
                sheepmodel.SheepCollection = DB.sheep.ToList<sheep>();
                sheepmodel.DrugCollection = DB.drug.ToList<drug>();
            }
            return View(sheepmodel);
        }

        [HttpPost]
        public ActionResult Create(medication obj)
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
            medication sheepmodel = new medication();
            using (DBContext DB = new DBContext())
            {
                sheepmodel.SheepCollection = DB.sheep.ToList<sheep>();
                sheepmodel.DrugCollection = DB.drug.ToList<drug>();
            }
            return View(sheepmodel);
        }

        [HttpPost]
        public ActionResult ECreate(medication obj)
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
            medication existing = repository.SelectByID(id);
            using (DBContext DB = new DBContext())
            {
                existing.SheepCollection = DB.sheep.ToList<sheep>();
                existing.DrugCollection = DB.drug.ToList<drug>();
            }

            return View(existing);
        }

        [HttpPost]
        public ActionResult Edit(medication obj)
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
            medication existing = repository.SelectByID(id);
            using (DBContext DB = new DBContext())
            {
                existing.SheepCollection = DB.sheep.ToList<sheep>();
                existing.DrugCollection = DB.drug.ToList<drug>();
            }

            return View(existing);
        }

        [HttpPost]
        public ActionResult EEdit(medication obj)
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
            medication existing = repository.SelectByID(id);
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