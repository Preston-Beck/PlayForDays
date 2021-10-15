using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlayForDays.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        [Required]
        [MaxLength(40)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(40)]
        public string LastName { get; set; }
        [Range(1,10)]
        public int SkillLevel { get; set; }
        [Range(20,84)]
        public double HeightInInches { get; set; }
        //Parent Reference
        public int SportingEventId { get; set; }
    }
}
