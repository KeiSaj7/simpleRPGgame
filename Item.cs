﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleConsoleAppGame
{
    public class Item
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public ItemType Type { get; set; }
        public int? Odd { get; set; }
        public Item(string name, int value, ItemType type, int? odd)
        {
            this.Name = name;
            this.Value = value;
            this.Type = type;
            this.Odd = odd;
        }
        public void Use(Character character)
        {
            if (this.Type == ItemType.Potion)
            {
                Console.WriteLine($"You have used a {this.Name}.");
                character.RemoveItem(this);
                character.SetCurrHealth(-this.Value);
            }
            else if (this.Type == ItemType.Armor)
            {
                Console.WriteLine($"You have equipped a {this.Name}.");
                character.EquipArmor(this);

            }
            else if (this.Type == ItemType.Weapon)
            {
                Console.WriteLine($"You have equipped a {this.Name}.");
                character.EquipWeapon(this);
            }
        }
    }
    public enum ItemType
    {
        Potion,
        Armor,
        Weapon
    }
}