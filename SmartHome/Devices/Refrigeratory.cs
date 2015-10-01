namespace SmartHome
{
    public class Refrigeratory : FridgeModule
    {
        // Constructors
        public Refrigeratory(uint volume)
            : base("Морозильная камера", volume)
        {
            Temperature = -10;
        }


        // Properties
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
    }
}
