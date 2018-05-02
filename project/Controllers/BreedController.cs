using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project.Models.Repositories;
using project.Models;
using PagedList;

namespace project.Controllers
{
    public class BreedController : Controller
    {

        private IBreedRepository repository = null;
        DBContext db = new DBContext();
        public BreedController()
        {
            this.repository = new BreedRepository();
        }

        public BreedController(IBreedRepository repository)
        {
            this.repository = repository;
        }


        [HttpGet]
        public ActionResult NewIndex(string sortOrder, string searchString, string currentFilter, int? page)
        {

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "breed" : "";
            ViewBag.ColourSortParm = String.IsNullOrEmpty(sortOrder) ? "Colour" : "";
            ViewBag.DescriptionSortParm = String.IsNullOrEmpty(sortOrder) ? "Description" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            List<breed> model = (List<breed>)repository.SelectAll();
            var Breed = from s in db.breed
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                int TagNo;
                if (int.TryParse(searchString, out TagNo))
                {

                    Breed = Breed.Where(s => s.Breed1.Equals(TagNo) );
                }
                else
                {
                    Breed = Breed.Where(s => s.Breed1.Contains(searchString) || (s.Colour.Contains(searchString)) || (s.Description.Contains(searchString)));
                }

            }

            switch (sortOrder)
            {
                case "breed":
                    Breed = Breed.OrderByDescending(s => s.Breed1);
                    break;
                case "Colour":
                    Breed = Breed.OrderBy(s => s.Colour);
                    break;
                case "Description":
                    Breed = Breed.OrderByDescending(s => s.Description);
                    break;
                default:
                    Breed = Breed.OrderBy(s => s.Breed1);
                    break;
            }


            return View(Breed.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult GetEditBreed(int BreedId)
        {
            breed model = new breed();

            if (BreedId > 0)
            {
                breed emp = db.breed.SingleOrDefault(x => x.BreedID == BreedId);
                model.BreedID = emp.BreedID;
                model.Breed1 = emp.Breed1;
                model.Colour = emp.Colour;
                model.Description = emp.Description;
            }
            return PartialView("EditBreed", model);
        }
        [HttpPost]
        public ActionResult AddBreed(breed obj)
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
        public ActionResult AddBreed()
        {
            breed breedmodel = new breed();
            return View(breedmodel);   
        }

        [HttpGet, ActionName("EditBreed")]
        public ActionResult ConfirmBreedEdit(int id)
        {
            breed existing = repository.SelectByID(id);
            return View(existing);
        }

        [HttpPost]
        public ActionResult EditBreed(breed obj)
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

        // GET: Breed
        public ActionResult Index()
        {
            List<breed> model = (List<breed>)repository.SelectAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
           breed existing = repository.SelectByID(id);
            return View(existing);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(breed obj)
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
            breed existing = repository.SelectByID(id);
            return View(existing);
        }

        [HttpPost]
        public ActionResult Edit(breed obj)
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
            breed existing = repository.SelectByID(id);
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