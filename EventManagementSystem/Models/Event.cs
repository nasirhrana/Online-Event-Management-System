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
        [Required(ErrorMessage = "Please Enter Event Start Time")]
        [DataType(DataType.Time)]
        public double StartTime { get; set; }
        [NotMapped]

        public double StartTime1 { get; set; }
        [Required(ErrorMessage = "Please Enter Event End Time")]
        [DataType(DataType.Time)]
        public double EndTime { get; set; }
        [NotMapped]
        public double EndTime1 { get; set; }

        public string CreatedBy { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime TimeOfCreation { get; set; }

        public User User { get; set; }


    }
}