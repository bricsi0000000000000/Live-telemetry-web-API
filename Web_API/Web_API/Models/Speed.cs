using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_API.Models
{
    public class Speed : Channel<int>
    {
        public override int Value { get => base.Value; set => base.Value = value; }
    }
}
