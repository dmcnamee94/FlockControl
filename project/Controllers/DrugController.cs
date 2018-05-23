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
    public class DrugController : Controller
    {

        private IDrugRepository repository = null;
        DBContext db = new DBContext();
        public DrugController()
        {
            this.repository = new DrugRepository();
        }

        public DrugController(IDrugRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult NewIndex(string sortOrder, string searchString, string currentFilter, int? page)
        {
            List<drugsupplier> SupplierList = db.drugsupplier.ToList();
            ViewBag.ListOfSupplier = new SelectList(SupplierList, "SupplierName", "SupplierName");
            mealproduct mealmodel = new mealproduct();

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.DrugSuppierSortParm = String.IsNullOrEmpty(sortOrder) ? "DrugSupplier" : "";
            ViewBag.PriceSortParm = String.IsNullOrEmpty(sortOrder) ? "Price" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            List<drug> model = (List<drug>)repository.SelectAll();
            var Drug = from s in db.drug
                       select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                int TagNo;
                if (int.TryParse(searchString, out TagNo))
                {

                    Drug = Drug.Where(s => s.Name.Equals(TagNo) || s.DrugSupplier.Equals(TagNo) || (s.Price.Equals(searchString)) );
                }
                else
                {
                    Drug = Drug.Where(s => s.Name.Contains(searchString) || (s.DrugSupplier.Equals(searchString)) || (s.Price.Equals(searchString)));
                }

            }
            switch (sortOrder)
            {
                case "Name":
                    Drug = Drug.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    Drug = Drug.OrderBy(s => s.PurchaseDate);
                    break;
                case "date_desc":
                    Drug = Drug.OrderByDescending(s => s.PurchaseDate);
                    break;
                case "DrugSupplier":
                    Drug = Drug.OrderByDescending(s => s.DrugSupplier);
                    break;
                case "Price":
                    Drug = Drug.OrderByDescending(s => s.Price);
                    break;
                default:
                    Drug = Drug.OrderBy(s => s.PurchaseDate);
                    break;
            }


            return View(Drug.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult GetEditDrug(int DrugId)
        {

            List<drugsupplier> SupplierList = db.drugsupplier.ToList();
            ViewBag.ListOfSupplier = new SelectList(SupplierList, "SupplierName", "SupplierName");

            drug model = new drug();

            if (DrugId > 0)
            {

                drug emp = db.drug.SingleOrDefault(x => x.DrugID == DrugId);
                model.DrugID = emp.DrugID;
                model.Name = emp.Name;
                model.PurchaseDate = emp.PurchaseDate;
                model.DrugSupplier = emp.DrugSupplier;
                model.Withdrawalperiod = emp.Withdrawalperiod;
                model.Price = emp.Price;
                model.Dateofdisposal = emp.Dateofdisposal;
                model.BottleSize = emp.BottleSize;
                model.IsDisposedOf = emp.IsDisposedOf;
            }
            return PartialView("EditDrug", model);
        }

        //createfornewmodal
        [HttpPost]
        public ActionResult AddDrug(drug obj)
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
        public ActionResult AddDrug()
        {

            drug drugmodel = new drug();
            List<drugsupplier> SupplierList = db.drugsupplier.ToList();
            ViewBag.ListOfSupplier = new SelectList(SupplierList, "SupplierName", "SupplierName");

            return View(drugmodel);
            //return PartialView("_AddSheep");
        }

        [HttpGet, ActionName("EditDrug")]
        public ActionResult ConfirmDrugEdit(int id)
        {
            drug existing = repository.SelectByID(id);
            List<drugsupplier> SupplierList = db.drugsupplier.ToList();
            ViewBag.ListOfSupplier = new SelectList(SupplierList, "SupplierID", "SupplierName");
            return View(existing);
        }

        [HttpPost]
        public ActionResult EditDrug(drug obj)
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