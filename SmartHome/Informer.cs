using System;
using System.Collections;
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
            string info = "";
            info += GetAllInfo(device);

            // Устройства, состоящие из произвольного количества модулей, должны поддерживать
            // интерфейс IEnumerable<Device>, что бы корректно отображаться в меню.
            if (device is IEnumerable<Device>)
            {
                info += "***********\n";
                info += "Модули:\n";
                info += "\n";
                foreach (Device module in (IEnumerable<Device>)device)
                {
                    info += GetAllInfo(module);
                    info += "-----\n";
                }
                info += "***********\n";
                return info;
            }
            return info;
        }
        private static string GetAllInfo(Device device)
        {
            string info = "";
            info += GetBaseInfo(device);
            info += GetOpenableInfo(device);
            info += GetBacklightInfo(device);
            info += GetTemperatureInfo(device);
            info += GetClockInfo(device);
            info += GetTimerInfo(device);
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
                info += "Мощность лампочки: " + ((IBacklight)device).LampPower + " Вт\n";
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
                info += "Установка термостата: " + ((ITemperaturable)device).Temperature + " градусов\n";
            }
            return info;
        }

        private static string GetClockInfo(Device device)
        {
            string info = "";
            if (device is IClock)
            {
                info += "Текущее время: " + ((IClock)device).CurrentTime.ToLongTimeString() + "\n";
            }
            return info;
        }

        private static string GetTimerInfo(Device device)
        {
            string info = "";
            if (device is ITimer)
            {
                info += "Статус: ";
                if (((ITimer) device).IsRunning)
                {
                    info += "Выполняет задачу\n";
                }
                else
                {
                    info += "Задач нет\n";
                }
            }
            return info;
        }
    }
}
