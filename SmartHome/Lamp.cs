namespace SmartHome
{
    public class Lamp : SmartHome.Device
    {
        private double power;
        
        public double Power
        {
            get { return power; }
        }

        public Lamp (double power)
        {
            this.power = power;
        }
    }
}
