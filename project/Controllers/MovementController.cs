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
    public class MovementController : Controller
    {
        private IMovementRepository repository = null;
        DBContext db = new DBContext();
        public MovementController()
        {
            this.repository = new MovementRepository();
        }

        public MovementController(IMovementRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public ActionResult NewIndex(string sortOrder, string searchString, string currentFilter, int? page)
        {
            List<_event> EventList = db._event.ToList();
            ViewBag.ListOfEvent = new SelectList(EventList, "Eventcode", "Eventcode");
            movement movmodel = new movement();

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            ViewBag.CurrentSort = sortOrder;
            ViewBag.RefSortParm = String.IsNullOrEmpty(sortOrder) ? "Ref" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.StockSortParm = String.IsNullOrEmpty(sortOrder) ? "Stock" : "";
            ViewBag.NoSortParm = String.IsNullOrEmpty(sortOrder) ? "No" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            List<movement> model = (List<movement>)repository.SelectAll();
            var Move = from s in db.movement
                       select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                int TagNo;
                if (int.TryParse(searchString, out TagNo))
                {

                    Move = Move.Where(s => s.NoAnimals.Equals(TagNo)|| (s.RefNo.Equals(TagNo)));
                }
                else
                {
                    Move = Move.Where(s => s.Description.Contains(searchString) || (s.Eventcode.Equals(searchString)));
                }

            }
            switch (sortOrder)
            {
                case "Ref":
                    Move = Move.OrderByDescending(s => s.RefNo);
                    break;
                case "Date":
                    Move = Move.OrderBy(s => s.Date);
                    break;
                case "date_desc":
                    Move = Move.OrderByDescending(s => s.Date);
                    break;
                case "Stock":
                    Move = Move.OrderByDescending(s => s.Description);
                    break;
                case "No":
                    Move = Move.OrderByDescending(s => s.NoAnimals);
                    break;
                default:
                    Move = Move.OrderBy(s => s.RefNo);
                    break;
            }


            return View(Move.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult GetEditMovement(int MovementId)
        {
            List<_event> EventList = db._event.ToList();
            ViewBag.ListOfEvent = new SelectList(EventList, "Eventcode", "Eventcode");
            movement model = new movement();

            if (MovementId > 0)
            {

                movement emp = db.movement.SingleOrDefault(x => x.MovementID == MovementId);
                model.MovementID = emp.MovementID;
                model.RefNo = emp.RefNo;
                model.Date = emp.Date;
                model.Description = emp.Description;
                model.NoAnimals = emp.NoAnimals;
                model.Eventcode = emp.Eventcode;
               

            }
            return PartialView("EditMovement", model);
        }

        //createfornewmodal
        [HttpPost]
        public ActionResult AddMovement(movement obj)
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
        public ActionResult AddMovement()
        {

            addmovement movemodel = new addmovement();
            List<_event> EventList = db._event.ToList();
            ViewBag.ListOfEvent = new SelectList(EventList, "Eventcode", "Eventcode");
            return View(movemodel);
            //return PartialView("_AddSheep");
        }

        [HttpGet, ActionName("EditMovement")]
        public ActionResult ConfirmMovementEdit(int id)
        {
            movement existing = repository.SelectByID(id);
            List<_event> EventList = db._event.ToList();
            ViewBag.ListOfEvent = new SelectList(EventList, "Eventcode", "Eventcode");
            return View(existing);
        }

        [HttpPost]
        public ActionResult EditMovement(movement obj)
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

        [HttpPost]
        public JsonResult doesrefnoExist(int refno)
        {

            DBContext db = new DBContext();

            return Json(!db.movement.Any(movement => movement.RefNo == refno), JsonRequestBehavior.AllowGet);


        }
        // GET: movement
        public ActionResult Index()
        {
            List<movement> model = (List<movement>)repository.SelectAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            movement existing = repository.SelectByID(id);
            return View(existing);
        }

        [HttpGet]
        public ActionResult Create()
        {

            addmovement eventmodel = new addmovement();
            using (DBContext DB = new DBContext())
            {
               eventmodel.EventCollection = DB._event.ToList<_event>();
            }


            return View(eventmodel);
        }

        [HttpPost]
        public ActionResult Create(movement obj)
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
            movement existing = repository.SelectByID(id);
            using (DBContext DB = new DBContext())
            {
                existing.EventCollection = DB._event.ToList<_event>();
            }

            return View(existing);
        }

        [HttpPost]
        public ActionResult Edit(movement obj)
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
            movement existing = repository.SelectByID(id);
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