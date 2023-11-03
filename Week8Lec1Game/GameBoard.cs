using System;
using System.Collections.Generic;
using System.Linq;
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
        public void startGame()
        {
            
            Player[,] board = new Player[boardSize, boardSize];
            fillBoard(board);
            //Player player = new Player("Starting Player");
            //board[5, 5] = player;
            placePlayers(board, random, playerCount, boardSize, names);
            int flag = 1;

            Console.WriteLine("Game Start!");

            while (flag != 0)
            {
                Console.WriteLine();
                printBoard(board);
                /*
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
                        moveUp(board, player);
                        break;
                    case 2:
                        moveDown(board, player);
                        break;
                    case 3:
                        moveLeft(board, player);
                        break;
                    case 4:
                        moveRight(board, player);
                        break;
                    default: 
                        break;
                }
                */
                Console.WriteLine("Move Players? (0 to Exit)");
                try
                {
                    flag = int.Parse(Console.ReadLine());
                    if (flag != 0)
                    {
                        movePlayers(board, random);
                        resetHasMoved(board);
                        flag = checkBoard(board);
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("Input must be an int");
                }

            }
            Console.ReadLine();
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
            for (int i = 0; i < players; i++)
            {
                board[random.Next(boardSize), random.Next(boardSize)] = new Player(names[i]);
            }
        }
        public void movePlayers(Player[,] board, Random random)
        {
            double generated;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(0); j++)
                {
                    if (board[i, j].isReal() && !board[i, j].hasMoved)
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
            Player winner;
            do
            {
                if (random.NextDouble() > 0.5)
                {
                    p2.hp -= 1 + p1.kills;
                    Console.WriteLine("{0} has attacked {1} for {2} damage", p1.name, p2.name, 1 + p1.kills);
                    winner = battleOutcome(p2, p1);
                }
                else
                {
                    p1.hp -= 1 + p2.kills;
                    Console.WriteLine("{0} has attacked {1} for {2} damage", p2.name, p1.name, 1 + p2.kills);
                    winner = battleOutcome(p1, p2);

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
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
