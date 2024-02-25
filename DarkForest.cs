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
            this.Stage = 1;
            this.Directions = new Dictionary<string, string>() { { "North", "Castle" }, { "South", "Village" } };
            this.Items = new Dictionary<string, int>() { { "HealthPotion", 20 }, {"ChainArmor", 6}, {"SilverBow", 13}, {"CritPotion", 5} };
            this.RewardOdds = new Dictionary<string, int>() { { "HealthPotion", 60 }, { "ChainArmor", 25 }, { "SilverBow", 10 }, { "CritPotion", 5 } };
            this.Enemies = new Dictionary<int, Enemy>() 
            { 
                { 1, new Enemy("Goblin",35,9,2,5) }, 
                { 2, new Enemy("Orc",50,13,3,10) }, 
                { 3, new Boss("Dark Wizard",45,17,2,15,new Dictionary<string, Tuple<int, int>>{ { "FireBall", new Tuple<int, int>(26, 20) } }) } 
            };
        }
    } 
}
    