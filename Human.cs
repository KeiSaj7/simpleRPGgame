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
            this.CurrentHealth = this.Health;
            this.CritChance = 10;
            this.MainHand = "SilverSword";
            this.Armor = "LeatherArmor";
            this.Inventory = new List<Item>() 
            {
                new Item("SilverSword", 10, 1, ItemType.Weapon, null), 
                new Item("LeatherArmor", 5, 1, ItemType.Armor, null) 
            };
            this.Attack = this.Inventory.Find(x => x.Name == MainHand).Value;
            this.Defense = this.Inventory.Find(x => x.Name == Armor).Value;
        }
    }
}
