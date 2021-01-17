using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_API.Models
{
    public class Time : Channel<float>
    {
        public override float Value { get => base.Value; set => base.Value = value; }
    }
}
