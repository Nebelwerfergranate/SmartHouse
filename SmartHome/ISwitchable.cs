using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHome
{
    public interface ISwitchable
    {
        bool IsOn
        {
            get;
        }
    
        void TurnOn();

        void TurnOff();
    }
}
