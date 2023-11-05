using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Week8Lec1Game
{
    internal class GameBoard
    {
        private int boardSize;
        private Player[] players;
        private Player playableCharacter = null;
        private Player[,] board;
        private int playable = 0;
        Random random = new Random();
        string[] names = { "B", "D", "J", "R", "S" };

        public GameBoard(int playerCount, int BoardSize, int playable)
        {
            players = new Player[playerCount];
            this.boardSize = BoardSize;
            board = new Player[boardSize, boardSize];
            this.playable = playable;
            fillBoard(board);

            if (playable == 1)
            {
                int x = random.Next(boardSize);
                int y = random.Next(boardSize);
                playableCharacter = new Player("You",6, x, y);
                board[playableCharacter.x, playableCharacter.y] = playableCharacter;
            }

            placePlayers(random, names);
        }
        public void startGame()
        {
            int flag = 1;

            while (flag != 0)
            {
                Console.WriteLine();
                try
                {
                    if (flag != 0)
                    {
                        movePlayers(players, random);
                        printBoard(board);
                        if (playable == 1)
                        {
                            if (playableCharacter.isAlive)
                            {
                                if (playableCharacter.hp < playableCharacter.getMaxHp())
                                {
                                    Console.WriteLine("You Cannot Move as You Need to Heal");
                                    playableCharacter.hp++;
                                    Console.WriteLine("You healed 1 hp {0}/{1}", playableCharacter.hp - 1, playableCharacter.getMaxHp());
                                    Console.ReadLine();
                                }
                                else if (playableCharacter.hp == 0)
                                {
                                    Console.WriteLine("You Died");
                                    flag = 0;
                                }
                                else
                                {
                                    Console.WriteLine("What direction would you like to move");
                                    Console.WriteLine("1: UP");
                                    Console.WriteLine("2: DOWN");
                                    Console.WriteLine("3: LEFT");
                                    Console.WriteLine("4: RIGHT");
                                    Console.WriteLine("0: EXIT");

                                    flag = int.Parse(Console.ReadLine());
                                    switch (flag)
                                    {
                                        case 1:
                                            moveUp(playableCharacter);
                                            break;
                                        case 2:
                                            moveDown(playableCharacter);
                                            break;
                                        case 3:
                                            moveLeft(playableCharacter);
                                            break;
                                        case 4:
                                            moveRight(playableCharacter);
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
                        if(flag != 0)
                        {
                            flag = checkBoard(board);
                        }
                        
                    }
                }
                catch
                {
                    Console.WriteLine("Input must be an int");
                }

            }
        }

        public void placePlayers(Random random, string[] names)
        {
            int x = 0;
            int y = 0;
            for (int i = 0; i < players.Length; i++)
            {
                x = random.Next(boardSize);
                y = random.Next(boardSize);
                if (board[x, y].isReal())
                {
                    i--;
                    continue;
                }
                else
                {
                    players[i] = new Player(names[i],i+1, x, y);
                    board[x, y] = players[i];
                }


            }
        }
        public void movePlayers(Player[] players, Random random)
        {
            double generated;
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].id != 6 && players[i].isAlive)
                {
                    if (players[i].hp < players[i].getMaxHp())
                    {
                        players[i].hp += 1;
                        Console.WriteLine("Player {0} healed 1 hp", players[i].name);
                    }
                    else
                    {
                        generated = random.NextDouble();

                        if (generated > 0.75)
                        {
                            moveUp(players[i]);
                        }
                        else if (generated > 0.5)
                        {
                            moveLeft(players[i]);
                        }
                        else if (generated > 0.25)
                        {
                            moveDown(players[i]);
                        }
                        else
                        {
                            moveRight(players[i]);
                        }
                    }
                }
            }
        }

        public void moveDown(Player player)
        {
            if (player.x != board.GetLength(0) - 1)
            {
                if (board[(player.x + 1), player.y].isReal())
                {
                    player = battle(player, board[player.x + 1, player.y]);
                }
                
                board[player.x, player.y] = new Player();
                board[player.x + 1, player.y] = player;
                player.x += 1;
                Console.WriteLine("Player Moved Down");
            }
        }
        

        public void moveUp(Player player)
        {
            if (player.x != 0)
            {
                if (board[(player.x - 1), player.y].isReal())
                {
                    player = battle(player, board[player.x - 1, player.y]);
                }
                
                board[player.x, player.y] = new Player();
                board[player.x - 1, player.y] = player;
                player.x -= 1;

                Console.WriteLine("Player Moved Up");
            }

        }
        public void moveRight(Player player)
        {
            if (player.y != board.GetLength(1) - 1)
            {
                if (board[player.x, (player.y + 1)].isReal())
                {
                    player = battle(player, board[player.x, player.y + 1]);
                }
                
                board[player.x, player.y] = new Player();
                board[player.x, player.y + 1] = player;
                player.y += 1;
                Console.WriteLine("Player Moved Right");
            }
        }

        public void moveLeft(Player player)
        {
            if (player.y != 0)
            {
                if (board[player.x, (player.y - 1)].isReal())
                {
                    player = battle(player, board[player.x, player.y - 1]);
                }
                
                board[player.x, player.y] = new Player();
                board[player.x, player.y - 1] = player;
                player.y -= 1;
                Console.WriteLine("Player Moved Left");
            }
        }
        public void printBoard(Player[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write(board[i, j].id + " ");
                }
                Console.WriteLine();
            }
        }
        public void fillBoard(Player[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(0); j++)
                {
                    board[i, j] = new Player();
                }
            }
        }
        public Player battle(Player p1, Player p2)
        {
            Random random = new Random();
            Player winner = null;
            do
            {
                if (random.NextDouble() > 0.5)
                {
                    p2.hp -= 1 + p1.kills;
                    Console.WriteLine("{0} has attacked {1} for {2} damage", p1.name, p2.name, 1 + p1.kills);
                    winner = battleOutcome(p2, p1);
                }
                else if (random.NextDouble() > 0.5)
                {
                    p1.hp -= 1 + p2.kills;
                    Console.WriteLine("{0} has attacked {1} for {2} damage", p2.name, p1.name, 1 + p2.kills);
                    winner = battleOutcome(p1, p2);
                }
                else
                {
                    Console.WriteLine("Double Miss");
                }
            } while (winner == null);
            return winner;
        }
        public Player battleOutcome(Player p1, Player p2)
        {
            if (p1.hp <= 0)
            {
                p1.isAlive = false;
                Console.WriteLine(p2.name + " has defeated " + p1.name);
                p2.kills += 1 + p1.kills;
                return p2;
            }
            else
            {
                return null;
            }
        }
        public int checkBoard(Player[,] board)
        {
            int counter = 0;
            Player winner = new Player();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j].isReal())
                    {
                        counter++;
                        winner = board[i, j];
                    }
                }
            }

            if (counter == 1 && winner != null)
            {
                Console.WriteLine("{0} has won the tournament", winner.name);
                printBoard(board);
                Console.ReadLine();
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
