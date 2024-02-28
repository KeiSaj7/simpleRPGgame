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
            var mainHandItem = new Item("SilverSword", 10, 1, ItemType.Weapon, null);
            var armorItem = new Item("LeatherArmor", 5, 1, ItemType.Armor, null);
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
