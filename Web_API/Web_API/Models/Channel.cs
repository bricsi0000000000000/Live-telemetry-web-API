using System.ComponentModel.DataAnnotations;

namespace Web_API.Models
{
    abstract public class Channel<ValueType>
    {
        [Key]
        public int ID { get; set; }

        virtual public ValueType Value { get; set; }
    }
}
