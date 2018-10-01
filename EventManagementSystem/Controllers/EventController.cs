using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventManagementSystem.Models;
using EventManagementSystem.ViewModel;

namespace EventManagementSystem.Controllers
{
    public class EventController : Controller
    {
        //
        // GET: /Event/
        private EMSDbContext dbContext = new EMSDbContext();
        public ActionResult EventIndex()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CreateEvent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateEvent(Event eEvent)   
        {
            eEvent.UserId = 1;
            eEvent.CreatedBy = "Nasir";
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
            return View();
        }
        public ActionResult ShowDateWiseEventByRange(DateTime frmDate, DateTime edDate)
        {
            var eventList = dbContext.Events.Where(x => x.EventDate > frmDate && x.EventDate < edDate);

            return Json(eventList.ToList(), JsonRequestBehavior.AllowGet);
        }
	}
}