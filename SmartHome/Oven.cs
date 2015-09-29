namespace SmartHome
{
    public class Oven : Device, ITemperaturable, IOpenable, IBacklight, ITimer
    {
        // События
        public event OperationDoneDelegate OperationDone;


        // Поля
        private double temperature = 110;

        private bool isOpen;

        private Lamp backlight;

        private System.Timers.Timer timer = new System.Timers.Timer();
        private TimerInfo timerInfo = new TimerInfo();
        private bool isRunning;


        // Свойства
        public double Temperature
        {
            get { return temperature; }
            set
            {
                if (value > 110 && value < 250)
                {
                    temperature = value;
                }
            }
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

        public bool IsRunning
        {
            get { return isRunning; }
        }


        // Конструкторы
        public Oven(Lamp lamp)
        {
            Name = "Духовка";
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

        // Методы
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
            this.Stop();
            backlight.TurnOff();
        }

        public void Close()
        {
            isOpen = false;
            backlight.TurnOff();
        }
        public void Open()
        {
            isOpen = true;
            if (this.IsOn)
            {
                backlight.TurnOn();
            }
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
            // В отличие от микроволновки духовку можно открывать.
            if (this.isOn && timerInfo.GetMilliseconds() > 0)
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
    }
}
