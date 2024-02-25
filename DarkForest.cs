using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleConsoleAppGame
{
    public class DarkForest: Destination
    {
        public DarkForest()
        {
            this.Name = "Dark Forest";
            this.Directions = new Dictionary<string, string>() { { "North", "Castle" }, { "South", "Village" } };
            this.Items = new Dictionary<string, string>() { { "Potion", "Heals 20 health" }, { "Sword", "Attack +5" } };
            this.Enemies = new Dictionary<int, Enemy>() { { 1, new Enemy("Goblin",35,9,2,5) }, { 2, new Enemy("Orc",50,13,3,10) } };
        }

    } 
}
