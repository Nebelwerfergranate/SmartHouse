using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHome
{
    public delegate void SendMessage(string str);
    public class Microwave : Device, IClock, IOpenable, IBacklight
    {
        public event SendMessage IsReady;
        private bool isOpen;
        private Clock clock = new Clock();
        private System.Timers.Timer timer = new System.Timers.Timer();
        private TimerInfo timerInfo = new TimerInfo();
        private Lamp backlight;
        private bool isHighlighted;
 
        public string OnReadyMessage { get; set; }
        public DateTime CurrentTime
        {
            get { return clock.CurrentTime; }
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
            timerInfo.Minutes = minutes;
        }

        public void TimerSetSeconds(byte seconds)
        {
            timerInfo.Seconds = seconds;
        }

        public void Start()
        {
            timer.Interval = timerInfo.GetMilliseconds();
            timer.Start();
        }
        public void Stop()
        {
            timer.Stop();
        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (IsReady != null)
            {
                IsReady.Invoke(OnReadyMessage);
            }
        }

        public Microwave(Lamp lamp)
        {
            Name = "Микроволновая печь";
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = false;
            this.backlight = lamp;
        }

        private class TimerInfo
        {
            private byte seconds;
            private byte minutes;
            
            public byte Seconds
            {
                get { return seconds; }
                set
                {
                    if (value > 59)
                    {
                        return;
                    }
                    seconds = value;
                }
            }
            public byte Minutes
            {
                get { return minutes; }
                set
                {
                    if (value > 59)
                    {
                        return;
                    }
                    minutes = value;
                }
            }

            public int GetMilliseconds()
            {
                return Seconds * 1000 + Minutes * 60 * 1000;
            }
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

        public bool IsHighlighted
        {
            get { return isHighlighted; }
        }

        public double GetLampPower()
        {
            return backlight.Power;
        }
    }
}
