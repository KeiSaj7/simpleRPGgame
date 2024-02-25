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
            this.Inventory = new List<Item>() { new Item("SilverSword", 10, ItemType.Weapon, null), new Item("LeatherArmor", 5, ItemType.Armor, null) };
            //this.Inventory = new Dictionary<string, int>() { {"SilverSword", 10}, { "LeatherArmor", 5} };
            this.Attack = this.Inventory.Find(x => x.Name == MainHand).Value;
            this.Defense = this.Inventory.Find(x => x.Name == Armor).Value;
        }
    }
}
