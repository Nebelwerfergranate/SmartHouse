using System;

namespace SmartHome
{
    public interface IClock
    {
        DateTime CurrentTime
        {
            get;
        }

        void SetHours(byte hours);

        void SetMinutes(byte minutes);
    }
}
