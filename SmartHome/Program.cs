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
            List<Clock> clocks = new List<Clock>();
            clocks.Add(new Clock());
            clocks.Add(new Clock(new DateTime(1,1, 1,18,0,0)));
            clocks.Add(new Clock(new DateTime(1, 1, 1, 0, 25, 0)));
            clocks.Add(new Clock(new DateTime(1, 1, 1, 1, 44, 0)));
            clocks.Add(new Clock(new DateTime(1, 1, 1, 12, 0, 0)));
            clocks.Add(new Clock(new DateTime(1, 1, 1, 18, 25, 0)));
            clocks.Add(new Clock(new DateTime(1, 1, 1, 23, 59, 0)));

            while (false)
            {
                foreach (Clock clock in clocks)
                {
                   Console.WriteLine(clock.CurrentTime.ToLongTimeString());
                }
                Console.WriteLine();
                Thread.Sleep(1000);
            }


            Fridge myFridge = Factory.GetSamsungFridge();
            //string state = Informer.StateToString(myFridge);
            Console.WriteLine(Informer.StateToString(myFridge));

            myFridge.TurnOn();
            Console.WriteLine("myFridge.TurnOn()");
            Console.WriteLine(Informer.StateToString(myFridge));

            myFridge.OpenColdstore();
            Console.WriteLine("myFridge.Modules[0].Open()");
            Console.WriteLine(Informer.StateToString(myFridge));


            myFridge.TurnOff();
            Console.WriteLine("myFridge.TurnOff()");
            Console.WriteLine(Informer.StateToString(myFridge));

            myFridge.TurnOn();
            Console.WriteLine("myFridge.TurnOn()");
            Console.WriteLine(Informer.StateToString(myFridge));

            myFridge.OpenRefrigeratory();
            Console.WriteLine("myFridge.Modules[0].Close()");
            Console.WriteLine(Informer.StateToString(myFridge));

            Microwave microwaveOven = Factory.GetMicrowaveOven();
            microwaveOven.TurnOn();
            microwaveOven.CurrentTime = new DateTime(1,1,1,13,34,0);
            microwaveOven.SetTimer(new TimeSpan(0,0,20));
            microwaveOven.Start();
            Console.WriteLine(Informer.StateToString(microwaveOven));

            Microwave microwave = Factory.GetMicrowaveOven();
            microwave.TurnOn();
            microwave.SetTimer(new TimeSpan(0,1,10));
            microwave.Open();
            microwave.Close();
            microwave.Start();
            Console.WriteLine(Informer.StateToString(microwave));

            Oven myOven = Factory.GetOven();
            myOven.TurnOn();
            myOven.SetTimer(new TimeSpan(0, 0, 15));
            myOven.Open();
            myOven.Start();
            Console.WriteLine(Informer.StateToString(myOven));


            //clocks[0].TurnOn();
            //clocks[0].SetHours(10);
            //clocks[0].SetMinutes(35);
            //Console.WriteLine(Informer.StateToString(clocks[0]));

            //oven.Open();
            //oven.TurnOn();

            
            
            
            Console.ReadKey();
        }
    }
}
