using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleConsoleAppGame
{
    public class Elf: Character
    {
        public Elf(string name)
        {
            this.Name = name;
            this.Health = 95;
            this.CurrentHealth = 95;
            this.CritChance = 15;
            this.MainHand = "WoodenBow";
            this.Armor = "BlackCoat";
            this.Inventory = new Dictionary<string, int>() { { "WoodenBow", 11 }, { "BlackCoat", 4 } };
            this.Attack = this.Inventory[this.MainHand];
            this.Defense = this.Inventory[this.Armor];

        }
    }
}
