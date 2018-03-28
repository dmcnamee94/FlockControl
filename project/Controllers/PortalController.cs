using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using project.Models;
using project;
using System.Web.Helpers;

namespace project.Controllers
{
    
    public class PortalController : Controller
    {
        DBContext db = new DBContext();
        JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
        // GET: Portal
        [Filters.AutorizeAdmin]
        public ActionResult Index()
        {
          
            return View();
            
        }

       
        public ActionResult SIndex()
        {
            // DBContext db = new DBContext();
            // return View(db.sheep.ToList());

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
            }

            return View(chartData);

        }

      
        public ActionResult LIndex()
        {
            // DBContext db = new DBContext();
            // return View(db.sheep.ToList());
            string query = "SELECT year(DOB) as year_of_birth, count(LambID) TotalLambs from dbo.lamb group by year(DOB)";
            
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            List<lamb> chartData = new List<lamb>();

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
                            chartData.Add(new lamb
                            {
                                
                               Year = Convert.ToInt32(sdr["year_of_birth"].ToString()),
                                TotalLambs = Convert.ToInt32(sdr["TotalLambs"])
                            });
                        }
                    }

                    con.Close();
                }
            }

            return View(chartData);

        }

        public ActionResult SheepChart()
        {
            // DBContext db = new DBContext();
            // return View(db.sheep.ToList());
            string query = "SELECT yearadded,sum(case when detail='Bred' then 1 else 0 end)*100/count(*) as Bred, sum(case when detail = 'Bought' then 1 else 0 end) * 100 / count(*) as Bought from dbo.sheep group by yearadded";

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

                                yearadded = Convert.ToInt32(sdr["yearadded"].ToString()),
                                detail = sdr["Bred"].ToString(),
                                sheepdetail = sdr["Bought"].ToString()
                            });
                        }
                    }

                    con.Close();
                }
            }

            return View(chartData);

        }

        public ActionResult FinishWeightChart()
        {
            // DBContext db = new DBContext();
            // return View(db.sheep.ToList());
            string query = "select year(DOB) as year_of_birth, avg(finishweight) Weight from lamb where year(DOB) != 2018 group by year(DOB)";

            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            List<lamb> chartData = new List<lamb>();

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
                            chartData.Add(new lamb
                            {

                                Year = Convert.ToInt32(sdr["year_of_birth"].ToString()),
                                Weight = Convert.ToInt32(sdr["Weight"].ToString())
                                
                            });
                        }
                    }

                    con.Close();
                }
            }

            return View(chartData);

        }

        public ActionResult MedicationIssue()
        {
            // DBContext db = new DBContext();
            // return View(db.sheep.ToList());
            string query = "SELECT year(Date) as year_of_issue,sum(case when issue='Reid out' then 1 else 0 end) as Reid_Out from dbo.medication group by year(Date)";

            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            List<medication> chartData = new List<medication>();

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
                            chartData.Add(new medication
                            {

                                Year = Convert.ToInt32(sdr["year_of_issue"].ToString()),
                                Issue =sdr["Reid_Out"].ToString()

                            });
                        }
                    }

                    con.Close();
                }
            }

            return View(chartData);

        }

        public JsonResult GetEvents()
        {
            using (DBContext dc = new DBContext())
            {
                var events = dc.calevent.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [HttpPost]
        public JsonResult SaveEvent(calevent e)
        {
            var status = false;
            using (DBContext dc = new DBContext())
            {
                if (e.EventID > 0)
                {
                    //Update the event
                    var v = dc.calevent.Where(a => a.EventID == e.EventID).FirstOrDefault();
                    if (v != null)
                    {
                        v.Subject = e.Subject;
                        v.Start = e.Start;
                        v.Enddate = e.Enddate;
                        v.Description = e.Description;
                        v.IsFullDay = e.IsFullDay;
                        v.ThemeColor = e.ThemeColor;
                    }
                }
                else
                {
                    dc.calevent.Add(e);
                }
                dc.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult DeleteEvent(int eventID)
        {
            var status = false;
            using (DBContext dc = new DBContext())
            {
                var v = dc.calevent.Where(a => a.EventID == eventID).FirstOrDefault();
                if (v != null)
                {
                    dc.calevent.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

      
    }
}