using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlayForDays.Models
{
    public class SportingEvent
    {
        //PK
        public int SportingEventId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [MaxLength(2)]
        public string Province { get; set; }
        //Parent Reference
        public int SportId { get; set; }
        //Child Reference
        public List<Player> Players { get; set; }
    }
}
