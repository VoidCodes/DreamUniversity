using SigmaUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SigmaUniversity.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();
        // GET: List
        public ActionResult List()
        {
            // Check if session is valid
           if (Session["StaffID"] != null)
            {
                var data = db.College.SqlQuery("SELECT * FROM College").ToList();
                return View(data);
            }
           else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Account
        public ActionResult Account()
        {
            // Check if session is valid
            if (Session["StaffID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Home
        public ActionResult Index()
        {
            //Session.RemoveAll();
            return View();
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            if (Session["StaffID"] != null)
            {
                var data = db.College.SqlQuery("SELECT * FROM College WHERE CollegeCode=@p0", id).SingleOrDefault();
                return View(data);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            if (Session["StaffID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["StaffID"] != null)
            {
                var data = db.College.SqlQuery("SELECT * FROM College WHERE CollegeCode=@p0", id).SingleOrDefault();
                return View(data);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["StaffID"] != null)
            {
                var data = db.College.SqlQuery("SELECT * FROM College WHERE CollegeCode=@p0", id).SingleOrDefault();
                return View(data);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Logout
        /*public ActionResult Logout()
        {
            return RedirectToAction("Index");
        }*/

        // POST: Logout
        [HttpPost]
        public ActionResult Logout()
        {
            Session.Abandon();
            return Json(new { status = "done" });
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(College collection)
        {
            if (Session["StaffID"] != null)
            {
                try
                {
                    // Create new list to store details of new record to be inserted
                    List<object> newRecord = new List<object>();
                    newRecord.Add(collection.CollegeCode);
                    newRecord.Add(collection.CollegeName);
                    newRecord.Add(collection.DeanFirstName);
                    newRecord.Add(collection.DeanLastName);
                    newRecord.Add(collection.DeanEmail);

                    // Copy records into array for SQL statement
                    object[] recordItems = newRecord.ToArray();
                    int result = db.Database.ExecuteSqlCommand("INSERT INTO College " + "(CollegeCode, CollegeName, DeanFirstName, DeanLastName, DeanEmail) " +
                        "VALUES (@p0, @p1, @p2, @p3, @p4)", recordItems);

                    if (result > 0)
                    {
                        ViewBag.msg = "Record added successfully!";
                    }
                    return View();
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, College collection)
        {
            if (Session["StaffID"] != null)
            {
                try
                {
                    // Create new list to store details of new record to be updated
                    List<object> record = new List<object>();
                    record.Add(collection.CollegeCode);
                    record.Add(collection.CollegeName);
                    record.Add(collection.DeanFirstName);
                    record.Add(collection.DeanLastName);
                    record.Add(collection.DeanEmail);

                    // Copy records into array for SQL statement
                    object[] recordItems = record.ToArray();
                    int result = db.Database.ExecuteSqlCommand("UPDATE College " + "SET CollegeCode=@p0, CollegeName=@p1, DeanFirstName=@p2, DeanLastName=@p3, DeanEmail=@p4 " +
                        "WHERE CollegeCode=" + id, recordItems);

                    if (result > 0)
                    {
                        ViewBag.msg = "Record update success!";
                    }
                    return View();
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete (int id, College collection)
        {
            if (Session["StaffID"] != null)
            {
                try
                {
                    // SQL Statement to delete record
                    int result = db.Database.ExecuteSqlCommand("DELETE FROM College WHERE CollegeCode=@p0", id);

                    if (result > 0)
                    {
                        // Return to Home when record is deleted successfully
                        return RedirectToAction("Index");
                    }
                    return View();
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        /* Admin section */
        // POST: Home
        [HttpPost]
        public ActionResult Index(ITAdmin login)
        {
            try
            {
                // Create new list to store login details
                List<object> newlogin = new List<object>();
                newlogin.Add(login.StaffID);
                newlogin.Add(login.Password);

                // Copy login details into array for SQL statement
                object[] loginitems = newlogin.ToArray();
                var data = db.ITAdmin.SqlQuery("SELECT * FROM ITADMIN WHERE StaffID=@p0 AND Password=@p1", loginitems).SingleOrDefault();

                // Check if login is successful
                if (data != null)
                {
                    // Start session
                    Session["StaffID"] = login.StaffID.ToString();
                    // Redirect to List(Home) page
                    return RedirectToAction("List");
                }
                else
                {
                    ViewBag.msg = "Staff ID or Password is invalid";
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}