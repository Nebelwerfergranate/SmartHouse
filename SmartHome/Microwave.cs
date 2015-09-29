using System;

namespace SmartHome
{
    public delegate void OperationDoneDelegate(Device source);
    public class Microwave : Device, IClock, ITimer, IOpenable, IBacklight
    {
        // События
        public event OperationDoneDelegate OperationDone;


        // Поля
        private Clock clock = new Clock();

        private System.Timers.Timer timer = new System.Timers.Timer();
        private TimerInfo timerInfo = new TimerInfo();
        private bool isRunning;

        private bool isOpen;
 
        private Lamp backlight;
        

        // Конструкторы
        public Microwave(Lamp lamp)
        {
            Name = "Микроволновая печь";
            this.backlight = lamp;
            timer.AutoReset = false;
            timer.Elapsed += (sourse, eventArgs) =>
            {
                if (OperationDone != null && this.IsOn)
                {
                    OperationDone.Invoke(this);
                }
            };
        }


        // Свойства
        public DateTime CurrentTime
        {
            get { return clock.CurrentTime; }
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


        // Методы
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

        public void SetHours(byte hours)
        {
            clock.SetHours(hours);
        }
        public void SetMinutes(byte minutes)
        {
            clock.SetMinutes(minutes);
        }

        public void TimerSetMinutes(byte minutes)
        {
            if (this.IsOn)
            {
                timerInfo.Minutes = minutes;
            }
        }
        public void TimerSetSeconds(byte seconds)
        {
            if (this.IsOn)
            {
                timerInfo.Seconds = seconds;
            }
        }
        public void Start()
        {
            if (this.isOn && !IsOpen && timerInfo.GetMilliseconds() > 0)
            {
                timer.Interval = timerInfo.GetMilliseconds();
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
