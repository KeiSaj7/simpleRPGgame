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
        public List<Destination> Directions { get; set; }
        public List<Item> Items { get; set; }
        // public Dictionary<string, int> Items { get; set; }
        public Dictionary<string, int> RewardOdds { get; set; }
        public Dictionary<int, Enemy> Enemies { get; set; }

        public virtual void StartLevel(Character character)
        {
            Console.Clear();
            Choices(character);
        }
        public virtual void Choices(Character character)
        {
            Console.Clear();
            Console.WriteLine($"You have entered the {this.Name}. What would you like to do?");
            Console.WriteLine("1. Fight");
            Console.WriteLine("2. Check Inventory");
            Console.WriteLine("3. Check Stats");
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 4)
            {
                Console.WriteLine("Invalid choice, please try again.");
            }
            Console.Clear();
            switch (choice)
            {
                case 1:
                    Fight(character);
                    break;
                case 2:
                    CheckInventory(character);
                    Choices(character);
                    break;
                case 3:
                    character.PrintStats();
                    Console.WriteLine("Press any key to go back...");
                    Console.ReadKey();
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
            while (true)
            {   
                var HPs = CalcHealth(character);
                Console.WriteLine(this.Enemies[Stage].Name + $" {PrintHealthBar(HPs.Item2)} {(HPs.Item2 < 0? 0 : HPs.Item2)}%               " + character.GetName() + $" {PrintHealthBar(HPs.Item1)} {(HPs.Item1 < 0? 0 : HPs.Item1)}%");
                FightChoice(character);
                if (this.Enemies[Stage].CurrentHealth <= 0)
                {
                    HPs = CalcHealth(character);
                    Console.WriteLine(this.Enemies[Stage].Name + $" {PrintHealthBar(HPs.Item2)} {(HPs.Item2 < 0 ? 0 : HPs.Item2)}%               " + character.GetName() + $" {PrintHealthBar(HPs.Item1)} {(HPs.Item1 < 0 ? 0 : HPs.Item1)}%");
                    Console.WriteLine("You have slain an enemy!");
                    GetReward(character);
                    DropGold(character, this.Enemies[Stage]);
                    this.Stage++;
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    if (this.Stage > this.Enemies.Count)
                    {
                        Console.WriteLine($"You have defeated the {this.Enemies[Stage - 1].Name} and now {this.Name} is safe! Congratulations!\n");
                        Console.WriteLine($"Choose your next destination:");
                        for (int i = 0; i < this.Directions.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}.{this.Directions[i].Name}");
                        }
                        int choice;
                        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > this.Directions.Count)
                        {
                            Console.WriteLine("Invalid choice, please try again.");
                        }
                        this.GetDestination(choice - 1).StartLevel(character);
                        Console.ReadKey();
                    }
                    else break;
                }
                this.Enemies[Stage].AttackEnemy(character, character.GetName(), character.GetDefense());
                if (character.GetCurrHealth() <= 0)
                {
                    HPs = CalcHealth(character);
                    Console.WriteLine(this.Enemies[Stage].Name + $" {PrintHealthBar(HPs.Item2)} {(HPs.Item2 < 0 ? 0 : HPs.Item2)}%               " + character.GetName() + $" {PrintHealthBar(HPs.Item1)} {(HPs.Item1 < 0 ? 0 : HPs.Item1)}%");
                    Console.WriteLine($"You have been defeated by {this.Enemies[Stage].Name}! What a shame...\nTHE END");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
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
                    character.UseItem();
                    break;
            }
        }
        public void CheckInventory(Character character)
        {
            bool exit = true;
            while (exit)
            {
                character.ShowInventory();
                exit = character.UseItem();
            }

        }
        public (int,int,int) CalcHealth(Character character)
        {
            int currHealth = character.GetCurrHealth();
            int charHP = (currHealth * 100) / character.GetMaxHealth();
            int enemyHP = (this.Enemies[Stage].CurrentHealth * 100) / this.Enemies[Stage].Health;
            return (charHP,enemyHP,currHealth);
        }
        public string PrintHealthBar(int Health)
        {
            int bars = Health / 10;
            string HealthBar = new string('█', bars < 1? 1 : bars );
            return HealthBar;
        }
        public void GetReward(Character character)
        {
            int dropChance = new Random().Next(1, 101);
            if (dropChance > 50)
            {
                return;
            }
            Console.WriteLine("Your enemy dropped an item...");
            var rewards = new List<string>();
            foreach (var item in this.Items)
            {
                for (int i = 0; i < item.Odd; i++)
                {
                    rewards.Add(item.Name);
                }
            }   
            string random = rewards[new Random().Next(rewards.Count)];
            Item reward = this.Items.Find(x => x.Name == random);
            character.AddItem(reward);
            Console.WriteLine($"You have received {reward.Name}!");
        }
        public void DropGold(Character character, Enemy enemy)
        {
            int gold = enemy is Boss? new Random().Next(0, 6) : new Random().Next(0, 4);
            character.AddGold(gold);
            Console.WriteLine($"You have received {gold} gold for that fight!\n");
        }
        public Destination GetDestination(int idx)
        {
            return this.Directions[idx];
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
        public Enemy(string name, int Health, int attack, int defense, int critChance ) { 
            this.Name = name;   
            this.Health = Health;
            this.CurrentHealth = Health;
            this.Attack = attack;
            this.Defense = defense;
            this.CritChance = critChance;
        }
        public virtual void AttackEnemy(Character character, string enemyName, int enemyDef)
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
        public Boss(string name, int Health, int attack, int defense, int critChance, Dictionary<string, Tuple<int, int>> specialAbility) : base(name, Health, attack, defense, critChance)
        {
            this.SpecialAbility = specialAbility;
        }
        public override void AttackEnemy(Character character, string enemyName, int enemyDef)
        {
            int damage;
            if (new Random().Next(1, 101) <= this.SpecialAbility.First().Value.Item2)
            {
                damage = this.SpecialAbility.First().Value.Item1 - enemyDef;
                character.SetCurrHealth(damage);
                Console.WriteLine($"{this.Name} used {this.SpecialAbility.First().Key} and dealt {damage} damage!");
                return;
            }
            damage = this.Attack - enemyDef;
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
