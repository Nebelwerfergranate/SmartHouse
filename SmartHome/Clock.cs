using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHome
{
    public class Clock : ISwitchable, IClock
    {
        private bool isOn;
        private TimeSpan delta;

        public bool IsOn
        {
            get { return isOn; }
        }

        public void TurnOn()
        {
            isOn = true;
        }

        public void TurnOff()
        {
            isOn = false;
        }

        public DateTime CurrentTime
        {
            get { return DateTime.Now + delta; }
        }

        public void SetHours(byte hours)
        {
            // delta = new TimeSpan(hours, DateTime.Now.Minute, DateTime.Now.Second) - (DateTime.Now.TimeOfDay + delta);
            //delta = CurrentTime - DateTime.Now;
            //delta = new TimeSpan(CurrentTime.Hour, CurrentTime.Minute, CurrentTime.Second) - DateTime.Now.TimeOfDay;
            delta = new TimeSpan(hours, CurrentTime.Minute, CurrentTime.Second) - DateTime.Now.TimeOfDay;
        }

        public void SetMinutes(byte minutes)
        {
            //delta = new TimeSpan(DateTime.Now.Hour, minutes, DateTime.Now.Second) - (DateTime.Now.TimeOfDay + delta);
            //delta = CurrentTime - DateTime.Now;
            //delta = new TimeSpan(CurrentTime.Hour, CurrentTime.Minute, CurrentTime.Second) - DateTime.Now.TimeOfDay;
            delta = new TimeSpan(CurrentTime.Hour, minutes, CurrentTime.Second) - DateTime.Now.TimeOfDay;
        }

        public Clock()
        {
            delta = -DateTime.Now.TimeOfDay;
        }

        public Clock(byte hours, byte minutes)
        {
            delta = -DateTime.Now.TimeOfDay;
            SetHours(hours);
            SetMinutes(minutes);
        }
    }
}
