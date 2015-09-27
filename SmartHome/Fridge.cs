namespace SmartHome
{
    public class Fridge : Device
    {
        private IFridgeModule[] modules;

        public IFridgeModule[] Modules
        {
            get { return modules; }
        }

        public Fridge(IFridgeModule[] modules)
        {
            this.modules = modules;
            Name = "Холодильник";
        }

        public override void TurnOn()
        {
            base.TurnOn();
            for (int i = 0; i < modules.Length; i++)
            {
                if (modules[i] is Device)
                {
                    ((Device) modules[i]).TurnOn();
                }
            }
        }

        public override void TurnOff()
        {
            base.TurnOff();
            for (int i = 0; i < modules.Length; i++)
            {
                if (modules[i] is Device)
                {
                    ((Device)modules[i]).TurnOff();
                }
            }
        }
    }
}
