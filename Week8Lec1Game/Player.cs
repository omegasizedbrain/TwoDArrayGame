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

        public bool isAlive { get; set; }

        public string name {  get; set;}

        public int id { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public Player()
        {
            kills = 0;
            name = "";
            id = 0;
        }
        public Player(string name, int id, int x, int y)
        {
            kills = 0;
            this.name = name;
            this.id = id;
            hp = 5;
            this.x = x;
            this.y = y;
            isAlive = true;

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
