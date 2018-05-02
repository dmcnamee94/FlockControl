using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project.Models.Repositories;
using project.Models;
using PagedList;

namespace project.Controllers
{
    public class MedicationController : Controller
    {
        private IMedicationRepository repository = null;
        DBContext db = new DBContext();
        public MedicationController()
        {
            this.repository = new MedicationRepository();
        }

        public MedicationController(IMedicationRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult NewIndex(string sortOrder, string searchString, string currentFilter, int? page)
        {
            List<drug> DrugList = db.drug.ToList();
            ViewBag.ListOfBreed = new SelectList(DrugList, "Name", "Name");
            ViewBag.ListOfDosage = new SelectList("2ml","3ml","5ml","100ml");
            List<sheep> SheepList = db.sheep.ToList();
            ViewBag.ListOfSheep = new SelectList(SheepList, "sheeptag", "sheeptag");

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TagNoSortParm = String.IsNullOrEmpty(sortOrder) ? "sheeptag" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.DrugSortParm = String.IsNullOrEmpty(sortOrder) ? "DrugGiven" : "";
            ViewBag.IssueSortParm = String.IsNullOrEmpty(sortOrder) ? "Issue" : "";
            ViewBag.DosageSortParm = String.IsNullOrEmpty(sortOrder) ? "Dosage" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            List<medication> Model = (List<medication>)repository.SelectAll();
            var Med = from s in db.medications
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                int TagNo;
                if (int.TryParse(searchString, out TagNo))
                {

                    Med = Med.Where(s => s.sheeptag == (TagNo));
                }
                else
                {
                    Med = Med.Where(s => s.Name.Contains(searchString) || (s.Issue.Contains(searchString)) || (s.Dosage.Contains(searchString)));
                }

            }
            switch (sortOrder)
            {
                case "sheeptag":
                    Med = Med.OrderByDescending(s => s.sheeptag);
                    break;
                case "Date":
                    Med = Med.OrderBy(s => s.Date);
                    break;
                case "date_desc":
                    Med = Med.OrderByDescending(s => s.Date);
                    break;
                case "DrugGiven":
                    Med = Med.OrderByDescending(s => s.Name);
                    break;
                case "Issue":
                    Med = Med.OrderByDescending(s => s.Issue);
                    break;
                case "Dosage":
                    Med = Med.OrderByDescending(s => s.Dosage);
                    break;
                default:
                    Med = Med.OrderBy(s => s.sheeptag);
                    break;
            }


            return View(Med.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult GetEditMedication(int MedicationId)
        {

            List<drug> DrugList = db.drug.ToList();
            ViewBag.ListOfBreed = new SelectList(DrugList, "Name", "Name");
            List<sheep> SheepList = db.sheep.ToList();
            ViewBag.ListOfSheep = new SelectList(SheepList, "sheeptag", "sheeptag");

            medication model = new medication();

            if (MedicationId > 0)
            {

                medication emp = db.medications.SingleOrDefault(x => x.MedicationID == MedicationId);
                model.MedicationID = emp.MedicationID;
                model.Date = emp.Date;
                model.Name = emp.Name;
                model.Issue = emp.Issue;
                model.sheeptag = emp.sheeptag;
                model.Dosage = emp.Dosage;
                

            }
            return PartialView("EditMedication", model);
        }

        [HttpPost]
        public ActionResult AddMedication(medication obj)
        {
            if (ModelState.IsValid)
            {
                // check valid state
                repository.Insert(obj);
                repository.Save();
                return RedirectToAction("NewIndex");
            }

            else // not valid so redisplay
            {
                return View();
                // return PartialView("_AddSheep");
            }
        }

        [HttpGet]
        public ActionResult AddMedication()
        {

            medication medicationmodel = new medication();

            //sheepmodel.BreedCollection = db.breed.ToList<breed>();
            List<drug> DrugList = db.drug.ToList();
            ViewBag.ListOfBreed = new SelectList(DrugList, "Name", "Name");
            List<sheep> SheepList = db.sheep.ToList();
            ViewBag.ListOfSheep = new SelectList(SheepList, "sheeptag", "sheeptag");

            return View(medicationmodel);
            //return PartialView("_AddSheep");
        }

        [HttpGet, ActionName("EditMedication")]
        public ActionResult ConfirmMedicationEdit(int id)
        {
            medication existing = repository.SelectByID(id);
            List<drug> DrugList = db.drug.ToList();
            ViewBag.ListOfBreed = new SelectList(DrugList, "Name", "Name");
            List<sheep> SheepList = db.sheep.ToList();
            ViewBag.ListOfSheep = new SelectList(SheepList, "sheeptag", "sheeptag");
            return View(existing);
        }

        [HttpPost]
        public ActionResult EditMedication(medication obj)
        {
            if (ModelState.IsValid)
            { // check valid state
                repository.Update(obj);
                repository.Save();
                return RedirectToAction("NewIndex");
            }
            else // not valid so redisplay
            {
                //return View(obj);
                return RedirectToAction("NewIndex");
            }
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