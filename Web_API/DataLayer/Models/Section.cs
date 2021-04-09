using DataLayer.Models.Sensors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Models
{
    public class Section
    {
        [Key]
        public int ID { get; set; }
        public bool IsLive { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string SensorNames { get; set; }
    }
}
