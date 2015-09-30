namespace SmartHome
{
    public class Refrigeratory : FridgeModule
    {
        // Свойства
        public override double Temperature
        {
            get { return temperature; }
            set
            {
                if (value < -6 && value > -30)
                {
                    temperature = value;
                }
            }
        }


        // Конструкторы
        public Refrigeratory(uint volume) : base(volume)
        {
            Name = "Морозильная камера";
            Temperature = -10;
        }
    }
}
