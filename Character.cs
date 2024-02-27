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
        protected int Gold { get; set; }    
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
                case Race.Ninja:
                    return new Ninja(name);
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
        public void SetCurrHealth(int damage)
        {
            this.CurrentHealth -= damage;
            if(this.CurrentHealth > this.Health)
            {
                this.CurrentHealth = this.Health;
            }
        }
        public void IncreaseCritChance(int value)
        {
            this.CritChance += value;
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
                if(Inventory.ElementAt(i).Name == this.MainHand) inventory += $"{i + 1}. {Inventory.ElementAt(i).Name}: {Inventory.ElementAt(i).Value} | Price: {Inventory.ElementAt(i).Price} gold [Main Hand]\n";
                else if(Inventory.ElementAt(i).Name == this.Armor) inventory += $"{i + 1}. {Inventory.ElementAt(i).Name}: {Inventory.ElementAt(i).Value} | Price: {Inventory.ElementAt(i).Price} gold [Armor]\n";
                else
                {
                    inventory += $"{i + 1}. {Inventory.ElementAt(i).Name}: {Inventory.ElementAt(i).Value} | Price: {Inventory.ElementAt(i).Price} gold\n";

                }
            }
            Console.WriteLine(inventory);
        }
        public int GetInventoryQuantity()
        {
            return Inventory.Count;
        }
        public void ShowGold()
        {
            Console.WriteLine($"Your gold balance: {this.Gold}\n");
        }   
        public bool UseItem()
        {
            Console.Write("Select an item to use/equip (type 0 to leave): ");
            int chosenIdx;
            while (!int.TryParse(Console.ReadLine(), out chosenIdx) || chosenIdx < 0 || chosenIdx > Inventory.Count)
            {
                Console.Clear();
                Console.WriteLine("Invalid choice, please try again.\n");
                Console.WriteLine("Select an item to use/equip (type 0 to leave): ");
                ShowInventory();
            }
            if (chosenIdx == 0)
            {
                return false;
            }
            Item selectedItem = Inventory[chosenIdx - 1];
            selectedItem.Use(this);
            return true;
        }
        public void RemoveItem(Item item)
        {
               Inventory.Remove(item);
        }
        public bool BuyItem(Item item)
        {
            if (this.Gold < item.Price)
            {
                Console.WriteLine("You don't have enough gold to buy this item.");
                return false;
            }
            this.Gold -= item.Price;
            AddItem(item);
            Console.WriteLine($"You have bought {item.Name} for {item.Price} gold.");
            return true;
        }
        public void SellItem(int idx)
        {
            Item item = Inventory.ElementAt(idx - 1);
            if(item.Type != ItemType.Potion && this.Inventory.Count(i => i.Type == item.Type) <= 1) { 
                Console.WriteLine("You can't sell your only weapon or armor.");
                return;
            }
            this.Gold += item.Price;
            RemoveItem(item);
            Console.Clear();
            Console.WriteLine($"You have sold {item.Name} for {item.Price} gold.");
        }
        public void BuyDrink(Item item)
        {
            Console.Clear();
            if (this.Gold < item.Price)
            {
                Console.WriteLine("You don't have enough gold to buy this drink.");
                return;
            }
            this.Gold -= item.Price;
            this.SetCurrHealth(-item.Value);
            Console.WriteLine($"You have drinked {item.Name} for {item.Price} gold and healed for {item.Value} health.");
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
        public void AddGold(int gold)
        {
            this.Gold += gold;
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
