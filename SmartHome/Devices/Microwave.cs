using System;

namespace SmartHome
{
    public class Microwave : Device, IClock, ITimer, IOpenable, IBacklight, IVolume
    {
        // Fields
        private readonly Clock clock = new Clock();

        private readonly Lamp backlight;

        private readonly double volume;

        private readonly System.Timers.Timer timer = new System.Timers.Timer();
        private bool isRunning;

        private bool isOpen;


        // Constructors
        public Microwave( double volume, Lamp lamp)
        {
            Name = "Микроволновая печь";
            this.backlight = lamp;
            if (volume < 10)
            {
                this.volume = 10;
            }
            else
            {
                this.volume = volume;
            }
            timer.AutoReset = false;
            timer.Elapsed += (sourse, eventArgs) =>
            {
                if (OperationDone != null && this.IsOn)
                {
                    OperationDone.Invoke(this);
                }
            };
        }


        // Events
        public event OperationDoneDelegate OperationDone;


        // Properties
        public DateTime CurrentTime
        {
            get { return clock.CurrentTime; }
            set { clock.CurrentTime = value; }
        }

        public bool IsRunning
        {
            get { return isRunning; }
        }

        public bool IsOpen
        {
            get { return isOpen; }
        }

        public bool IsHighlighted
        {
            get { return backlight.IsOn; }
        }
        public double LampPower
        {
            get { return backlight.Power; }
        }

        public double Volume
        {
            get { return volume; }
        }


        // Methods
        public override void TurnOn()
        {
            base.TurnOn();
            if (this.isOpen)
            {
                backlight.TurnOn();
            }
            clock.TurnOn();
        }
        public override void TurnOff()
        {
            base.TurnOff();
            this.Stop();
            backlight.TurnOff();
            clock.TurnOff();
        }

        public void SetTimer(TimeSpan time)
        {
            if (this.isOn)
            {
                timer.Interval = time.Seconds * 1000 + time.Minutes * 60 * 1000;
            }
        }
        public void Start()
        {
            if (this.isOn && !IsOpen && timer.Interval > 0)
            {
                timer.Start();
                isRunning = true;
                backlight.TurnOn();
            }
        }
        public void Stop()
        {
            timer.Stop();
            isRunning = false;
            if (!this.IsOpen)
            {
                backlight.TurnOff();
            }
        }

        public void Open()
        {
            isOpen = true;
            if (this.IsOn)
            {
                backlight.TurnOn();
            }
            this.Stop();
        }
        public void Close()
        {
            isOpen = false;
            backlight.TurnOff();
        }
    }
}
