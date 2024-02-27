using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace SimpleConsoleAppGame
{
    public class Item
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public int Price { get; set; }
        public ItemType Type { get; set; }
        public int? Odd { get; set; }
        public Item(string name, int value, int price, ItemType type, int? odd)
        {
            this.Name = name;
            this.Value = value;
            this.Price = price;
            this.Type = type;
            this.Odd = odd;
        }
        public void Use(Character character)
        {
            Console.Clear();
            if (this.Type == ItemType.Potion)
            {   
                Console.WriteLine($"You have used a {this.Name}.\n");
                character.RemoveItem(this);
                character.SetCurrHealth(-this.Value);
            }
            else if (this.Type == ItemType.Armor)
            {
                Console.WriteLine($"You have equipped a {this.Name}.\n");
                character.EquipArmor(this);

            }
            else if (this.Type == ItemType.Weapon)
            {
                Console.WriteLine($"You have equipped a {this.Name}.\n");
                character.EquipWeapon(this);
            }
            else if (this.Type == ItemType.CritBuff)
            {
                Console.WriteLine($"You have used a {this.Name} and gained +{this.Value} Crit %.\n");
                character.IncreaseCritChance(this.Value);
            }
        }
    }
    public enum ItemType
    {
        Potion,
        Armor,
        Weapon,
        CritBuff
    }
}
