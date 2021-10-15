using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlayForDays.Models
{
    public class Sport
    {
        public int SportId { get; set; }
        [Required]
        public string SportName { get; set; }
        [Range(2,100)]
        public int NumOfPlayers { get; set; }
        [Range(0,50)]
        public int NumOfTeams { get; set; }
        public string Equipment { get; set; }
        //Child Reference
        public List<SportingEvent> SportingEvents { get; set; }
    }
}
