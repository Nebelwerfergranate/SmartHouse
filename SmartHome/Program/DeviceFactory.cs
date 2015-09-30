using System;
using System.Collections.Generic;

namespace SmartHome
{
    public static class DeviceFactory
    {
        public static Fridge GetSamsungFridge()
        {
            Fridge fridge = new Fridge(new Coldstore( 254, new Lamp(25)), new Refrigeratory(92));
            return fridge;
        }

        public static Microwave GetWhirpoolMicrowave()
        {
            Microwave oven = new Microwave(20, new Lamp(10));
            oven.OperationDone += source =>
            {
                Console.Beep(3000, 500);
                Console.WriteLine("Ваша тарелка нагрета");
            };
            return oven;
        }

        public static Oven GetSiemenseOven()
        {
            Oven oven = new Oven(67, new Lamp(25));
            oven.OperationDone += source =>
            {
                Console.Beep(2000, 1000);
                Console.WriteLine(source.Name + ": Выполнение операции закончено.");
            };
            return oven;
        }
    }
}
