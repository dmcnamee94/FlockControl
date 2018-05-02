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
    public class LambMedController : Controller
    {
        private Ilambmed repository = null;
        DBContext db = new DBContext();
        public LambMedController()
        {
            this.repository = new LambmedRepository();
        }

        public LambMedController(Ilambmed repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public ActionResult NewIndex(string sortOrder, string searchString, string currentFilter, int? page)
        {
            List<drug> DrugList = db.drug.ToList();
            ViewBag.ListOfBreed = new SelectList(DrugList, "Name", "Name");
            ViewBag.ListOfDosage = new SelectList("2ml", "3ml", "5ml", "100ml");
            List<lamb> LambList = db.lamb.ToList();
            ViewBag.ListOfLambs = new SelectList(LambList, "TagNo", "TagNo");

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TagNoSortParm = String.IsNullOrEmpty(sortOrder) ? "lambtag" : "";
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


            List<lambmed> Model = (List<lambmed>)repository.SelectAll();
            var Med = from s in db.lambmed
                      select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                int TagNo;
                if (int.TryParse(searchString, out TagNo))
                {

                    Med = Med.Where(s => s.TagNo == (TagNo));
                }
                else
                {
                    Med = Med.Where(s => s.Name.Contains(searchString) || (s.Issue.Contains(searchString)) || (s.Dosage.Contains(searchString)));
                }

            }
            switch (sortOrder)
            {
                case "lambtag":
                    Med = Med.OrderByDescending(s => s.TagNo);
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
                    Med = Med.OrderBy(s => s.TagNo);
                    break;
            }


            return View(Med.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult GetEditMedication(int MedicationId)
        {

            List<drug> DrugList = db.drug.ToList();
            ViewBag.ListOfBreed = new SelectList(DrugList, "Name", "Name");
            List<lamb> LambList = db.lamb.ToList();
            ViewBag.ListOfLambs = new SelectList(LambList, "TagNo", "TagNo");

            lambmed model = new lambmed();

            if (MedicationId > 0)
            {

                lambmed emp = db.lambmed.SingleOrDefault(x => x.MedID == MedicationId);
                model.MedID = emp.MedID;
                model.Date = emp.Date;
                model.Name = emp.Name;
                model.Issue = emp.Issue;
                model.TagNo = emp.TagNo;
                model.Dosage = emp.Dosage;


            }
            return PartialView("EditMedication", model);
        }

        [HttpPost]
        public ActionResult AddMedication(lambmed obj)
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

            lambmed medicationmodel = new lambmed();

            //sheepmodel.BreedCollection = db.breed.ToList<breed>();
            List<drug> DrugList = db.drug.ToList();
            ViewBag.ListOfBreed = new SelectList(DrugList, "Name", "Name");
            List<lamb> LambList = db.lamb.ToList();
            ViewBag.ListOfLambs = new SelectList(LambList, "TagNo", "TagNo");

            return View(medicationmodel);
            //return PartialView("_AddSheep");
        }

        [HttpGet, ActionName("EditMedication")]
        public ActionResult ConfirmMedicationEdit(int id)
        {
            lambmed existing = repository.SelectByID(id);
            List<drug> DrugList = db.drug.ToList();
            ViewBag.ListOfBreed = new SelectList(DrugList, "Name", "Name");
            List<lamb> LambList = db.lamb.ToList();
            ViewBag.ListOfLambs = new SelectList(LambList, "TagNo", "TagNo");
            return View(existing);
        }

        [HttpPost]
        public ActionResult EditMedication(lambmed obj)
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