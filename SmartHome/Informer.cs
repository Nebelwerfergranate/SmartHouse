using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome
{
    public static class Informer
    {
        public static string StateToString(Device device)
        {
            string info = GetBaseInfo(device);
            info += GetBacklightInfo(device);
            info += GetOpenableInfo(device);
            info += GetTemperatureInfo(device);
            info += ClockInfo(device);

            if (device is Fridge)
            {
                info += FridgeMolulesInfo((Fridge)device);
            }
            return info;
        }

        private static string FridgeMolulesInfo(Fridge fridge)
        {
            string info = "";
            info += "***********\n";
            info += "Модули:\n";
            info += "\n";
            foreach (IFridgeModule module in fridge.Modules)
            {
                if (module is Device)
                {
                    info += GetBaseInfo((Device)module);
                    info += GetBacklightInfo((Device)module);
                    info += GetOpenableInfo((Device)module);
                    info += GetTemperatureInfo((Device) module);
                }
                info += "-----\n";
            }
            info += "***********\n";
            return info;
        }

        private static string GetBaseInfo(Device device)
        {
            string info = "";

            info += "Имя устройства: " + device.Name + "\n";
            if (device.IsOn)
            {
                info += "Cостояние: включен\n";
            }
            else
            {
                info += "Состояние: выключен\n";
            }
            return info;
        }

        private static string GetBacklightInfo(Device device)
        {
            string info = "";
            if (device is IBacklight)
            {
               
                if (((IBacklight)device).IsHighlighted)
                {
                    info += "Подсветка: включена\n";
                }
                else
                {
                    info += "Подсветка: выключена\n";
                }
                info += "Мощность лампочки: " + ((IBacklight)device).GetLampPower() + " Вт\n";
            }
            return info;
        }

        private static string GetOpenableInfo(Device device)
        {
            string info = "";
            if (device is IOpenable)
            {
                 if (((IOpenable)device).IsOpen)
                {
                    info += "Дверца: открыта\n";
                }
                else
                {
                    info += "Дверца: закрыта\n";
                }   
            }
            return info;
        }

        private static string GetTemperatureInfo(Device device)
        {
            string info = "";
            if (device is ITemperaturable)
            {
                info += "Температура: " + ((ITemperaturable)device).Temperature + " градусов\n";
            }
            return info;
        }

        private static string ClockInfo(Device device)
        {
            string info = "";
            if (device is IClock)
            {
                info += "Текущее время: " + ((IClock)device).CurrentTime.ToLongTimeString() + "\n";
            }
            return info;
        }
    }
}
