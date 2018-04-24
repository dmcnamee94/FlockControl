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
    public class LambController : Controller
    {
        private ILambRepository repository = null;
        DBContext db = new DBContext();
        public LambController()
        {
            this.repository = new LambRepository();
        }

        public LambController(ILambRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public JsonResult doesExist(int TagNo)
        {

           DBContext db = new DBContext();

            return Json(!db.lamb.Any(lamb => lamb.TagNo == TagNo), JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult NewIndex(string sortOrder, string searchString, string currentFilter, int? page)
        {
            List<breed> BreedList = db.breed.ToList();
            ViewBag.ListOfBreed = new SelectList(BreedList, "Breed1", "Breed1");
            List<sheep> SheepList = db.sheep.ToList();
            ViewBag.ListOfSheep = new SelectList(SheepList, "sheeptag", "sheeptag");
            List<result> ResultList = db.result.ToList();
            ViewBag.ListOfResult = new SelectList(ResultList, "Description", "Description");
            ViewBag.ListOfYear = new SelectList("2017", "2018", "2016", "2015");
            createlamb lambmodel = new createlamb();
            lambmodel.BreedCollection = db.breed.ToList<breed>();


            int pageSize = 6;
            int pageNumber = (page ?? 1);
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "breed" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.MotherEweSortParm = String.IsNullOrEmpty(sortOrder) ? "MotherEwe" : "";
            ViewBag.TagNoSortParm = String.IsNullOrEmpty(sortOrder) ? "TagNo" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            List<lamb> model = (List<lamb>)repository.SelectAll();
            var Lamb = from s in db.lamb
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                int TagNo;
                if (int.TryParse(searchString, out TagNo))
                {

                    Lamb = Lamb.Where(s => s.sheeptag.Equals(TagNo) || s.TagNo.Equals(TagNo));
                }
                else
                {
                    Lamb = Lamb.Where(s => s.Breed.Contains(searchString) || (s.Sex.Equals(searchString)));
                }

            }
            switch (sortOrder)
            {
                case "breed":
                    Lamb = Lamb.OrderByDescending(s => s.Breed);
                    break;
                case "Date":
                    Lamb = Lamb.OrderBy(s => s.DOB);
                    break;
                case "date_desc":
                    Lamb = Lamb.OrderByDescending(s => s.DOB);
                    break;
                case "MotherEwe":
                    Lamb = Lamb.OrderByDescending(s => s.sheeptag);
                    break;
                case "TagNo":
                    Lamb = Lamb.OrderByDescending(s => s.TagNo);
                    break;
                default:
                    Lamb = Lamb.OrderBy(s => s.TagNo);
                    break;
            }


            return View(Lamb.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult GetEditLamb(int LambId)
        {

            List<breed> BreedList = db.breed.ToList();
            ViewBag.ListOfBreed = new SelectList(BreedList, "Breed1", "Breed1");
            List<sheep> SheepList = db.sheep.ToList();
            ViewBag.ListOfSheep = new SelectList(SheepList, "sheeptag", "sheeptag");
            List<result> ResultList = db.result.ToList();
            ViewBag.ListOfResult = new SelectList(ResultList, "Description", "Description");

            lamb model = new lamb();

            if (LambId > 0)
            {

                lamb emp = db.lamb.SingleOrDefault(x => x.LambID == LambId);
                model.LambID = emp.LambID;
                model.Breed = emp.Breed;
                model.TagNo = emp.TagNo;
                model.DOB = emp.DOB;
                model.sheeptag = emp.sheeptag;
                model.Sex = emp.Sex;
                model.ResultDes = emp.ResultDes;
                model.FinishWeight = emp.FinishWeight;

            }
            return PartialView("EditLamb", model);
        }
        //createfornewmodal
        [HttpPost]
        public ActionResult AddLamb(lamb obj)
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
        public ActionResult AddLamb()
        {

            createlamb lambmodel = new createlamb();

            //sheepmodel.BreedCollection = db.breed.ToList<breed>();
            List<breed> BreedList = db.breed.ToList();
            ViewBag.ListOfBreed = new SelectList(BreedList, "Breed1", "Breed1");
            List<sheep> SheepList = db.sheep.ToList();
            ViewBag.ListOfSheep = new SelectList(SheepList, "sheeptag", "sheeptag");
            List<result> ResultList = db.result.ToList();
            ViewBag.ListOfResult = new SelectList(ResultList, "Description", "Description");

            return View(lambmodel);
         
        }

        [HttpGet, ActionName("EditLamb")]
        public ActionResult ConfirmLambEdit(int id)
        {
            lamb existing = repository.SelectByID(id);
            List<breed> BreedList = db.breed.ToList();
            ViewBag.ListOfBreed = new SelectList(BreedList, "Breed1", "Breed1");
            List<sheep> SheepList = db.sheep.ToList();
            ViewBag.ListOfSheep = new SelectList(SheepList, "sheeptag", "sheeptag");
            List<result> ResultList = db.result.ToList();
            ViewBag.ListOfResult = new SelectList(ResultList, "Description", "Description");
            return View(existing);
        }

        [HttpPost]
        public ActionResult EditLamb(lamb obj)
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

        // GET: result
        [HttpGet]
        public ActionResult Index( string searchBy, string search)
        {
            DBContext db = new DBContext();
            if (searchBy == "Breed")
            {
                return View(db.lamb.Where(x => x.Breed.StartsWith(search)).ToList());

            }

            else if (searchBy == "TagNo")
            {

                try
                {
                    int TagNo = Convert.ToInt32(search);
                    return View(db.lamb.Where(x => x.TagNo == TagNo || search == null).ToList());
                }
                catch (FormatException )
                {
                    List<lamb> Model = (List<lamb>)repository.SelectAll();
                    return View(Model);
                }

            }

            else if (searchBy == "sheeptag")
            {

                try
                {
                    int sheept = Convert.ToInt32(search);
                    return View(db.lamb.Where(x => x.sheeptag == sheept || search == null).ToList());
                }
                catch (FormatException )
                {
                    List<lamb> Model = (List<lamb>)repository.SelectAll();
                    return View(Model);
                }

            }

            List<lamb> model = (List<lamb>)repository.SelectAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult EIndex(string searchBy, string search)
        {
            DBContext db = new DBContext();
            if (searchBy == "Breed")
            {
                return View(db.lamb.Where(x => x.Breed.StartsWith(search)).ToList());

            }

            else if (searchBy == "TagNo")
            {

                try
                {
                    int TagNo = Convert.ToInt32(search);
                    return View(db.lamb.Where(x => x.TagNo == TagNo || search == null).ToList());
                }
                catch (FormatException)
                {
                    List<lamb> Model = (List<lamb>)repository.SelectAll();
                    return View(Model);
                }

            }

            else if (searchBy == "sheeptag")
            {

                try
                {
                    int sheept = Convert.ToInt32(search);
                    return View(db.lamb.Where(x => x.sheeptag == sheept || search == null).ToList());
                }
                catch (FormatException)
                {
                    List<lamb> Model = (List<lamb>)repository.SelectAll();
                    return View(Model);
                }

            }

            List<lamb> model = (List<lamb>)repository.SelectAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            lamb existing = repository.SelectByID(id);
            return View(existing);
        }

        [HttpGet]
        public ActionResult EDetails(int id)
        {
            lamb existing = repository.SelectByID(id);
            return View(existing);
        }

        [HttpGet]
        public ActionResult Create()
        {
            createlamb lambmodel = new createlamb();
            using (DBContext DB = new DBContext())
            {
                lambmodel.BreedCollection = DB.breed.ToList<breed>();
                lambmodel.SheepCollection = DB.sheep.ToList<sheep>();
                lambmodel.ResultCollection = DB.result.ToList<result>();
            }

            return View(lambmodel);
        }

        [HttpPost]
        public ActionResult Create(lamb obj)
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
            createlamb lambmodel = new createlamb();
            using (DBContext DB = new DBContext())
            {
                lambmodel.BreedCollection = DB.breed.ToList<breed>();
                lambmodel.SheepCollection = DB.sheep.ToList<sheep>();
                lambmodel.ResultCollection = DB.result.ToList<result>();
            }

            return View(lambmodel);
        }

        [HttpPost]
        public ActionResult ECreate(lamb obj)
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
            lamb existing = repository.SelectByID(id);

           
            using (DBContext DB = new DBContext())
            {
              
                existing.BreedCollection = DB.breed.ToList<breed>();
                existing.SheepCollection = DB.sheep.ToList<sheep>();
                existing.ResultCollection = DB.result.ToList<result>();
            }

           
            return View(existing);
        }

        [HttpPost]
        public ActionResult Edit(lamb obj)
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
            lamb existing = repository.SelectByID(id);


            using (DBContext DB = new DBContext())
            {

                existing.BreedCollection = DB.breed.ToList<breed>();
                existing.SheepCollection = DB.sheep.ToList<sheep>();
                existing.ResultCollection = DB.result.ToList<result>();
            }


            return View(existing);
        }

        [HttpPost]
        public ActionResult EEdit(lamb obj)
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
            lamb existing = repository.SelectByID(id);
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