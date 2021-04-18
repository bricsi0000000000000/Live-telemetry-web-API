using DataLayer.Models.Sensors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public class Package
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public int SectionID { get; set; }
        [Required]
        public long SentTime { get; set; }

        [NotMapped]
        public List<Speed> Speeds { get; set; } = new List<Speed>();
        [NotMapped]
        public List<Time> Times { get; set; } = new List<Time>();
    }
}
