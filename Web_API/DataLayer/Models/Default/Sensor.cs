using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Default
{
    abstract public class Sensor<ValueType>
    {
        [Key]
        public int ID { get; set; }

        [Required]
        virtual public ValueType Value { get; set; }

        [Required]
        public int PackageID { get; set; }

        [Required]
        public int SectionID { get; set; }
    }
}
