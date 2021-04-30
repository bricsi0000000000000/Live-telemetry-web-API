using DataLayer.Models.Sensors;
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
        public List<Speed> SpeedValues { get; set; } = new List<Speed>();
        [NotMapped]
        public List<Time> TimeValues { get; set; } = new List<Time>();
        [NotMapped]
        public List<Yaw> YawValues { get; set; } = new List<Yaw>();
    }
}
