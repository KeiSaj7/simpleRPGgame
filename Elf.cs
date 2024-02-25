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
            this.Inventory = new List<Item>() {new Item("WoodenBow", 11, ItemType.Weapon, null), new Item("BlackCoat", 4, ItemType.Armor, null) };
            //this.Inventory = new Dictionary<string, int>() { { "WoodenBow", 11 }, { "BlackCoat", 4 } };
            this.Attack = this.Inventory.Find(x => x.Name == MainHand).Value;
            this.Defense = this.Inventory.Find(x => x.Name == Armor).Value;

        }
    }
}
