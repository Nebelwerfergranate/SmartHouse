using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHome
{
    public static class Factory
    {
        public static Fridge Get2CamFridge()
        {
            Fridge fridge = new Fridge(new IFridgeModule[2]
            {
                new Coldstore(new Lamp(25)),
                new Refrigeratory() 
            });
            return fridge;
        }

        public static Microwave GetMicrowaveOven()
        {
            Microwave oven = new Microwave(new Lamp(10));
            oven.OnReadyMessage = "Ваша тарелка нагрета!";
            oven.IsReady += msg =>
            {
                Console.Beep(3000, 500);
                Console.WriteLine(msg);
            };
            return oven;
        }
    }
}
