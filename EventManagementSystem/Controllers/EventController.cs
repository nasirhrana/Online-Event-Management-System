using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EventManagementSystem.Models;

namespace EventManagementSystem.Controllers
{
    public class EventController : Controller
    {
        //
        // GET: /Event/
        private EMSDbContext dbContext = new EMSDbContext();
        private int id;

        public ActionResult EventIndex()
        {
            if (Session["Id"]== null)
            {
              return  RedirectToAction("Login", "Home")
                ;
            }
            return View();
        }
        [HttpGet]
        public ActionResult CreateEvent()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("Login", "Home")
                ;
            }
            return View();
        }
        [HttpPost]
        public ActionResult CreateEvent(Event eEvent)   
        {
            eEvent.UserId = (int) Session["Id"];
            eEvent.CreatedBy = (string) Session["user"];
            eEvent.TimeOfCreation = DateTime.Now;
            bool isExist = false;
            var msg = "";
            
                var eventList =
                dbContext.Events.Where(m => m.EventVenue == eEvent.EventVenue && m.EventDate == eEvent.EventDate)
                    .ToList();
            if (eventList.Count==0)
            {
                dbContext.Events.Add(eEvent);
                dbContext.SaveChanges();
                msg = "Event Information has been saved successfully";
            }
                foreach (var ev in eventList)
                {
                    if ((eEvent.StartTime1 >= ev.StartTime1 && eEvent.StartTime1<ev.EndTime1)
                        || (eEvent.EndTime1 > ev.StartTime1 && eEvent.EndTime1<=ev.StartTime1))
                    {
                        msg = "Event Time conflicted";
                    }
                    else
                    {
                        dbContext.Events.Add(eEvent);
                        dbContext.SaveChanges();
                        msg = "Event Information has been saved successfully";
                    }
                }


            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowDateWiseEvent()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("Login", "Home")
                ;
            }
            return View();
        }
        public ActionResult ShowDateWiseEventByRange(DateTime frmDate, DateTime edDate)
        {
            var eventList = dbContext.Events.Where(x => x.EventDate >= frmDate && x.EventDate <= edDate);

            return Json(eventList.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowEventWiseReport()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("Login", "Home")
                ;
            }
            ViewBag.EventList = dbContext.Events.Where(m => m.EventDate < DateTime.Now);

            return View();
        }

        public ActionResult GetEventVisitorByEventId(int eventID)
        {
            var visitorList = dbContext.VisitorRegistrations.Where(m => m.EventId == eventID).ToList();
            return Json(visitorList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsVisitorEmailExist(string email)
        {
            bool isExist = false;
            if (dbContext.VisitorRegistrations.Any(o => o.VisitorEmail == email))
            {
                isExist = true;
            }
            return Json(!isExist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsVisitorContactNoExist(string contactNo)
        {
            bool isExist = false;
            if (dbContext.VisitorRegistrations.Any(o => o.VisitorContactNo == contactNo))
            {
                isExist = true;
            }
            return Json(!isExist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowEventReportGenderWise()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("Login", "Home")
                ;
            }
            return View();
        }

        public ActionResult ShowDateWiseEventReportByGender(DateTime frmDate, DateTime edDate, string gender)
        {
            var data = dbContext.Events.Include(x => x.VisitorRegistrations).Where(s =>
                    s.EventDate >= frmDate && s.EventDate <= edDate &&
                    s.VisitorRegistrations.FirstOrDefault().Gender == gender)
                .Select(s => new {EventName = s.EventName, Eventdte = s.Date, Count = s.VisitorRegistrations.Count})
                .ToList();
            return
                Json(data, JsonRequestBehavior.AllowGet);

        }
    }
}