using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleConsoleAppGame
{
    public class Tavern: Destination
    {
        protected List<Item> Drinks { get; set; }
        protected Destination Direction { get; set; }
        public Tavern(Destination destination, List<Item> items)
        {
            this.Name = "Tavern";
            this.Direction = destination;
            this.Items = items;
            this.Drinks = new List<Item>() { new Item("Beer", 10, 1, ItemType.Potion, null), new Item("Wine", 20, 2, ItemType.Potion, null), new Item("Whiskey", 35, 3, ItemType.Potion, null) };

        }
        public override void StartLevel(Character character)
        {
            Choices(character);
        }
        public override void Choices(Character character)
        {
            Console.Clear();
            Console.WriteLine("Welcome to Tavern traveller, what are you looking for?");
            Console.WriteLine("1. Buy");
            Console.WriteLine("2. Sell");
            Console.WriteLine("3. Drink");
            Console.WriteLine("4. Leave");
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 4)
            {
                Console.WriteLine("Invalid choice, please try again.");
            }
            Console.Clear();
            switch (choice)
            {
                case 1:
                    Buy(character);
                    Choices(character);
                    break;
                case 2:
                    Sell(character);
                    Choices(character);
                    break;
                case 3:
                    Drink(character);
                    Choices(character);
                    break;
                case 4:
                    Leave(character);
                    Choices(character);
                    break;
            }
        }
        public void Buy(Character character)
        {
            int choice = -1;
            while (choice != 0)
            {
                character.ShowGold();
                Console.WriteLine("What do you want to buy?(type 0 to leave)");
                for (int i = 0; i < Items.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {Items[i].Name} | Price: {Items[i].Price} gold");
                }
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > Items.Count)
                {
                    Console.WriteLine("Invalid choice, please try again.");
                }
                if (choice == 0)
                {
                    return;
                }
                Console.Clear();
                bool purchase = character.BuyItem(Items[choice - 1]);
                if(purchase)
                {
                    this.Items.RemoveAt(choice - 1);
                }   
            }
        }
        public void Sell(Character character)
        {
            int choice = -1;
            while(choice != 0)
            {
                character.ShowGold();
                Console.WriteLine("Which item do you want to sell?(type 0 to leave)");
                character.ShowInventory();
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > character.GetInventoryQuantity())
                {
                    Console.WriteLine("Invalid choice, please try again.");
                }
                if (choice == 0)
                {
                    return;
                }
                character.SellItem(choice);
            }
        }
        public void Drink(Character character)
        {
            int choice = -1;
            while(choice != 0)
            {
                character.ShowGold();
                Console.WriteLine("What do you want to drink?(type 0 to leave)");
                for (int i = 0; i < Drinks.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {Drinks[i].Name} | Price: {Drinks[i].Price} gold");
                }
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > Drinks.Count)
                {
                    Console.WriteLine("Invalid choice, please try again.");
                }
                if (choice == 0)
                {
                    return;
                }
                character.BuyDrink(Drinks[choice - 1]);
            }
        }
        public void Leave(Character character)
        {
            Console.WriteLine($"You have left Tavern and now you are heading to {this.Direction.Name}");
            this.Direction.StartLevel(character);
        }
    }
}
