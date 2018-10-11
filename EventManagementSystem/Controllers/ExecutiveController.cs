using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EventManagementSystem.Models;
using iTextSharp.text;
using Microsoft.Ajax.Utilities;
using Rotativa;
using Rotativa.Options;

namespace EventManagementSystem.Controllers
{

    
    public class ExecutiveController : Controller
    {
        //
        // GET: /Executive/

        private EMSDbContext dbContext = new EMSDbContext();

        public ActionResult Index()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("Login", "Home")
                    ;
            }

            return View();
        }




        [HttpGet]
        public ActionResult VisitorRegistration()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("Login", "Home")
                    ;
            }

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
            visitorRegistration.TicketNo = "CODEBREAKERS" + "-" + number + count;
            dbContext.VisitorRegistrations.Add(visitorRegistration);
            dbContext.SaveChanges();
            ViewBag.msg = "Visitor Has been  succesfully registered";


            return View();
        }












        [HttpGet]
        public ActionResult PrintTicket()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("Login", "Home")
                    ;
            }

            return View(dbContext.VisitorRegistrations.ToList());
           
        }





        [HttpGet]

        public ActionResult Details(int? Id)
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("Login", "Home")
                    ;
            }

            VisitorRegistration visitor = new VisitorRegistration();
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            visitor = dbContext.VisitorRegistrations.Find(Id);
            if (visitor == null)
            {
                return HttpNotFound();
            }

            var aVisitor= visitor;
            
            return View(aVisitor);


            //var printTicket = new ActionAsPdf("Details");
            //return printTicket;

        }






        [HttpGet]

        public ActionResult PrintDetails(int? Id)
        {
         

            VisitorRegistration visitor = new VisitorRegistration();
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            visitor = dbContext.VisitorRegistrations.Find(Id);
            if (visitor == null)
            {
                return HttpNotFound();
            }

            var aVisitor = visitor;

            return View(aVisitor);

        }
















        public ActionResult ExportPdf(int? id)
        {

            VisitorRegistration visitor = new VisitorRegistration();
            var Id = visitor.VisitorId;
            var printTicket = new ActionAsPdf("PrintDetails", new { id = id });
            return printTicket;
        }







        //public ActionResult ToExport()
        //{
        //    VisitorRegistration visitor = new VisitorRegistration();

        //    visitor = dbContext.VisitorRegistrations;

        //    var aVisitor = visitor;
        //    return View(aVisitor);


        //}



   




      

        public ActionResult GetEventById(int eId)
        {
            var eEvent=
            dbContext.Events.Where(m=>m.EventId==eId);
           string sss= eEvent.FirstOrDefault().Date;
            return Json(eEvent.ToList(), JsonRequestBehavior.AllowGet);
        }

	}
}