using System;
using System.Collections.Generic;

namespace SmartHome
{
    public static class ConsoleMenu
    {
        // Fields
        private static Dictionary<string, Device> devices;
        private const string noNameError = "У устройства должно быть имя!";
        private const string noNameOrFabricatorError = "Не указаны имя или производитель устройства!";
        private const string existingNameError = "Устройство с таким именем уже существует!";
        private const string nameNotFoundError = "Устройства с таким именем не существует!";

        // Methods
        public static void Start(Dictionary<string, Device> existingDevices)
        {
            devices = existingDevices;
            while (true)
            {
                Console.Clear();
                PrintDevicesInfo(devices);
                Console.WriteLine();
                Console.Write("Введите команду: ");
                string userInput = Console.ReadLine();
                if (userInput.Length == 0)
                {
                    Help();
                    InformUser();
                    continue;
                }
                List<String> userCommands = new List<string>(
                    userInput.Split(new char[] { ' ' },
                    StringSplitOptions.RemoveEmptyEntries)
                    );

                if (userCommands[0].ToLower() == "add")
                {
                    userCommands.RemoveAt(0);
                    AddDevice(userCommands);
                }

                else if (userCommands[0].ToLower() == "remove" || userCommands[0].ToLower() == "rem")
                {
                    userCommands.RemoveAt(0);
                    RemoveDevice(userCommands);
                }

                else if (userCommands[0].ToLower() == "use" || userCommands[0].ToLower() == "u")
                {
                    userCommands.RemoveAt(0);
                    UseDevice(userCommands);
                }

                else if (userCommands[0].ToLower() == "global" || userCommands[0].ToLower() == "glob")
                {
                    userCommands.RemoveAt(0);
                    GlobalCommand(userCommands);
                }

                else if (userCommands[0].ToLower() == "refresh" || userCommands[0].ToLower() == "r")
                {
                    continue;
                }

                else if (userCommands[0].ToLower() == "exit")
                {
                    return;
                }

                else
                {
                    ErrorMessage(userInput);
                }
            }

        }

        public static void GetDefaultDevices(Dictionary<string, Device> devices)
        {
            devices.Add("clock1", new Clock("clock1"));
            devices.Add("clock2", new Clock("clock2", DateTime.Now));
            devices.Add("micr", MicrowaveFactory.GetWhirpool("micr"));
            ((IClock)devices["micr"]).CurrentTime = new DateTime(1, 1, 1, 12, 0, 0);
            devices.Add("oven", OvenFactory.GetSiemense("oven"));
            devices.Add("fr", FridgeFactory.GetIndesit("fr"));
        }

        private static void Help()
        {
            Console.WriteLine("Доступные команды:");
            Console.WriteLine("В скобках указана сокращенная версия команд");

            Console.WriteLine("\tadd");
            Console.WriteLine("\t\tClock");
            Console.WriteLine("\t\tFridge");
            Console.WriteLine("\t\t\tSamsung");
            Console.WriteLine("\t\t\tIndesit");
            Console.WriteLine("\t\t\tAtlant");
            Console.WriteLine("\t\tMicrowave");
            Console.WriteLine("\t\t\tWhirpool");
            Console.WriteLine("\t\t\tPanasonic");
            Console.WriteLine("\t\t\tLG");
            Console.WriteLine("\t\tOven");
            Console.WriteLine("\t\t\tSiemense");
            Console.WriteLine("\t\t\tElectrolux");
            Console.WriteLine("\t\t\tPyramida");

            Console.WriteLine("\t(rem)ove");

            Console.WriteLine("\t(u)se");
            Console.WriteLine("\t\ton");
            Console.WriteLine("\t\toff");
            Console.WriteLine("\t\topen");
            Console.WriteLine("\t\tclose");
            Console.WriteLine("\t\t(openC)oldstore");
            Console.WriteLine("\t\t(closeC)oldstore");
            Console.WriteLine("\t\t(openR)efrigeratory");
            Console.WriteLine("\t\t(closeR)efrigeratory");
            Console.WriteLine("\t\t(temp)erature");
            Console.WriteLine("\t\t(temp)erature(c)oldstore");
            Console.WriteLine("\t\t(temp)erature(r)erfrigeratory");
            Console.WriteLine("\t\tclock Формат часы:минуты:секунды");
            Console.WriteLine("\t\ttimer Формат часы:минуты:секунды");
            Console.WriteLine("\t\tstart");
            Console.WriteLine("\t\tstop");

            Console.WriteLine("\t(glob)al");
            Console.WriteLine("\t\ton");
            Console.WriteLine("\t\toff");
            Console.WriteLine("\t\tclock Формат часы:минуты:секунды");

            Console.WriteLine("\t(r)efresh");
            Console.WriteLine("\texit");
        }

        private static void PrintDevicesInfo(Dictionary<string, Device> devices)
        {
            foreach (KeyValuePair<string, Device> device in devices)
            {
                Console.WriteLine(Informer.StateToString(device.Value));
            }
            Console.WriteLine();
        }

        private static void AddDevice(List<string> commands)
        {
            if (commands.Count >= 1)
            {
                if (commands[0].ToLower() == "clock")
                {
                    commands.RemoveAt(0);
                    AddClock(commands);
                }
                else if (commands[0].ToLower() == "fridge")
                {
                    commands.RemoveAt(0);
                    AddFridge(commands);
                }
                else if (commands[0].ToLower() == "microwave")
                {
                    commands.RemoveAt(0);
                    AddMicrowave(commands);
                }
                else if (commands[0].ToLower() == "oven")
                {
                    commands.RemoveAt(0);
                    AddOven(commands);
                }
                else
                {
                    ErrorMessage(String.Join(" ", commands));
                }
            }
            else
            {
                InformUser("Необходимо указать тип, производителя и имя устройства.");
            }
        }

        private static void RemoveDevice(List<string> commands)
        {
            if (commands.Count >= 1)
            {
                if (!devices.ContainsKey(commands[0]))
                {
                    InformUser(nameNotFoundError);
                }
                else
                {
                    devices.Remove(commands[0]);
                }
            }
            else
            {
                InformUser("Имя устройства указано");
            }
        }

        private static void UseDevice(List<string> commands)
        {
            if (commands.Count >= 2)
            {
                if (!devices.ContainsKey(commands[0]))
                {
                    InformUser(nameNotFoundError);
                    return;
                }
                Device device = devices[commands[0]];
                if (commands[1].ToLower() == "on")
                {
                    device.TurnOn();   
                }
                else if (commands[1].ToLower() == "off")
                {
                    device.TurnOff();
                }
                else if (commands[1].ToLower() == "open")
                {
                    Open(device);
                }
                else if (commands[1].ToLower() == "close")
                {
                    Close(device );
                }
                else if (commands[1].ToLower() == "opencoldstore" || commands[1].ToLower() == "openc")
                {
                    OpenColdstore(device);
                }
                else if (commands[1].ToLower() == "openrefrigeratory" || commands[1].ToLower() == "openr")
                {
                    OpenRefrigeratory(device);
                }
                else if (commands[1].ToLower() == "closecoldstore" || commands[1].ToLower() == "closec")
                {
                    CloseColdstore(device);
                }
                else if (commands[1].ToLower() == "closerefrigeratory" || commands[1].ToLower() == "closer")
                {
                    CloseRefrigeratory(device);
                }
                else if (commands[1].ToLower() == "temperature" || commands[1].ToLower() == "temp")
                {
                    if (commands.Count < 3)
                    {
                        InformUser("Температура не указана!");
                    }
                    else
                    {
                        SetTemperature(device, commands[2]);
                    }
                }
                else if (commands[1].ToLower() == "temperaturecoldstore" || commands[1].ToLower() == "tempc")
                {
                    if (commands.Count < 3)
                    {
                        InformUser("Температура не указана!");
                    }
                    else
                    {
                        SetColdstoreTemperature(device, commands[2]);
                    }
                }
                else if (commands[1].ToLower() == "temperaturerefrigeratory" || commands[1].ToLower() == "tempr")
                {
                    if (commands.Count < 3)
                    {
                        InformUser("Температура не указана!");
                    }
                    else
                    {
                        SetRefrigeratoryTemperature(device, commands[2]);
                    }
                }
                else if (commands[1].ToLower() == "clock")
                {
                    if (commands.Count < 3)
                    {
                        InformUser("Время не указано!");
                    }
                    else
                    {
                        SetClock(device, commands[2]);
                    }
                }
                else if (commands[1].ToLower() == "timer")
                {
                    if (commands.Count < 3)
                    {
                        InformUser("Значение таймера не указано!");
                    }
                    else
                    {
                        SetTimer(device, commands[2]);
                    }
                }
                else if (commands[1].ToLower() == "start")
                {
                    TimerStart(device);
                }
                else if (commands[1].ToLower() == "stop")
                {
                    TimerStop(device);
                }

                else
                {
                    InformUser("Комманда " + commands[1] + " программой не поддерживается!");
                }
            }
            else
            {
                InformUser("Имя устройства или команда не указаны");
            }
        }

        private static void GlobalCommand(List<string> commands)
        {
            if (commands.Count >= 1)
            {
                if (commands[0].ToLower() == "on")
                {
                    foreach (KeyValuePair<string, Device> keyValuePair in devices)
                    {
                        keyValuePair.Value.TurnOn();
                    }
                }
                else if (commands[0].ToLower() == "off")
                {
                    foreach (KeyValuePair<string, Device> keyValuePair in devices)
                    {
                        keyValuePair.Value.TurnOff();
                    } 
                }
                else if (commands[0].ToLower() == "clock")
                {
                    if (commands.Count >= 2)
                    {
                        DateTime time;
                        try
                        {
                            time = DateTime.Parse(commands[1]);
                        }
                        catch (FormatException)
                        {
                            InformUser("Время указано в неправильном формате!");
                            return;
                        }
                        foreach (KeyValuePair<string, Device> keyValuePair in devices)
                        {
                            if (keyValuePair.Value is IClock)
                            {
                                ((IClock) keyValuePair.Value).CurrentTime = time;
                            }
                        }
                    }
                    else
                    {
                        InformUser("Время не указано!");
                    }
                }
                else
                {
                    ErrorMessage(String.Join(" ", commands));
                }
            }
            else
            {
                InformUser("Команда не указана!");
            }
        }


        // Добавление
        private static void AddClock(List<string> commands)
        {
            if (commands.Count >= 1)
            {
                if (!devices.ContainsKey(commands[0]))
                {
                    devices.Add(commands[0], new Clock(commands[0]));
                }
                else
                {
                    InformUser(existingNameError);
                }
            }
            else
            {
                InformUser(noNameError);
            }
        }
        private static void AddFridge(List<string> commands)
        {
            if (commands.Count >= 2)
            {
                if (commands[0].ToLower() == "samsung")
                {
                    if (!devices.ContainsKey(commands[1]))
                    {
                        devices.Add(commands[1], FridgeFactory.GetSamsung(commands[1]));
                    }
                    else
                    {
                        InformUser(existingNameError);
                    }
                }

                else if (commands[0].ToLower() == "indesit")
                {
                    if (!devices.ContainsKey(commands[1]))
                    {
                        devices.Add(commands[1], FridgeFactory.GetIndesit(commands[1]));
                    }
                    else
                    {
                        InformUser(existingNameError);
                    }
                }

                else if (commands[0].ToLower() == "atlant")
                {
                    if (!devices.ContainsKey(commands[1]))
                    {
                        devices.Add(commands[1], FridgeFactory.GetAtlant(commands[1]));
                    }
                    else
                    {
                        InformUser(existingNameError);
                    }
                }
                else
                {
                    InformUser("Холодильники производителя " + commands[0] + " программой не поддерживается!");
                }
            }
            else
            {
                InformUser(noNameOrFabricatorError);
            }
        }

        private static void AddMicrowave(List<string> commands)
        {
            if (commands.Count >= 2)
            {
                if (commands[0].ToLower() == "whirpool")
                {
                    if (!devices.ContainsKey(commands[1]))
                    {
                        devices.Add(commands[1], MicrowaveFactory.GetWhirpool(commands[1]));
                    }
                    else
                    {
                        InformUser(existingNameError);
                    }
                }

                else if (commands[0].ToLower() == "panasonic")
                {
                    if (!devices.ContainsKey(commands[1]))
                    {
                        devices.Add(commands[1], MicrowaveFactory.GetPanasonic(commands[1]));
                    }
                    else
                    {
                        InformUser(existingNameError);
                    }
                }

                else if (commands[0].ToLower() == "lg")
                {
                    if (!devices.ContainsKey(commands[1]))
                    {
                        devices.Add(commands[1], MicrowaveFactory.GetLg(commands[1]));
                    }
                    else
                    {
                        InformUser(existingNameError);
                    }
                }
                else
                {
                    InformUser("Микроволновки производителя " + commands[0] + " программой не поддерживается!");
                }
            }
            else
            {
                InformUser(noNameOrFabricatorError);
            }
        }

        private static void AddOven(List<string> commands)
        {
            if (commands.Count >= 2)
            {
                if (commands[0].ToLower() == "siemense")
                {
                    if (!devices.ContainsKey(commands[1]))
                    {
                        devices.Add(commands[1], OvenFactory.GetSiemense(commands[1]));
                    }
                    else
                    {
                        InformUser(existingNameError);
                    }
                }

                else if (commands[0].ToLower() == "electrolux")
                {
                    if (!devices.ContainsKey(commands[1]))
                    {
                        devices.Add(commands[1], OvenFactory.GetElectrolux(commands[1]));
                    }
                    else
                    {
                        InformUser(existingNameError);
                    }
                }

                else if (commands[0].ToLower() == "pyramida")
                {
                    if (!devices.ContainsKey(commands[1]))
                    {
                        devices.Add(commands[1], OvenFactory.GetPyramida(commands[1]));
                    }
                    else
                    {
                        InformUser(existingNameError);
                    }
                }
                else
                {
                    InformUser("Духовки производителя " + commands[0] + " программой не поддерживается!");
                }
            }
            else
            {
                InformUser(noNameOrFabricatorError);
            }
        }


        // Использование
        private static void Open(Device device)
        {
            if (device is IOpenable)
            {
                ((IOpenable)device).Open();
            }
            else
            {
                BadCommandErrorMessage(device.Name, "open");
            }
        }

        private static void Close(Device device)
        {
            if (device is IOpenable)
            {
                ((IOpenable)device).Close();
            }
            else
            {
                BadCommandErrorMessage(device.Name, "close");
            }
        }

        private static void OpenColdstore(Device device)
        {
            if (device is Fridge)
            {
                ((Fridge)device).OpenColdstore();
            }
            else
            {
                BadCommandErrorMessage(device.Name, "OpenColdstore");
            }
        }

        private static void OpenRefrigeratory(Device device)
        {
            if (device is Fridge)
            {
                ((Fridge)device).OpenRefrigeratory();
            }
            else
            {
                BadCommandErrorMessage(device.Name, "OpenRefrigeratory");
            }
        }

        private static void CloseColdstore(Device device)
        {
            if (device is Fridge)
            {
                ((Fridge)device).CloseColdstore();
            }
            else
            {
                BadCommandErrorMessage(device.Name, "CloseColdstore");
            }
        }

        private static void CloseRefrigeratory(Device device)
        {
            if (device is Fridge)
            {
                ((Fridge)device).CloseRefrigeratory();
            }
            else
            {
                BadCommandErrorMessage(device.Name, "CloseRefrigeratory");
            }
        }

        private static void SetTemperature(Device device, string userInput)
        {
            if (device is ITemperature)
            {
                double temperature = 0;
                if (!ParseTemperature(userInput, out temperature))
                {
                    InformUser("Температура указанна в неправильном формате!");
                }
                ((ITemperature)device).Temperature = temperature;
            }
            else
            {
                BadCommandErrorMessage(device.Name, "temperature");
            }
        }

        private static void SetColdstoreTemperature(Device device, string userInput)
        {
            if (device is Fridge)
            {
                double temperature = 0;
                if (!ParseTemperature(userInput, out temperature))
                {
                    InformUser("Температура указанна в неправильном формате!");
                }
                ((Fridge)device).ColdstoreTemperature = temperature;
            }
            else
            {
                BadCommandErrorMessage(device.Name, "coldstoreTemperature");
            }
        }

        private static void SetRefrigeratoryTemperature(Device device, string userInput)
        {
            if (device is Fridge)
            {
                double temperature = 0;
                if (!ParseTemperature(userInput, out temperature))
                {
                    InformUser("Температура указанна в неправильном формате!");
                }
                ((Fridge)device).RefrigeratoryTemperature = temperature;
            }
        }

        private static void SetClock(Device device, string userInput)
        {
            if (device is IClock)
            {
                try
                {
                    DateTime time = DateTime.Parse(userInput);
                    ((IClock) device).CurrentTime = time;

                }
                catch (FormatException)
                {
                    InformUser("Время указано в неправильном формате!");
                }
            }
            else
            {
                BadCommandErrorMessage(device.Name, "clock");
            }
        }

        private static void SetTimer(Device device, string userInput)
        {
            if (device is ITimer)
            {
                try
                {
                    TimeSpan time = TimeSpan.Parse(userInput);
                    ((ITimer) device).SetTimer(time);
                }
                catch (FormatException)
                {
                    InformUser("Время указано в неправильном формате!");
                }
            }
            else
            {
                BadCommandErrorMessage(device.Name, "timer");
            }
        }

        private static void TimerStart(Device device)
        {
            if (device is ITimer)
            {
                ((ITimer)device).Start();
            }
            else
            {
                BadCommandErrorMessage(device.Name, "start");
            }
        }

        private static void TimerStop(Device device)
        {
            if (device is ITimer)
            {
                ((ITimer)device).Stop();
            }
            else
            {
                BadCommandErrorMessage(device.Name, "stop");
            }
        }

        // Вспомогательные
        private static void InformUser(string msg = "")
        {
            if (msg.Length != 0)
            {
                Console.WriteLine(msg);
            }
            Console.WriteLine("Нажмите любую клавишу что бы продолжить");
            Console.ReadKey();
        }

        private static void ErrorMessage(string userInput)
        {
            Console.WriteLine();
            Help();
            Console.WriteLine();
            InformUser("Некорректная комманда. Ошибка после '" + userInput + "'. Пожалуйста, проверьте синтаксис.");
        }

        private static void BadCommandErrorMessage(string device, string command)
        {
            Console.WriteLine();
            Help();
            Console.WriteLine();
            InformUser("Некорректная комманда. Устройство '" + device + "' комманду '" + command +
                "' Не поддерживает! Пожалуйста, проверьте синтаксис.");
        }

        private static bool ParseTemperature(string userInput, out double temperature)
        {
            temperature = 0;
            try
            {
                temperature = Double.Parse(userInput);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
