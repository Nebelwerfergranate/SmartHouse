﻿namespace SmartHome
{
    public abstract class FridgeModule : Device, IOpenable, ITemperature, IVolume
    {
        // Fields
        protected readonly double volume;

        protected bool isOpen;

        protected double temperature;


        // Constructors
        protected FridgeModule(string name, uint volume) : base(name)
        {
            if (volume < 10)
            {
                this.volume = 10;
            }
            else
            {
                this.volume = volume;
            }
        }


        // Properties
        public bool IsOpen
        {
            get { return isOpen; }
        }

        public abstract double Temperature
        { get; set; }

        public double Volume
        {
            get { return volume; }
        }


        // Methods
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
