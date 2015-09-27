using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHome
{
    public interface IOpenable
    {
        bool IsOpen { get; }

        void Close();

        void Open();
    }
}
