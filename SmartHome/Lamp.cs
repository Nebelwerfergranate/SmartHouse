namespace SmartHome
{
    public class Lamp : SmartHome.Device
    {
        // Поля
        // Лампочки с нулевой мощностью не смогут подсвечивать.
        private double power = 1;
  
      
        // Свойства
        public double Power
        {
            get { return power; }
        }


        // Конструкторы
        public Lamp (double power)
        {
            if (power > 1)
            {
                this.power = power;
            }
        }
    }
}
