using DataLayer.Models.Default;

namespace DataLayer.Models.Sensors
{
    public class Speed : Channel<int>
    {
        public override int Value { get => base.Value; set => base.Value = value; }
    }
}
