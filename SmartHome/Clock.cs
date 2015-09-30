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
                    return DateTime.Now + delta;
            }
            set
            {
                delta = new TimeSpan(value.Hour, value.Minute, CurrentTime.Second) - DateTime.Now.TimeOfDay;
            }
        }


        // Конструкторы
        public Clock()
        {
            delta = -DateTime.Now.TimeOfDay;
            Name = "Часы";
        }
        public Clock(DateTime time)
            : this()
        {
            CurrentTime = time;
        }
    }
}
