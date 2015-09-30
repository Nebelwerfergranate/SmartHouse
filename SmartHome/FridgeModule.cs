namespace SmartHome
{
    public abstract class FridgeModule : Device, IOpenable, ITemperaturable
    {
        // Поля
        protected bool isOpen;

        protected double temperature;

        protected uint volume;


        //Свойства
        public bool IsOpen
        {
            get { return isOpen; }
        }

        public abstract double Temperature
        { get; set; }

        public uint Volume
        {
            get { return volume; }
        }



        // Конструкторы
        protected FridgeModule(uint volume)
        {
            this.volume = volume;
        }


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
