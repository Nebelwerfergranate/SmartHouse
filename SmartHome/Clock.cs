using System;

namespace SmartHome
{
    public class Clock : Device, IClock
    {
        // Поля
        private TimeSpan delta;


        // Свойства
        public DateTime CurrentTime
        {
            get
            {
                if (this.IsOn)
                {
                    return DateTime.Now + delta;
                }
                return new DateTime(1, 1, 1, 0, 0, 0);
            }
        }


        // Конструкторы
        public Clock()
        {
            delta = -DateTime.Now.TimeOfDay;
            Name = "Часы";
        }
        public Clock(byte hours, byte minutes)
            : this()
        {
            this.TurnOn();
            SetHours(hours);
            SetMinutes(minutes);
        }


        // Методы
        public void SetHours(byte hours)
        {
            if (hours > 23 || !this.IsOn)
            {
                return;
            }
            //delta = CurrentTime - DateTime.Now;
            //delta = new TimeSpan(CurrentTime.Hour, CurrentTime.Minute, CurrentTime.Second) - DateTime.Now.TimeOfDay;
            delta = new TimeSpan(hours, CurrentTime.Minute, CurrentTime.Second) - DateTime.Now.TimeOfDay;
        }
        public void SetMinutes(byte minutes)
        {
            if (minutes > 59 || !this.IsOn)
            {
                return;
            }
            delta = new TimeSpan(CurrentTime.Hour, minutes, CurrentTime.Second) - DateTime.Now.TimeOfDay;
        }
    }
}
