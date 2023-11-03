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
        private int playerCount;
        private int boardSize;
        Random random = new Random();
        string[] names = { "B", "D", "J", "R", "S" };

        public GameBoard(int playerCount, int BoardSize)
        {
            this.playerCount = playerCount;
            this.boardSize = BoardSize;
        }
        public void startGame(int playable)
        {
            
            Player[,] board = new Player[boardSize, boardSize];
            fillBoard(board);
            Player playableCharacter = new Player();
            if (playable == 1)
            {
                playableCharacter = new Player("You");
                playableCharacter.id = 2;
                board[random.Next(boardSize), random.Next(boardSize)] = playableCharacter;
            }
  
            placePlayers(board, random, playerCount, boardSize, names);
            int flag = 1;

            Console.WriteLine("Game Start!");

            while (flag != 0)
            {
                Console.WriteLine();
                try
                {
                    printBoard(board);
                    
                    if (flag != 0)
                    {
                        movePlayers(board, random);
                        resetHasMoved(board);
                        if (playable == 1)
                        {
                            if (playableCharacter.hp < playableCharacter.getMaxHp())
                            {
                                Console.WriteLine("You Cannot Move as You Need to Heal");
                                playableCharacter.hp++;
                                Console.WriteLine("You healed 1 hp {0}/{1}", playableCharacter.hp, playableCharacter.getMaxHp());
                                Console.ReadLine();
                            }
                            else if(playableCharacter.hp == 0)
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
                                        moveUp(board, playableCharacter);
                                        break;
                                    case 2:
                                        moveDown(board, playableCharacter);
                                        break;
                                    case 3:
                                        moveLeft(board, playableCharacter);
                                        break;
                                    case 4:
                                        moveRight(board, playableCharacter);
                                        break;
                                    default:
                                        break;
                                }
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

        public void moveDown(Player[,] board, Player player)
        {
            int flag = 0;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == player && i != board.GetLength(0) - 1)
                    {
                        if (board[i + 1, j].isReal())
                        {
                            player = battle(player, board[i + 1, j]);
                        }
                        board[i + 1, j] = player;
                        board[i, j] = new Player();
                        Console.WriteLine("Player Moved Down");
                        flag = 1;
                    }
                }
                if (flag == 1)
                {
                    break;
                }
            }
        }

        public void moveUp(Player[,] board, Player player)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == player && i != 0)
                    {
                        if (board[i - 1, j].isReal())
                        {
                            player = battle(player, board[i - 1, j]);
                        }
                        board[i - 1, j] = player;
                        board[i, j] = new Player();
                        Console.WriteLine("Player Moved Up");
                        break;
                    }
                }
            }
        }
        public void moveRight(Player[,] board, Player player)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == player && j != board.GetLength(1) - 1)
                    {
                        if (board[i, j + 1].isReal())
                        {
                            player = battle(player, board[i, j + 1]);
                        }
                        board[i, j + 1] = player;
                        board[i, j] = new Player();
                        Console.WriteLine("Player Moved Right");
                        break;
                    }
                }
            }
        }

        public void moveLeft(Player[,] board, Player player)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == player && j != 0)
                    {
                        if (board[i, j - 1].isReal())
                        {
                            player = battle(player, board[i, j - 1]);
                        }
                        board[i, j - 1] = player;

                        board[i, j] = new Player();
                        Console.WriteLine("Player Moved Left");
                        break;
                    }
                }
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

        public void placePlayers(Player[,] board, Random random, int players, int boardSize, string[] names)
        {
            int x = 0;
            int y = 0;
            for (int i = 0; i < players; i++)
            {
                x = random.Next(boardSize);
                y = random.Next(boardSize);
                if (board[x, y].isReal())
                {
                    i--;
                }
                else
                {
                    board[random.Next(boardSize), random.Next(boardSize)] = new Player(names[i]);
                }

                
            }
        }
        public void movePlayers(Player[,] board, Random random)
        {
            double generated;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(0); j++)
                {
                    if (board[i, j].isReal() && !board[i, j].hasMoved && board[i,j].id != 2)
                    {
                        board[i, j].hasMoved = true;
                        if (board[i, j].hp < board[i, j].getMaxHp())
                        {
                            board[i, j].hp += 1;
                            Console.WriteLine("Player {0} healed 1 hp", board[i, j].name);
                        }
                        else
                        {
                            generated = random.NextDouble();

                            if (generated > 0.75)
                            {
                                moveUp(board, board[i, j]);
                            }
                            else if (generated > 0.5)
                            {
                                moveLeft(board, board[i, j]);
                            }
                            else if (generated > 0.25)
                            {
                                moveDown(board, board[i, j]);
                            }
                            else
                            {
                                moveRight(board, board[i, j]);
                            }
                        }


                    }
                }
            }
        }

        public void resetHasMoved(Player[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(0); j++)
                {
                    board[i, j].hasMoved = false;
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
