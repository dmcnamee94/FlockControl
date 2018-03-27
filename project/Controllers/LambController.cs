using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project.Models.Repositories;
using project.Models;

namespace project.Controllers
{
    public class LambController : Controller
    {
        private ILambRepository repository = null;

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