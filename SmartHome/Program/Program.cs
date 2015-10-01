using System.Collections.Generic;

namespace SmartHome
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Device> devices = new Dictionary<string, Device>();
            //Test.ClockTest(devices);
            //Test.FridgeTest(devices);
            //Test.TimerTest(devices);
            ConsoleMenu.GetDefaultDevices(devices);
            ConsoleMenu.Start(devices);
        }
    }
}
