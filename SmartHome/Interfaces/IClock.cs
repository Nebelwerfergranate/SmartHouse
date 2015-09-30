using System;

namespace SmartHome
{
    public interface IClock
    {
        DateTime CurrentTime
        {
            set;
            get;
        }


    }
}
