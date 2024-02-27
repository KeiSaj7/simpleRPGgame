﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleConsoleAppGame
{
    public class DarkForest: Destination
    {
        public DarkForest()
        {
            this.Name = "Dark Forest";
            this.Stage = 1;
            this.Directions = new List<Destination>() {
                new NorthCastle(),
                new Tavern(this, new List<Item>()
                {
                    new Item("GoldenSword",13,5,ItemType.Weapon,null),
                    new Item("GoldenArmor",7,6,ItemType.Armor,null),
                    new Item("SilverBow",14,8,ItemType.Weapon,null),
                    new Item("DwarvenArmor",10,11,ItemType.Armor,null),
                })
            };
            this.Items = new List<Item>() 
            {
                new Item("HealthPotion", 20, 2, ItemType.Potion, 60),
                new Item("ChainArmor", 6, 4, ItemType.Armor, 25),
                new Item("SilverBow", 14, 8, ItemType.Weapon, 10),
                new Item("GoldenSword", 15, 10, ItemType.Weapon, 5),
                new Item("CritBlessing", 10, 20, ItemType.CritBuff, 1) 
            };
            this.Enemies = new Dictionary<int, Enemy>()
            {
                { 1, new Enemy("Goblin",35,8,2,5) },
                { 2, new Enemy("Orc",40,9,3,10) },
                { 3, new Boss("Dark Wizard",40,17,2,15,new Dictionary<string, Tuple<int, int>>{ { "FireBall", new Tuple<int, int>(26, 20) } }) }
            };
        }
    } 
}
    