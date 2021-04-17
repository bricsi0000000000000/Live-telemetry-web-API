using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class Section
    {
        [Key]
        public int ID { get; set; }
        public bool IsLive { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Date { get; set; }
        public string SensorNames { get; set; }
    }
}
