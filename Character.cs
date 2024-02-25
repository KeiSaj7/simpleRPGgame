using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleConsoleAppGame
{
    public abstract class Character
    {
        protected string Name { get; set; }
        protected int Health { get; set; }
        protected int CurrentHealth { get; set; }   
        protected int Attack { get; set; }
        protected int Defense { get; set; }
        protected int CritChance { get; set; }
        protected string MainHand { get; set; }
        protected string Armor { get; set; }
        protected Dictionary<string, int>? Inventory { get; set; }

        public static Character CreateCharacter(string name, Race race)
        {
            switch (race)
            {
                case Race.Human:
                    return new Human(name);
                case Race.Elf:
                    return new Elf(name);
                default:
                    throw new ArgumentException("Invalid race", nameof(race));
            }
        }
        public (string,int,int,int,int) GetStats()
        {
            return (this.Name, this.Health, this.Attack, this.Defense, this.CritChance);
        }
        public int GetCurrHealth()
        {
            return this.CurrentHealth;
        }   
        public int SetCurrHealth(int damage)
        {
            return this.CurrentHealth -= damage;
        }
        public void AddItem(string item, int value)
        {
            Inventory.Add(item, value);
        }   
        public void PrintStats()
        {
            Console.WriteLine($"Name: {Name}\nMaxHealth: {Health}\nCurrentHealth: {CurrentHealth}\nAttack: {Attack}\nDefense: {Defense}\nCritChance: {CritChance}%\n");
        }
        public void ShowInventory()
        {
            string inventory = "Inventory:\n";
            foreach (var item in Inventory)
            {
                inventory += $"> {item.Key}: {item.Value}\n";
            }
            Console.WriteLine(inventory);
        }
        public void AttackEnemy(Enemy enemy)
        {
            int damage = this.Attack - enemy.Defense;
            if (damage <= 0)
            {
                Console.WriteLine($"{this.Name} attack blocked!");
                return;
            }
            if (new Random().Next(1, 101) <= this.CritChance)
            {
                damage *= 2;
            }
            enemy.CurrentHealth -= damage;
            Console.WriteLine($"{this.Name} attacked {enemy.Name} for {damage} damage!");
        }
    }
}
