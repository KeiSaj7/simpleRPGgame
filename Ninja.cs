using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleConsoleAppGame
{
    public class Ninja : Character
    {
        public Ninja(string name)
        {
            this.Name = name;
            this.Health = 80;
            this.CurrentHealth = this.Health;
            this.CritChance = 25;
            this.MainHand = "Daggers";
            this.Armor = "NinjaArmor";
            this.Inventory = new List<Item>()
            {
                new Item("Daggers", 9, 1, ItemType.Weapon, null),
                new Item("NinjaArmor", 5, 1, ItemType.Armor, null)
            };
            this.Attack = this.Inventory.Find(x => x.Name == MainHand).Value;
            this.Defense = this.Inventory.Find(x => x.Name == Armor).Value;
        }
    }
}
