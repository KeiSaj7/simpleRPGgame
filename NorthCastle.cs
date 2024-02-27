using SimpleConsoleAppGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleConsoleAppGame
{
    public class NorthCastle: Destination
    {
        public NorthCastle()
        {
            this.Name = "North Castle";
            this.Stage = 1;
            this.Directions = new List<Destination>() {};
            this.Items = new List<Item>()
            {
                new Item("CritBlessing", 10, 20, ItemType.CritBuff, 1)
            };
            this.Enemies = new Dictionary<int, Enemy>()
            {

            };
        }

    }
}
