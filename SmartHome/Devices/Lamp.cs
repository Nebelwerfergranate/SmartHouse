namespace SmartHome
{
    public class Lamp : SmartHome.Device
    {
        // Fields
        // Лампочки с нулевой мощностью не смогут подсвечивать.
        private readonly double power = 1;


        // Constructors
        public Lamp(double power) : base("лампочка")
        {
            if (power > 1)
            {
                this.power = power;
            }
        }


        // Properties
        public double Power
        {
            get { return power; }
        }
    }
}
