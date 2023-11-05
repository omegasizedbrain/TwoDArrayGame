using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using Week8Lec1Game;

namespace Week8Lec2TwoDArrayGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            try
            {
                Console.WriteLine("Main Menu\n1:Controlled Character\n2:Just Bots");
                int playable = int.Parse(Console.ReadLine());
                GameBoard board = new GameBoard(5, 10, playable);
                Console.WriteLine("Game Start!");
                board.startGame();
            }
            catch
            {
                Console.WriteLine("Must be an int");
            }

            
            
        }
    }
}
