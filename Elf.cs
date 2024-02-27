﻿using System;
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
            this.MainHand = "WoodenBow";
            this.Armor = "BlackCoat";
            this.Inventory = new List<Item>() 
            {
                new Item("WoodenBow", 11, 1, ItemType.Weapon, null),
                new Item("BlackCoat", 4, 1, ItemType.Armor, null) 
            };
            this.Attack = this.Inventory.Find(x => x.Name == MainHand).Value;
            this.Defense = this.Inventory.Find(x => x.Name == Armor).Value;

        }
    }
}
