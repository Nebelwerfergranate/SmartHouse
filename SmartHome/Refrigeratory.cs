namespace SmartHome
{
    public class Refrigeratory : Device, IFridgeModule
    {
        private double temperature;
        private bool isOpen;
        public double Temperature
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

        public bool IsOpen
        {
            get { return isOpen; }
        }

        public void Open()
        {
            isOpen = true;
        }

        public void Close()
        {
            isOpen = false;
        }

        public Refrigeratory()
        {
            Name = "Морозильная камера";
            Temperature = -10;
        }
    }
}
