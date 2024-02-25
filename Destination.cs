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
        public int Stage { get; set; }
        public Dictionary<string, string> Directions { get; set; }
        public Dictionary<string, int> Items { get; set; }
        public Dictionary<string, int> RewardOdds { get; set; }
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
                    Console.WriteLine("Press any key to go back...");
                    Console.ReadKey();
                    Console.Clear();
                    Choices(character);
                    break;
                case 3:
                    Console.Clear();
                    character.PrintStats();
                    Console.WriteLine("Press any key to go back...");
                    Console.ReadKey();
                    Console.Clear();
                    Choices(character);
                    break;
            }

            //Choices(character);
        }

        public void Fight(Character character)
        {
            Console.WriteLine($"You have encountered the {this.Enemies[Stage].Name}, prepare yourself to fight!\n");
            Console.WriteLine("Press any key to start the battle...");
            Console.ReadKey();
            Console.Clear();
            var characterStats = character.GetStats();
            while (true)
            {   
                int currHealth = character.GetCurrHealth();
                if (this.Enemies[Stage].CurrentHealth <= 0)
                {
                    if (this.Stage == 2) GetReward(character);
                    this.Stage++;
                    Console.WriteLine("You have slain an enemy!\n");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                }
                if (currHealth <= 0)
                {
                    Console.WriteLine($"You have been defeated by {this.Enemies[Stage].Name}! What a shame...\nTHE END");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                int charHP = (currHealth * 100) / characterStats.Item2;
                int enemyHP = (this.Enemies[Stage].CurrentHealth * 100) / this.Enemies[Stage].Health;
                Console.WriteLine(this.Enemies[Stage].Name + $" {PrintHealthBar(enemyHP)} {enemyHP}%               " +characterStats.Item1+ $" {PrintHealthBar(charHP)} {charHP}%");
                FightChoice(character);
                this.Enemies[Stage].AttackEnemy(character, characterStats.Item1, characterStats.Item4);
                
            }
            Choices(character);
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
                    character.AttackEnemy(this.Enemies[Stage]);
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
        public void GetReward(Character character)
        {
            Console.WriteLine("Your enemy dropped an item...");
            var reward = new List<string>();
            foreach (var item in this.RewardOdds)
            {
                for (int i = 0; i < item.Value; i++)
                {
                    reward.Add(item.Key);
                }
            }   
            string random = reward[new Random().Next(reward.Count)];
            character.AddItem(random, this.Items[random]);
            Console.WriteLine($"You have received {random}!");
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
    public class Boss: Enemy
    {   
        public Dictionary<string, Tuple<int,int>> SpecialAbility { get; set; } // <Name, <Damage, Chance>>
        public Boss(string name, int health, int attack, int defense, int critChance, Dictionary<string, Tuple<int, int>> specialAbility) : base(name, health, attack, defense, critChance)
        {
            this.SpecialAbility = specialAbility;
        }
    }
}
