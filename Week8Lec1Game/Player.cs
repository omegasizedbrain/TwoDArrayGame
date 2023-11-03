using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week8Lec1Game
{
    internal class Player
    {
        private const int MAXHP = 5;
        public int hp { get; set; }
        public int kills { get; set;}

        public string name {  get; set;}

        public int id { get; set; }

        public bool hasMoved { get; set; }

        public Player()
        {
            kills = 0;
            name = "";
            id = 0;
        }
        public Player(string name)
        {
            kills = 0;
            this.name = name;
            id = 1;
            hasMoved = false;
            hp = 5;

        }

        public bool isReal()
        {
            return id != 0;
        }
        public int getMaxHp()
        {
            return MAXHP;
        }

    }
}
