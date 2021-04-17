using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Default
{
    abstract public class Sensor<ValueType>
    {
        [Key]
        public int ID { get; set; }

        [Required]
        virtual public ValueType Value { get; set; }

        [Required]
        [ForeignKey("package")]
        public int PackageID { get; set; }
    }
}
