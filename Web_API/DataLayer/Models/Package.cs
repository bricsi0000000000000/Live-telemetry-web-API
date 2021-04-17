using DataLayer.Models.Sensors;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public class Package
    {
        public int ID { get; set; }
        public List<Speed> Speeds { get; set; } = new List<Speed>();
        public List<Time> Times { get; set; } = new List<Time>();
        public int SectionID { get; set; }
    }
}
