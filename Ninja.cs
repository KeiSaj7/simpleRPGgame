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
            this.Health = 1185;
            this.CurrentHealth = this.Health;
            this.CritChance = 30;
            var mainHandItem = new Item("Daggers", 9, 1, ItemType.Weapon, null);
            var armorItem = new Item("NinjaArmor", 5, 1, ItemType.Armor, null);
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
