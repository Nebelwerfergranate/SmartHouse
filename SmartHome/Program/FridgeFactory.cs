﻿namespace SmartHome
{
    public static class FridgeFactory
    {
        public static Fridge GetSamsung(string name)
        {
            Fridge fridge = new Fridge(name, new Coldstore(254, new Lamp(15)), new Refrigeratory(92));
            return fridge;
        }

        public static Fridge GetIndesit(string name)
        {
            Fridge fridge = new Fridge(name, new Coldstore(233, new Lamp(15)), new Refrigeratory(85));
            return fridge;
        }

        public static Fridge GetAtlant(string name)
        {
            Fridge fridge = new Fridge(name, new Coldstore(202, new Lamp(15)), new Refrigeratory(70));
            return fridge;
        }
    }
}
