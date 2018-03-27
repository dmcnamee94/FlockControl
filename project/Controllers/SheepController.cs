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

namespace project.Controllers
{
    public class SheepController : Controller
    {
        private ISheepRepository repository = null;

        public SheepController()
        {
            this.repository = new SheepRepository();
        }

        public SheepController(ISheepRepository repository)
        {
            this.repository = repository;
        }

        JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
        public ActionResult TIndex()
        {
            string query = "SELECT Breed, COUNT(SheepID) TotalSheep";
            query += " FROM Sheep GROUP BY Breed";
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            List<sheep> chartData = new List<sheep>();

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            chartData.Add(new sheep
                            {
                                Breed = sdr["Breed"].ToString(),
                                TotalSheep = Convert.ToInt32(sdr["TotalSheep"])
                            });
                        }
                    }

                    con.Close();
                }

                return View(chartData);
            }
        }

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

           // else if(SearchBy == "Age")
           // {
             //   try
               // {
                 //   int Age = Convert.ToInt32(SearchValue);
                   // SheepList = db.sheep.Where(x => x.Age == Age || SearchValue == null).ToList();
                //}
                //catch (FormatException)
               // {
                 //   Console.WriteLine("{0} Is Not An Age", SearchValue);
                //}

                //return Json(SheepList, JsonRequestBehavior.AllowGet);
            //}
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
                return View(obj);
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
            return View(existing);
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