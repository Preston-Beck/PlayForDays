using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlayForDays.Models
{
    //The SportingEvent class is a child of the Sport class.
    //And a Parent of the Player class.
    //A SportingEvent object can be created with the attributes of:
    //Name, Start Time, End Time, Address, City, Province, and a corresponding Sport.
    public class SportingEvent
    {
        //PK
        public int SportingEventId { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }
        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [MaxLength(2)]
        public string Province { get; set; }
        //FK
        [Display(Name = "Sport")]
        public int SportId { get; set; }

        //Parent Reference
        public Sport Sport { get; set; }
        //Child Reference
        public List<Player> Players { get; set; }
    }
}
