using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlayForDays.Models
{
    //The Player class is a child of the SportingEvent class.
    //A Player object can be created with the simple attributes of:
    //First Name, Last Name, Skill Level, Height, and a corresponding event
    public class Player
    {
        public int PlayerId { get; set; }
        [Required]
        [MaxLength(40)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(40)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Range(1,10)]
        [Display(Name = "Skill Level")]
        public int SkillLevel { get; set; }
        [Range(20,84)]
        [Display(Name = "Height (in Inches)")]
        public double HeightInInches { get; set; }
        //FK
        [Display(Name = "Sporting Event")]
        public int SportingEventId { get; set; }
        //Parent Reference
        [Display(Name = "Sporting Event")]
        public SportingEvent SportingEvent { get; set; }
    }
}
