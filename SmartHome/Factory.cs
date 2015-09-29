using System;
using System.Collections.Generic;

namespace SmartHome
{
    public static class Factory
    {
        public static Fridge Get2CamFridge()
        {
            List<FridgeModule> modules = new List<FridgeModule>();
            modules.Add(new Coldstore(new Lamp(25)));
            modules.Add(new Refrigeratory());

            Fridge fridge = new Fridge(modules);
            return fridge;
        }

        public static Microwave GetMicrowaveOven()
        {
            Microwave oven = new Microwave(new Lamp(10));
            oven.OperationDone += source =>
            {
                Console.Beep(3000, 500);
                Console.WriteLine("Ваша тарелка нагрета");
            };
            return oven;
        }

        public static Oven GetOven()
        {
            Oven oven = new Oven(new Lamp(25));
            oven.OperationDone += source =>
            {
                Console.Beep(2000, 1000);
                Console.WriteLine(source.Name + ": Выполнение операции закончено.");
            };
            return oven;
        }
    }
}
