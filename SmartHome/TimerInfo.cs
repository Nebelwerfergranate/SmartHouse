namespace SmartHome
{
    internal class TimerInfo
    {
        // Поля
        private byte seconds;
        private byte minutes;


        // Свойства
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


        // Методы
        public int GetMilliseconds()
        {
            return Seconds * 1000 + Minutes * 60 * 1000;
        }
    }
}
