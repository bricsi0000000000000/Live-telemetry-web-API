using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Default
{
    abstract public class Channel<ValueType>
    {
        [Key]
        public int ID { get; set; }

        virtual public ValueType Value { get; set; }

        public int PackageID { get; set; }
    }
}
