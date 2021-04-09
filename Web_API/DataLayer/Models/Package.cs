using DataLayer.Models.Sensors;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class Package
    {
        [Key]
        public int ID { get; set; }
        public List<Speed> Speeds { get; set; } = new List<Speed>();
        public List<Time> Times { get; set; } = new List<Time>();
    }
}
