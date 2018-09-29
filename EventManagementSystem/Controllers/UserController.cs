using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventManagementSystem.Models;

namespace EventManagementSystem.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        private EMSDbContext dbContext = new EMSDbContext();
        public ActionResult UserIndex()
        {

            return View();
        }
        [HttpGet]
        public ActionResult CreateUser()
        {
            ViewBag.UserTypeId = new SelectList(dbContext.UserTypes, "Id", "UserTypeName");
            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(User user)
        {
            ViewBag.UserTypeId = new SelectList(dbContext.UserTypes);
            if (ModelState.IsValid)
            {
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
                ViewBag.msg = "Employee has been saved succesfully";
            }
            else
            {
                ViewBag.msg = "Failed to save";
            }
            return View();
        }
        public JsonResult IsEmpIdExist(string email)
        {
            bool isExist = false;
            if (dbContext.Users.Any(o => o.Email == email))
            {
                isExist = true;
            }
            return Json(!isExist, JsonRequestBehavior.AllowGet);
        }
	}
}