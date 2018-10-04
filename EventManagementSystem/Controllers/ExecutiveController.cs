using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventManagementSystem.Models;

namespace EventManagementSystem.Controllers
{
    public class ExecutiveController : Controller
    {
        //
        // GET: /Executive/
        private EMSDbContext dbContext=new EMSDbContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult VisitorRegistration()
        {
            ViewBag.EventId = new SelectList(dbContext.Events, "EventId", "EventName");
            return View();
        }
        [HttpPost]
        public ActionResult VisitorRegistration(VisitorRegistration visitorRegistration)
        {
            ViewBag.EventId = new SelectList(dbContext.Events);
            visitorRegistration.DOB = visitorRegistration.DateOfBirth.ToString();
            visitorRegistration.TimeOfRegistration = DateTime.Now;
            var Visitor = dbContext.VisitorRegistrations.ToList();
            var count = Visitor.Count;
            var ZeroToBeAdded = 3;
            string number = "";
            for (int i = 0; i < ZeroToBeAdded; i++)
            {
                number += 0;
            }
            count += 1;
            visitorRegistration.TicketNo ="CODEBREAKERS" + "-" + number + count;
                dbContext.VisitorRegistrations.Add(visitorRegistration);
                dbContext.SaveChanges();
                ViewBag.msg = "Visitor Has been  succesfully registered";
            
            
            return View();
        }

        public ActionResult GetEventById(int eId)
        {
            var eEvent=
            dbContext.Events.Where(m=>m.EventId==eId);
           string sss= eEvent.FirstOrDefault().Date;
            return Json(eEvent.ToList(), JsonRequestBehavior.AllowGet);
        }

	}
}