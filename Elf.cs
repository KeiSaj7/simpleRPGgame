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
            this.CurrentHealth = this.Health;
            this.CritChance = 15;
            var mainHandItem = new Item("WoodenBow", 11, 1, ItemType.Weapon, null);
            var armorItem = new Item("BlackCoat", 4, 1, ItemType.Armor, null);
            this.Inventory = new List<Item>()
            {
                mainHandItem,
                armorItem
            };  
            this.MainHand = mainHandItem.Id;
            this.Armor = armorItem.Id;
            this.Attack = this.Inventory.Find(x => x.Id == MainHand).Value;
            this.Defense = this.Inventory.Find(x => x.Id == Armor).Value;
        }
    }
}
