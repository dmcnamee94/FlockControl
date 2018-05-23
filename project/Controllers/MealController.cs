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
    public class MealController : Controller
    {
        private IMealRepository repository = null;
        DBContext db = new DBContext();
        public MealController()
        {
            this.repository = new MealRepository();
        }

        public MealController(IMealRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult NewIndex(string sortOrder, string searchString, string currentFilter, int? page)
        {
            List<mealsupplier> SupplierList = db.mealsupplier.ToList();
            ViewBag.ListOfSupplier = new SelectList(SupplierList, "SupplierName", "SupplierName");
            mealproduct mealmodel = new mealproduct();

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.MealSuppierSortParm = String.IsNullOrEmpty(sortOrder) ? "MealSupplier" : "";
            ViewBag.BagSizeSortParm = String.IsNullOrEmpty(sortOrder) ? "BagSize" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            List<mealproduct> model = (List<mealproduct>)repository.SelectAll();
            var Meal = from s in db.mealproduct
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                int TagNo;
                if (int.TryParse(searchString, out TagNo))
                {

                    Meal = Meal.Where(s => s.Name.Equals(TagNo) || s.MealSupplier.Equals(TagNo) || (s.BagSize.Equals(searchString)) || (s.Price.Equals(searchString)));
                }
                else
                {
                    Meal = Meal.Where(s => s.Name.Contains(searchString) || (s.MealSupplier.Equals(searchString)) || (s.BagSize.Equals(searchString)) || (s.Price.Equals(searchString)));
                }

            }
            switch (sortOrder)
            {
                case "Name":
                    Meal = Meal.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    Meal = Meal.OrderBy(s => s.DateofPurchase);
                    break;
                case "date_desc":
                    Meal = Meal.OrderByDescending(s => s.DateofPurchase);
                    break;
                case "MealSupplier":
                    Meal = Meal.OrderByDescending(s => s.MealSupplier);
                    break;
                case "BagSize":
                    Meal = Meal.OrderByDescending(s => s.BagSize);
                    break;
                default:
                    Meal = Meal.OrderBy(s => s.BagSize);
                    break;
            }


            return View(Meal.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult GetEditMeal(int MealId)
        {

            List<mealsupplier> SupplierList = db.mealsupplier.ToList();
            ViewBag.ListOfSupplier = new SelectList(SupplierList, "SupplierName", "SupplierName");

            mealproduct model = new mealproduct();

            if (MealId > 0)
            {

                mealproduct emp = db.mealproduct.SingleOrDefault(x => x.MealID == MealId);
                model.MealID = emp.MealID;
                model.Name = emp.Name;
                model.DateofPurchase = emp.DateofPurchase;
                model.MealSupplier = emp.MealSupplier;
                model.BagSize = emp.BagSize;
                model.Price = emp.Price;
                model.Description = emp.Description;

            }
            return PartialView("EditMeal", model);
        }

        //createfornewmodal
        [HttpPost]
        public ActionResult AddMeal(mealproduct obj)
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
        public ActionResult AddMeal()
        {

            mealproduct mealmodel = new mealproduct();

            mealmodel.SupplierCollection = db.mealsupplier.ToList<mealsupplier>();
            List<mealsupplier> SupplierList = db.mealsupplier.ToList();
            ViewBag.ListOfSupplier = new SelectList(SupplierList, "SupplierName", "SupplierName");

            return View(mealmodel);
            //return PartialView("_AddSheep");
        }

        [HttpGet, ActionName("EditMeal")]
        public ActionResult ConfirmMealEdit(int id)
        {
            mealproduct existing = repository.SelectByID(id);
            List<mealsupplier> SupplierList = db.mealsupplier.ToList();
            ViewBag.ListOfSupplier = new SelectList(SupplierList, "SupplierID", "SupplierName");
            return View(existing);
        }

        [HttpPost]
        public ActionResult EditMeal(mealproduct obj)
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