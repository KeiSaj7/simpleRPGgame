using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleConsoleAppGame
{
    public class Human: Character
    {
        public Human(string name)
        {
            this.Name = name;
            this.Health = 100;
            this.CurrentHealth = 100;
            this.CritChance = 10;
            this.MainHand = "SilverSword";
            this.Armor = "LeatherArmor";
            this.Inventory = new Dictionary<string, int>() { {"SilverSword", 10}, { "LeatherArmor", 5} };
            this.Attack = this.Inventory[this.MainHand];
            this.Defense = this.Inventory[this.Armor];
        }
    }
}
