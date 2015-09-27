namespace SmartHome
{
    public class Coldstore : Device, IFridgeModule, IBacklight
    {
        private double temperature;
        private bool isOpen;
        private Lamp backlight;
        public double Temperature
        {
            get { return temperature; }
            set
            {
                if(value < 5 && value > -5)
                {
                    temperature = value;
                }
            }
        }

        public bool IsHighlighted
        {
            get { return backlight.IsOn; }
        }

        public double GetLampPower()
        {
            return backlight.Power;
        }
        public bool IsOpen
        {
            get { return isOpen; }
        }

        public void Open()
        {
            isOpen = true;
            if (this.IsOn)
            {
                backlight.TurnOn();
            }
        }

        public void Close()
        {
            isOpen = false;
            if (backlight.IsOn)
            {
                backlight.TurnOff();
            }
        }

        public override void TurnOn()
        {
            base.TurnOn();
            if (this.isOpen)
            {
                backlight.TurnOn();
            }
        }

        public override void TurnOff()
        {
            base.TurnOff();
            backlight.TurnOff();
        }

        public Coldstore(Lamp lamp)
        {
            backlight = lamp;
            Temperature = 0;
            Name = "Холодильная камера";
        }
    }
}
