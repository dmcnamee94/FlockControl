using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project.Models.Repositories;
using project.Models;
using System.Web.Helpers;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using PagedList;
using System.Globalization;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace project.Controllers
{
    public class SheepController : Controller
    {
        private ISheepRepository repository = null;
        DBContext db = new DBContext();
        public SheepController()
        {
            this.repository = new SheepRepository();
        }

        public SheepController(ISheepRepository repository)
        {
            this.repository = repository;
        }
     

        JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
       

        [HttpPost]
        public JsonResult doessheeptagExist(int sheeptag)
        {

            DBContext db = new DBContext();

            return Json(!db.sheep.Any(sheep => sheep.sheeptag == sheeptag), JsonRequestBehavior.AllowGet);

        
        }
      
        public JsonResult GetSearchingData(string SearchBy, string SearchValue)
        {
            DBContext db = new DBContext();
            //List<sheep> SheepList = new List<sheep>();
            List<sheep> SheepList = (List<sheep>)repository.SelectAll();
            if (SearchBy == "TagNo")
            {
                try
                {
                    int TagNo = Convert.ToInt32(SearchValue);
                    SheepList = db.sheep.Where(x => x.sheeptag == TagNo || SearchValue == null).ToList();
                }
                catch(FormatException)
                {
                    Console.WriteLine("{0} Is Not A TagNo", SearchValue);
                }

                return Json(SheepList, JsonRequestBehavior.AllowGet);
            }

            else
            {
                SheepList = db.sheep.Where(x => x.Breed.StartsWith(SearchValue) || SearchValue == null).ToList();
                return Json(SheepList, JsonRequestBehavior.AllowGet);
            }

            
        }
        public ActionResult search(string searchString)
        {
            DBContext db = new DBContext();

            var sheep = from s in db.sheep
                        select s;

           

            if (!string.IsNullOrEmpty (searchString))
            {
                sheep = sheep.Where(s => s.Breed.Contains(searchString));
            }

            //int id = Convert.ToInt32(collection["txtsearchSheep"]);
           // var result = db.sheep.FirstOrDefault(m => m.TagNO == id);
            //return View(result);

            return View(sheep);

        }
       

        [HttpGet]
        public ActionResult NewIndex(string sortOrder, string searchString, string currentFilter, int? page)
        {
            List<breed> BreedList = db.breed.ToList();
            ViewBag.ListOfBreed = new SelectList(BreedList, "Breed1", "Breed1");
            ViewBag.ListOfYear = new SelectList("2017", "2018", "2016", "2015");
            createsheep sheepmodel = new createsheep();
            sheepmodel.BreedCollection = db.breed.ToList<breed>();
            

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "breed" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.TagNoSortParm = String.IsNullOrEmpty(sortOrder) ? "TagNo" : "";
            ViewBag.JoinYearSortParm = String.IsNullOrEmpty(sortOrder) ? "YearAdded" : "";
            if (searchString !=null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            List<sheep> model = (List<sheep>)repository.SelectAll();
            var Sheep = from s in db.sheep
                           select s;
            
            if (!String.IsNullOrEmpty(searchString))
            {
                int TagNo;
                if (int.TryParse(searchString, out TagNo))
                {
                  
                    Sheep = Sheep.Where(s => s.sheeptag.Equals(TagNo)|| s.yearadded.Equals(TagNo));
                }
                else
                {
                    Sheep = Sheep.Where(s => s.Breed.Contains(searchString)|| (s.detail.Equals(searchString)) || (s.sex.Equals(searchString)));
                }
            
            }
            switch (sortOrder)
            {
                case "breed":
                    Sheep = Sheep.OrderByDescending(s => s.Breed);
                    break;
                case "Date":
                    Sheep = Sheep.OrderBy(s => s.DOB);
                    break;
                case "date_desc":
                    Sheep = Sheep.OrderByDescending(s => s.DOB);
                    break;
                case "TagNo":
                    Sheep = Sheep.OrderByDescending(s => s.sheeptag);
                    break;
                case "YearAdded":
                    Sheep = Sheep.OrderByDescending(s => s.yearadded);
                    break;
                default:
                    Sheep = Sheep.OrderBy(s => s.sheeptag);
                    break;
            }

           
            return View(Sheep.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult NewEmployIndex(string sortOrder, string searchString, string currentFilter, int? page)
        {
            List<breed> BreedList = db.breed.ToList();
            ViewBag.ListOfBreed = new SelectList(BreedList, "Breed1", "Breed1");
            ViewBag.ListOfYear = new SelectList("2017", "2018", "2016", "2015");
            createsheep sheepmodel = new createsheep();
            sheepmodel.BreedCollection = db.breed.ToList<breed>();


            int pageSize = 6;
            int pageNumber = (page ?? 1);
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "breed" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.TagNoSortParm = String.IsNullOrEmpty(sortOrder) ? "TagNo" : "";
            ViewBag.JoinYearSortParm = String.IsNullOrEmpty(sortOrder) ? "YearAdded" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            List<sheep> model = (List<sheep>)repository.SelectAll();
            var Sheep = from s in db.sheep
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                int TagNo;
                if (int.TryParse(searchString, out TagNo))
                {

                    Sheep = Sheep.Where(s => s.sheeptag.Equals(TagNo) || s.yearadded.Equals(TagNo));
                }
                else
                {
                    Sheep = Sheep.Where(s => s.Breed.Contains(searchString) || (s.detail.Equals(searchString)) || (s.sex.Equals(searchString)));
                }

            }
            switch (sortOrder)
            {
                case "breed":
                    Sheep = Sheep.OrderByDescending(s => s.Breed);
                    break;
                case "Date":
                    Sheep = Sheep.OrderBy(s => s.DOB);
                    break;
                case "date_desc":
                    Sheep = Sheep.OrderByDescending(s => s.DOB);
                    break;
                case "TagNo":
                    Sheep = Sheep.OrderByDescending(s => s.sheeptag);
                    break;
                case "YearAdded":
                    Sheep = Sheep.OrderByDescending(s => s.yearadded);
                    break;
                default:
                    Sheep = Sheep.OrderBy(s => s.sheeptag);
                    break;
            }


            return View(Sheep.ToPagedList(pageNumber, pageSize));
        }
        public JsonResult GetSheepById(int SheepId)
        {
            createsheep sheepmodel = new createsheep();
            sheepmodel.BreedCollection = db.breed.ToList<breed>();
            sheep model = db.sheep.Where(x => x.SheepID == SheepId).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveDataInDatabase(createsheep model)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                try
                {

                    if (model.SheepID > 0)
                    {
                        sheep sh = db.sheep.SingleOrDefault(x => x.IsDeleted == false && x.SheepID == model.SheepID);
                        sh.sheeptag = model.sheeptag;
                        sh.DOB = model.DOB;
                        sh.Breed = model.Breed;
                        sh.detail = model.detail;
                        sh.sex = model.sex;
                        sh.yearadded = model.yearadded;
                        db.SaveChanges();
                        result = true;
                    }
                    else
                    {
                        sheep sh = new sheep();
                        sh.sheeptag = model.sheeptag;
                        sh.DOB = model.DOB;
                        sh.Breed = model.Breed;
                        sh.detail = model.detail;
                        sh.sex = model.sex;
                        sh.yearadded = model.yearadded;
                        db.sheep.Add(sh);
                        db.SaveChanges();
                        result = true;
                    }
                }


                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                RedirectToAction("NewIndex");
            }
            // catch (DbEntityValidationException dbEx)
            //{
            //  foreach (var validationErrors in dbEx.EntityValidationErrors)
            //{
            //  foreach (var validationError in validationErrors.ValidationErrors)
            //{
            //  Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
            //}
            //}
            // }
            return Json(result, JsonRequestBehavior.AllowGet);
            
        }

        [HttpGet]
        public ActionResult EmployeeIndex(string searchBy, string search)

        {


            DBContext db = new DBContext();
            if (searchBy == "Breed")
            {
                return View(db.sheep.Where(x => x.Breed.StartsWith(search)).ToList());

            }

            else if (searchBy == "TagNo")
            {

                try
                {
                    int TagNo = Convert.ToInt32(search);
                    return View(db.sheep.Where(x => x.sheeptag == TagNo || search == null).ToList());
                }
                catch (FormatException )
                {
                    List<sheep> Model = (List<sheep>)repository.SelectAll();
                    return View(Model);
                }

            }

            List<sheep> model = (List<sheep>)repository.SelectAll();
            return View(model);
        }

        // GET: Sheep

        [HttpGet]
        public ActionResult Index(string searchBy, string search)
            
        {
           
            
            DBContext db = new DBContext();
            if (searchBy == "Breed")
           {
                   return View(db.sheep.Where(x => x.Breed.StartsWith(search)).ToList());

           }

            else if(searchBy == "TagNo")
            {
                
                    try
                    {
                        int TagNo = Convert.ToInt32(search);
                        return View(db.sheep.Where(x => x.sheeptag == TagNo || search == null).ToList());
                    }
                    catch (FormatException )
                    {
                    List<sheep> Model = (List<sheep>)repository.SelectAll();
                    return View(Model);
                }
                
            }

                List<sheep> model = (List<sheep>)repository.SelectAll();
                return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            sheep existing = repository.SelectByID(id);
            return View(existing);
        }

        [HttpGet]
        public ActionResult DetailsE(int id)
        {
            sheep existing = repository.SelectByID(id);
            return View(existing);
        }
        public ActionResult GetEditSheep(int SheepId)
        {

            List<breed> BreedList = db.breed.ToList();
            ViewBag.ListOfBreed = new SelectList(BreedList, "Breed1", "Breed1");

            sheep model = new sheep();

            if (SheepId > 0)
            {

                sheep emp = db.sheep.SingleOrDefault(x => x.SheepID == SheepId);
                model.SheepID = emp.SheepID;
                model.Breed = emp.Breed;
                model.detail = emp.detail;
                model.DOB = emp.DOB;
                model.sheeptag = emp.sheeptag;
                model.sex = emp.sex;
                model.yearadded = emp.yearadded;

            }
            return PartialView("EditSheep", model);
        }
        //createfornewmodal
        [HttpPost]
        public ActionResult AddSheep(sheep obj)
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
        public ActionResult AddSheep()
        {

            createsheep sheepmodel = new createsheep();

            //sheepmodel.BreedCollection = db.breed.ToList<breed>();
            List<breed> BreedList = db.breed.ToList();
            ViewBag.ListOfBreed = new SelectList(BreedList, "Breed1", "Breed1");

            return View(sheepmodel);
            //return PartialView("_AddSheep");
        }

        //createfornewmodal
        [HttpPost]
        public ActionResult AddNewSheep(sheep obj)
        {
            if (ModelState.IsValid)
            {
                // check valid state
                repository.Insert(obj);
                repository.Save();
                return RedirectToAction("NewEmployIndex");
            }

            else // not valid so redisplay
            {
                return View();
                // return PartialView("_AddSheep");
            }
        }

        [HttpGet]
        public ActionResult AddNewSheep()
        {

            createsheep sheepmodel = new createsheep();

            //sheepmodel.BreedCollection = db.breed.ToList<breed>();
            List<breed> BreedList = db.breed.ToList();
            ViewBag.ListOfBreed = new SelectList(BreedList, "Breed1", "Breed1");

            return View(sheepmodel);
            //return PartialView("_AddSheep");
        }
        [HttpGet, ActionName("EditSheep")]
        public ActionResult ConfirmSheepEdit(int id)
        {
            sheep existing = repository.SelectByID(id);
            List<breed> BreedList = db.breed.ToList();
            ViewBag.ListOfBreed = new SelectList(BreedList, "Breed1", "Breed1");
            return View(existing);
        }

        [HttpPost]
        public ActionResult EditSheep(sheep obj)
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
        [HttpGet, ActionName("EditEmploySheep")]
        public ActionResult ConfirmSheepEmployEdit(int id)
        {
            sheep existing = repository.SelectByID(id);
            List<breed> BreedList = db.breed.ToList();
            ViewBag.ListOfBreed = new SelectList(BreedList, "Breed1", "Breed1");
            return View(existing);
        }

        [HttpPost]
        public ActionResult EditEmploySheep(sheep obj)
        {
            if (ModelState.IsValid)
            { // check valid state
                repository.Update(obj);
                repository.Save();
                return RedirectToAction("NewEmployIndex");
            }
            else // not valid so redisplay
            {
                //return View(obj);
                return RedirectToAction("NewEmployIndex");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {

            createsheep sheepmodel = new createsheep();
            using (DBContext DB = new DBContext())
            {
                sheepmodel.BreedCollection = DB.breed.ToList<breed>();
            }


            return View(sheepmodel);
        }

        [HttpPost]
       public ActionResult Create(sheep obj)
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
                        return View();
                    }
          }
        [HttpGet]
        public ActionResult EmployeeCreate()
        {

          createsheep sheepmodel = new createsheep();
          using (DBContext DB = new DBContext())
           {
               sheepmodel.BreedCollection = DB.breed.ToList<breed>();
           }


            return View(sheepmodel);
        }

       

        [HttpPost]
       public ActionResult EmployeeCreate(sheep obj)
        {
                    if (ModelState.IsValid)
                    {
                        // check valid state
                        repository.Insert(obj);
                        repository.Save();
                        return RedirectToAction("EmployeeIndex");
                    }

                   else // not valid so redisplay
                   {
                        return View();
                    }
          }

        [HttpGet, ActionName("Edit")]
        public ActionResult ConfirmEdit(int id)
        {
            sheep existing = repository.SelectByID(id);

            using (DBContext DB = new DBContext())
            {
                existing.BreedCollection = DB.breed.ToList<breed>();
                
            }
                return View(existing);
        }

        [HttpPost]
        public ActionResult Edit(sheep obj)
        {
            if (ModelState.IsValid)
            { // check valid state
                repository.Update(obj);
                repository.Save();
                return RedirectToAction("Index");
            }
            else // not valid so redisplay
            {
                //return View(obj);
                return RedirectToAction("Details");
            }
        }

        [HttpGet, ActionName("EditE")]
        public ActionResult ConfirmEditE(int id)
        {
            sheep existing = repository.SelectByID(id);

            using (DBContext DB = new DBContext())
            {
                existing.BreedCollection = DB.breed.ToList<breed>();

            }
            //return View(existing);
            return RedirectToAction("Details");
        }

        [HttpPost]
        public ActionResult EditE(sheep obj)
        {
            if (ModelState.IsValid)
            { // check valid state
                repository.Update(obj);
                repository.Save();
                return RedirectToAction("EmployeeIndex");
            }
            else // not valid so redisplay
            {
                return View(obj);
            }
        }

        [HttpGet, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            sheep existing = repository.SelectByID(id);
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