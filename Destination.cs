using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleConsoleAppGame
{
    public abstract class Destination
    {
        public string Name { get; set; }
        public Dictionary<string, string> Directions { get; set; }
        public Dictionary<string, string> Items { get; set; }
        public Dictionary<int, Enemy> Enemies { get; set; }

        public void StartLevel(Character character)
        {
            Console.Clear();
            Console.WriteLine($"You have entered the {this.Name}. What would you like to do?");
            Choices(character);
        }
        public void Choices(Character character)
        {
            Console.WriteLine("1. Fight");
            Console.WriteLine("2. Check Inventory");
            Console.WriteLine("3. Check Stats");
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 4)
            {
                Console.WriteLine("Invalid choice, please try again.");
            }
            switch (choice)
            {
                case 1:
                    Console.Clear();
                    Fight(character);
                    break;
                case 2:
                    Console.Clear();
                    character.ShowInventory();
                    break;
                case 3:
                    Console.Clear();
                    character.PrintStats();
                    break;
            }
            Console.WriteLine("Press any key to go back...");
            Console.ReadKey();
            Console.Clear();
            //Choices(character);
        }

        public void Fight(Character character)
        {
            Console.WriteLine($"You have encountered the {this.Enemies[1].Name}, prepare yourself to fight!");
            Console.WriteLine("Press any key to start the battle...");
            Console.ReadKey();
            Console.Clear();
            var characterStats = character.GetStats();
            while (true)
            {   
                int currHealth = character.GetCurrHealth();
                int charHP = (currHealth * 100) / characterStats.Item2;
                int enemyHP = (this.Enemies[1].CurrentHealth * 100) / this.Enemies[1].Health;
                Console.WriteLine(this.Enemies[1].Name + $" {PrintHealthBar(enemyHP)} {enemyHP}%               " +characterStats.Item1+ $" {PrintHealthBar(charHP)} {charHP}%");
                FightChoice(character);
                this.Enemies[1].AttackEnemy(character, characterStats.Item1, characterStats.Item4);
                if (this.Enemies[1].CurrentHealth <= 0)
                {
                    Console.WriteLine("You have slain an enemy!");
                    break;
                }
                if(characterStats.Item3 <= 0)
                {
                    Console.WriteLine($"You have been defeated by {this.Enemies[1].Name}! What a shame...\n THE END");
                    break;
                }
                
            }
        }
        public void FightChoice(Character character)
        {
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Use Item");
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
            {
                Console.WriteLine("Invalid choice, please try again.");
            }
            switch (choice)
            {
                case 1:
                    Console.Clear();
                    character.AttackEnemy(this.Enemies[1]);
                    break;
                case 2:
                    Console.Clear();
                    character.ShowInventory();
                    break;
            }
        }
        public string PrintHealthBar(int health)
        {
            int bars = health / 10;
            string healthBar = new string('█', bars < 1? 1 : bars );
            return healthBar;
        }
    }
    public class Enemy
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int CurrentHealth { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int CritChance { get; set; }
        public Enemy(string name, int health, int attack, int defense, int critChance ) { 
            this.Name = name;   
            this.Health = health;
            this.CurrentHealth = health;
            this.Attack = attack;
            this.Defense = defense;
            this.CritChance = critChance;
        }
        public void AttackEnemy(Character character, string enemyName, int enemyDef)
        {
            int damage = this.Attack - enemyDef;
            if (damage <= 0)
            {
                Console.WriteLine($"{this.Name} attack blocked!");
                return;
            }
            if (new Random().Next(1, 101) <= this.CritChance)
            {
                damage *= 2;
            }
            character.SetCurrHealth(damage);
            Console.WriteLine($"{this.Name} attacked {enemyName} for {damage} damage!");
        }
    }
}
