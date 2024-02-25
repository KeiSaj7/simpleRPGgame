using System.Xml.Linq;

namespace SimpleConsoleAppGame
{

    public enum Race
    {
        Human,
        Elf
    };

    class Program
    {
        static void Main(string[] args)
        {
            string welcomeMsg = "Welcome to game! Choose your username: ";
            Console.WriteLine(welcomeMsg);
            string? username = Console.ReadLine();
            while(string.IsNullOrWhiteSpace(username)) 
            {
                Console.Clear();
                Console.WriteLine("Username cannot be empty, please try again: ");
                username = Console.ReadLine();
                
            }
            Console.Clear();
            string raceMsg = $"It's nice too meet you {username}! Choose your character: ";
            Console.WriteLine(raceMsg);

            var races = Enum.GetValues(typeof(Race));
            for(int i = 0; i < races.Length; i++)
            {
                Console.WriteLine($"{i+1}.{races.GetValue(i)}");
            }
            int chosenIdx;
            while(!int.TryParse(Console.ReadLine(), out chosenIdx) || chosenIdx < 1 || chosenIdx > races.Length)
            {
                   Console.WriteLine("Invalid choice, please try again.");
            }
            Race chosenRace = (Race)races.GetValue(chosenIdx-1);
            var character = Character.CreateCharacter(username, chosenRace);
            Console.Clear();
            Console.WriteLine($"You've chosen {chosenRace} race. Your basic statistics:" );
            character.PrintStats();
           // character.ShowInventory();
            Console.WriteLine("Press any key to start the adventure...");
            Console.ReadKey();
            Console.Clear();
            DarkForest destination1 = new DarkForest();
            Console.WriteLine($"Your first deistination is {destination1.Name}. Kill all enemies to complete this level. \n\nPress any key to enter the forest...");
            Console.ReadKey();
            destination1.StartLevel(character);
        }
    }
}

