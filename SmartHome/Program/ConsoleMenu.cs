﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace SmartHome
{
    public static class ConsoleMenu
    {
        public static void Start(Dictionary<string, Device> devices)
        {
            PrintDevicesInfo(devices);
        }

        public static void ClockTest(Dictionary<string, Device> devices)
        {
            //List<Clock> clocks = new List<Clock>();
            devices.Add("часы 1", new Clock("часы 1"));
            devices.Add("часы 2", new Clock("часы 2", new DateTime(1, 1, 1, 18, 0, 0)));
            devices.Add("часы 3", new Clock("часы 3", new DateTime(1, 1, 1, 0, 25, 0)));
            devices.Add("часы 4", new Clock("часы 4", new DateTime(1, 1, 1, 1, 44, 0)));
            devices.Add("часы 5", new Clock("часы 5", new DateTime(1, 1, 1, 12, 0, 0)));
            devices.Add("часы 6", new Clock("часы 6", new DateTime(1, 1, 1, 18, 25, 0)));
            devices.Add("часы 7", new Clock("часы 7", new DateTime(1, 1, 1, 23, 59, 0)));

            while (true)
            {
                Console.Clear();
                foreach (KeyValuePair<string, Device> clock in devices)
                {
                    if (clock.Value is IClock)
                    {
                        Console.WriteLine(Informer.StateToString(clock.Value));
                    }
                }
                Thread.Sleep(1000);
            }
        }

        public static void TimerTest(Dictionary<string, Device> devices)
        {
            devices.Add("холодильник 1", FridgeFactory.GetSamsung("холодильник 1"));
            Console.WriteLine(Informer.StateToString(devices["холодильник 1"]));

            devices["холодильник 1"].TurnOn();
            Console.WriteLine("myFridge.TurnOn()");
            Console.WriteLine(Informer.StateToString(devices["холодильник 1"]));

            ((Fridge)devices["холодильник 1"]).OpenColdstore();
            Console.WriteLine("myFridge.Modules[0].Open()");
            Console.WriteLine(Informer.StateToString(devices["холодильник 1"]));


            devices["холодильник 1"].TurnOff();
            Console.WriteLine("myFridge.TurnOff()");
            Console.WriteLine(Informer.StateToString(devices["холодильник 1"]));

            devices["холодильник 1"].TurnOn();
            Console.WriteLine("myFridge.TurnOn()");
            Console.WriteLine(Informer.StateToString(devices["холодильник 1"]));

            ((Fridge)devices["холодильник 1"]).OpenRefrigeratory();
            Console.WriteLine("myFridge.Modules[0].Close()");
            Console.WriteLine(Informer.StateToString(devices["холодильник 1"]));

            devices.Add("микроволновка 1", MicrowaveFactory.GetWhirpool("микроволновка 1"));
            devices["микроволновка 1"].TurnOn();
            ((IClock)devices["микроволновка 1"]).CurrentTime = new DateTime(1, 1, 1, 13, 34, 0);
            ((ITimer)devices["микроволновка 1"]).SetTimer(new TimeSpan(0, 0, 20));
            ((ITimer)devices["микроволновка 1"]).Start();
            Console.WriteLine(Informer.StateToString(devices["микроволновка 1"]));

            devices.Add("микроволновка 2", MicrowaveFactory.GetLg("микроволновка 2"));
            devices["микроволновка 2"].TurnOn();
            ((ITimer)devices["микроволновка 2"]).SetTimer(new TimeSpan(0, 1, 10));
            ((IOpenable)devices["микроволновка 2"]).Open();
            ((IOpenable)devices["микроволновка 2"]).Close();
            ((ITimer)devices["микроволновка 2"]).Start();
            Console.WriteLine(Informer.StateToString(devices["микроволновка 2"]));

            devices.Add("духовка 1", OvenFactory.GetSiemense("духовка 1"));
            devices["духовка 1"].TurnOn();
            ((ITimer)devices["духовка 1"]).SetTimer(new TimeSpan(0, 0, 15));
            ((IOpenable)devices["духовка 1"]).Open();
            ((ITimer)devices["духовка 1"]).Start();
            Console.WriteLine(Informer.StateToString(devices["духовка 1"]));
        }

        public static void AddDevices(Dictionary<string, Device> devices)
        {
            devices.Add("kitchen clock", new Clock("kitchen clock"));
            devices.Add("bedroom clock", new Clock("bedroom clock", DateTime.Now));
            devices.Add("myMycrowave", MicrowaveFactory.GetWhirpool("myMycrowave"));
            ((IClock)devices["myMycrowave"]).CurrentTime = new DateTime(1, 1, 1, 12, 0, 0);
            devices.Add("myOven", OvenFactory.GetSiemense("myOven"));
            devices.Add("myFridge", FridgeFactory.GetIndesit("myFridge"));
        }

        private static void PrintDevicesInfo(Dictionary<string, Device> devices)
        {
            foreach (KeyValuePair<string, Device> device in devices)
            {
                Console.WriteLine(Informer.StateToString(device.Value));
            }
            Console.WriteLine();
        }
    }
}
