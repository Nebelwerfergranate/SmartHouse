using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
