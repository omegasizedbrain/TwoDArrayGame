using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
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
                int input = 0;
                int playable = 0;
                int shrinkBoard = 0;
                Console.WriteLine("Main Menu\n1:Start Game\n2:Add Playable Character\n3:Add Shrinking Board\n4:Exit");
                do
                {
                    input = int.Parse(Console.ReadLine());
                    if (input == 1)
                    {
                        break;
                    }
                    else if (input == 2)
                    {
                        playable = 1;
                        Console.WriteLine("Playable Character Enabled");
                    }
                    else if(input == 3)
                    {
                        shrinkBoard = 1;
                        Console.WriteLine("Shrinking Board Enabled");
                    }else if(input == 4)
                    {
                        System.Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("Thats not an option");
                    }
                    
                } while (true) ;
                
                
                GameBoard game = new GameBoard(5, 10, playable, shrinkBoard);
                Console.WriteLine("Game Start!");
                int flag = 1;
                BattleReport report = new BattleReport();

                while (flag != 0)
                {
                    Console.WriteLine();
                    try
                    {
                        if (flag != 0)
                        {  
                            report.battleText += game.movePlayers(game.players).battleText;
                            Console.WriteLine(report.battleText);
                            report = new BattleReport();
                            game.printBoard(game.board);
                            if (playable == 1)
                            {
                                if (game.playableCharacter.isAlive)
                                {
                                    if (game.playableCharacter.hp < game.playableCharacter.getMaxHp())
                                    {
                                        report.battleText += "You Cannot Move as You Need to Heal\n";
                                        game.playableCharacter.hp++;
                                        report.battleText += "You healed 1 hp " + (game.playableCharacter.hp - 1) + "/" + game.playableCharacter.getMaxHp() + "\n";
                                        Console.ReadLine();
                                    }
                                    else if (game.playableCharacter.hp == 0)
                                    {
                                        Console.WriteLine("You Died");
                                        flag = 0;
                                    }
                                    else
                                    {
                                        printMoveOptions();

                                        flag = int.Parse(Console.ReadLine());
                                        switch (flag)
                                        {
                                            case 1:
                                                game.moveUp(game.playableCharacter, report);
                                                break;
                                            case 2:
                                                game.moveDown(game.playableCharacter, report);
                                                break;
                                            case 3:
                                                game.moveLeft(game.playableCharacter, report);
                                                break;
                                            case 4:
                                                game.moveRight(game.playableCharacter, report);
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("You lost");
                                    Console.ReadLine();
                                    break;
                                }


                            }
                            else
                            {
                                Console.WriteLine("Move Players? (0 to Exit)");
                                flag = int.Parse(Console.ReadLine());

                            }
                            if (flag != 0)
                            {
                                Player winner = game.checkBoard(game.board);
                                if (winner != null)
                                {
                                    Console.WriteLine("{0} has won the tournament", winner.name);
                                }
                            }                          
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Input must be an int");
                    }

                }
            }
            catch
            {
                Console.WriteLine("Must be an int");
            }
            
        }

        public static void printMoveOptions()
        {
            Console.WriteLine("What direction would you like to move");
            Console.WriteLine("1: UP");
            Console.WriteLine("2: DOWN");
            Console.WriteLine("3: LEFT");
            Console.WriteLine("4: RIGHT");
            Console.WriteLine("0: EXIT");
        }
    }
}
