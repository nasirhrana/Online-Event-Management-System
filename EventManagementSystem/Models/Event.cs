using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventManagementSystem.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please Enter Event Name")]
        public string EventName { get; set; }
        [Required(ErrorMessage = "Please Enter Event Venue")]
        public string EventVenue { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]
        [EmailAddress]
        public string OrganizerEmail { get; set; }
        [Required(ErrorMessage = "Please Enter Contact Number")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Contact Number must be numeric")]
        public string ContactNo { get; set; }
        
        [Required(ErrorMessage = "Please Enter Event Date")]
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }
        [DataType(DataType.Date)]
        public string Date { get; set; }
        [Required(ErrorMessage = "Please Enter Event Start Time")]
        
        public double StartTime1 { get; set; }
        [DataType(DataType.Time)]
        public string StartTime { get; set; }
        [Required(ErrorMessage = "Please Enter Event End Time")]
        
        public double EndTime1 { get; set; }
        [DataType(DataType.Time)]
        public string EndTime { get; set; }

        public string CreatedBy { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime TimeOfCreation { get; set; }

        public User User { get; set; }
        public List<VisitorRegistration> VisitorRegistrations { get; set; }


    }
}