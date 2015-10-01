using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHome
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Device> devices = new Dictionary<string, Device>();
            //ConsoleMenu.ClockTest(devices);
            //ConsoleMenu.TimerTest(devices);
            ConsoleMenu.AddDevices(devices);
            ConsoleMenu.Start(devices);
            
            Console.ReadKey();
        }
    }
}
