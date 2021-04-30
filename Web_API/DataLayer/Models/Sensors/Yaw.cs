using DataLayer.Models.Default;

namespace DataLayer.Models.Sensors
{
    public class Yaw : Sensor<float>
    {
        public override float Value { get => base.Value; set => base.Value = value; }
    }
}
