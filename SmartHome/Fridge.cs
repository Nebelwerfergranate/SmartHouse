using System.Collections;
using System.Collections.Generic;

namespace SmartHome
{
    public class Fridge : Device, IEnumerable<FridgeModule>
    {
        // Поля
        private readonly List<FridgeModule> modules; 


        // Конструкторы
        public Fridge(List<FridgeModule> modules)
        {
            this.modules = modules;
            Name = "Холодильник";
        }


        // Методы
        public override void TurnOn()
        {
            base.TurnOn();
            for (int i = 0; i < modules.Count; i++)
            {
                modules[i].TurnOn();
            }
        }
        public override void TurnOff()
        {
            base.TurnOff();
            for (int i = 0; i < modules.Count; i++)
            {
                modules[i].TurnOff();
            }
        }

        public FridgeModule this[byte index]
        {
            get { return modules[index]; }
        }

        public IEnumerator<FridgeModule> GetEnumerator()
        {
            return modules.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return modules.GetEnumerator();
        }
    }
}
