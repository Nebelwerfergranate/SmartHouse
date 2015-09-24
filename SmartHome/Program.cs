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

            while (true)
            {
                foreach (Clock clock in clocks)
                {
                   // Console.WriteLine(clock.CurrentTime.Hours + ":" + clock.CurrentTime.Minutes + ":" + clock.CurrentTime.Seconds);
                    Console.WriteLine(clock.CurrentTime.ToLongTimeString());
                }
                Console.WriteLine();
                Thread.Sleep(1000);
            }
            
            
            //Clock clock = new Clock();
            //clock.SetHours(18);
            //clock.SetMinutes(25);

            //Clock c2 = new Clock();
            //c2.SetHours(1);
            //c2.SetMinutes(44);

            //Clock c3 = new Clock();

            //Clock c4 = new Clock();
            //c4.SetHours(12);
            //while (true)
            //{
            //    Console.WriteLine(clock.CurrentTime.Hours + " " + c);
            //    Console.WriteLine(c2.CurrentTime);
            //    Console.WriteLine(c3.CurrentTime);
            //    Console.WriteLine(c4.CurrentTime);
            //    Console.WriteLine();

            //    Thread.Sleep(1000);
            //}
            
     
            Console.ReadKey();
        }
    }
}
