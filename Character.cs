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
        //protected Dictionary<string, int>? Inventory { get; set; }
        protected List<Item> Inventory { get; set; }

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
        public string GetName()
        {
            return this.Name;
        }
        public int GetMaxHealth()
        {
            return this.Health;
        }
        public int GetDefense()
        {
            return this.Defense;
        }
        public int GetCurrHealth()
        {
            return this.CurrentHealth;
        }   
        public int SetCurrHealth(int damage)
        {
            return this.CurrentHealth -= damage;
        }
        public void AddItem(Item item)
        {
            Inventory.Add(item);
        }   
        public void PrintStats()
        {
            Console.WriteLine($"Name: {Name}\nMaxHealth: {Health}\nCurrentHealth: {CurrentHealth}\nAttack: {Attack}\nDefense: {Defense}\nCritChance: {CritChance}%\n");
        }
        public void ShowInventory()
        {
            string inventory = "Inventory:\n";
            for(int i = 0; i < Inventory.Count; i++)
            {
                if(Inventory.ElementAt(i).Name == this.MainHand) inventory += $"{i + 1}. {Inventory.ElementAt(i).Name}: {Inventory.ElementAt(i).Value} [Main Hand]\n";
                else if(Inventory.ElementAt(i).Name == this.Armor) inventory += $"{i + 1}. {Inventory.ElementAt(i).Name}: {Inventory.ElementAt(i).Value} [Armor]\n";
                else
                {
                    inventory += $"{i + 1}. {Inventory.ElementAt(i).Name}: {Inventory.ElementAt(i).Value}\n";

                }
            }

            Console.WriteLine(inventory);
            Console.Write("Select an item to use/equip (type 0 to leave): ");
            int chosenIdx;
            while (!int.TryParse(Console.ReadLine(), out chosenIdx) || chosenIdx < 0 || chosenIdx > Inventory.Count)
            {
                Console.WriteLine("Invalid choice, please try again.");
            }
            if(chosenIdx == 0)
            {
                return;
            }
            Item selectedItem = Inventory[chosenIdx - 1];
            selectedItem.Use(this);
        }
        public void RemoveItem(Item item)
        {
               Inventory.Remove(item);
        }
        public void EquipArmor(Item item)
        {
            this.Armor = item.Name;
            this.Defense = item.Value;
        }
        public void EquipWeapon(Item item)
        {
            this.MainHand = item.Name;
            this.Attack = item.Value;
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
