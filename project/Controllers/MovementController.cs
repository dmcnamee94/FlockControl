using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project.Models.Repositories;
using project.Models;

namespace project.Controllers
{
    public class MovementController : Controller
    {
        private IMovementRepository repository = null;
        public MovementController()
        {
            this.repository = new MovementRepository();
        }

        public MovementController(IMovementRepository repository)
        {
            this.repository = repository;
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