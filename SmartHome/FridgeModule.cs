namespace SmartHome
{
    public abstract class FridgeModule : Device, IOpenable, ITemperaturable
    {
        // Поля
        protected bool isOpen;

        protected double temperature;


        //Свойства
        public bool IsOpen
        {
            get { return isOpen; }
        }

        public abstract double Temperature
        { get; set; }


        // Методы
        public virtual void Close()
        {
            isOpen = false;
        }
        public virtual void Open()
        {
            isOpen = true;
        }
    }
}
