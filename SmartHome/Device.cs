namespace SmartHome
{
    public abstract class Device
    {
        public virtual string Name
        {get; set; }

        protected bool isOn;

        public bool IsOn
        {
            get { return isOn; }
        }

        public virtual void TurnOn()
        {
            isOn = true;
        }

        public virtual void TurnOff()
        {
            isOn = false;
        }
    }
}
