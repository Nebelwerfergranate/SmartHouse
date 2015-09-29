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
            clocks.Add(new Clock(18,0));
            clocks.Add(new Clock(0, 25));
            clocks.Add(new Clock(1, 44));
            clocks.Add(new Clock(12, 0));
            clocks.Add(new Clock(18, 25));
            clocks.Add(new Clock(23, 59));

            while (false)
            {
                foreach (Clock clock in clocks)
                {
                   Console.WriteLine(clock.CurrentTime.ToLongTimeString());
                }
                Console.WriteLine();
                Thread.Sleep(1000);
            }


            Fridge myFridge = Factory.Get2CamFridge(); 
            //string state = Informer.StateToString(myFridge);
            Console.WriteLine(Informer.StateToString(myFridge));

            myFridge.TurnOn();
            Console.WriteLine("myFridge.TurnOn()");
            Console.WriteLine(Informer.StateToString(myFridge));

            myFridge[0].Open();
            Console.WriteLine("myFridge.Modules[0].Open()");
            Console.WriteLine(Informer.StateToString(myFridge));
           
            
            myFridge.TurnOff();
            Console.WriteLine("myFridge.TurnOff()");
            Console.WriteLine(Informer.StateToString(myFridge));

            myFridge.TurnOn();
            Console.WriteLine("myFridge.TurnOn()");
            Console.WriteLine(Informer.StateToString(myFridge));

            myFridge[0].Close();
            Console.WriteLine("myFridge.Modules[0].Close()");
            Console.WriteLine(Informer.StateToString(myFridge));

            Microwave microwaveOven = Factory.GetMicrowaveOven();
            microwaveOven.TurnOn();
            microwaveOven.TimerSetMinutes(0);
            microwaveOven.TimerSetSeconds(20);
            microwaveOven.SetHours(12);
            microwaveOven.Start();
            Console.WriteLine(Informer.StateToString(microwaveOven));

            Microwave microwave = Factory.GetMicrowaveOven();
            microwave.TurnOn();
            microwave.TimerSetMinutes(1);
            microwave.TimerSetSeconds(10);
            microwave.Open();
            microwave.Start();
            Console.WriteLine(Informer.StateToString(microwave));

            Oven myOven = Factory.GetOven();
            myOven.TurnOn();
            myOven.TimerSetSeconds(15);
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
