namespace SmartHome
{
    public abstract class Device
    {
        // Поля
        protected bool isOn;


        // Свойства
        public virtual string Name
        {get; set; }
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
