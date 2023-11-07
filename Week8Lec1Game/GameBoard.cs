using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Week8Lec1Game
{
    internal class GameBoard
    {
        public int boardSize { get; set; }
        public Player[] players { get; }
        public Player playableCharacter { get; set; }
        public Player[,] board { get; set; }
        private int playable = 0;
        private int shrinkBoard = 0;
        Random random = new Random();
        string[] names = { "B", "D", "J", "R", "S" };

        public GameBoard(int playerCount, int BoardSize, int playable, int shrinkBoard)
        {
            players = new Player[playerCount];
            this.boardSize = BoardSize;
            board = new Player[boardSize, boardSize];
            this.playable = playable;
            fillBoard(board);
            this.shrinkBoard = shrinkBoard;

            if (playable == 1)
            {
                int x = random.Next(boardSize);
                int y = random.Next(boardSize);
                playableCharacter = new Player("You",6, x, y);
            }
            else
            {
                playableCharacter = null;
            }

            setPlayers(names);
            placePlayers();
        }
        public void setPlayers(string[] names)
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
                }


            }
        }
        public void placePlayers()
        {
            foreach(Player player in players)
            {
                if (player.isAlive)
                {
                    board[player.x, player.y] = player;
                }
                if (playable == 1)
                {
                    board[playableCharacter.x, playableCharacter.y] = playableCharacter;
                }
            }
        }
        public BattleReport movePlayers(Player[] players)
        {
            double generated;
            BattleReport report = new BattleReport();
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].id != 6 && players[i].isAlive)
                {
                    if (players[i].hp < players[i].getMaxHp())
                    {
                        players[i].hp += 1;
                        report.battleText += "Player " + players[i].name + " healed 1 hp\n";
                    }
                    else
                    {
                        generated = random.NextDouble();

                        if (generated > 0.75)
                        {
                            moveUp(players[i], report);
                        }
                        else if (generated > 0.5)
                        {
                            moveLeft(players[i], report);
                        }
                        else if (generated > 0.25)
                        {
                            moveDown(players[i], report);
                        }
                        else
                        {
                            moveRight(players[i], report);
                        }
                    }
                }
            }
            return report;
        }

        public void moveDown(Player player, BattleReport report)
        {
            if (player.x != board.GetLength(0) - 1)
            {
                if (board[(player.x + 1), player.y].isReal())
                {
                    player = battle(player, board[player.x + 1, player.y], report);
                }

                board[player.x, player.y] = new Player();
                board[player.x + 1, player.y] = player;
                player.x += 1;
                report.battleText += "Player Moved Down\n";
            }
                
        }
        

        public void moveUp(Player player, BattleReport report)
        {
            if (player.x != 0)
            {
                if (board[(player.x - 1), player.y].isReal())
                {
                    player = battle(player, board[player.x - 1, player.y], report);
                }

                board[player.x, player.y] = new Player();
                board[player.x - 1, player.y] = player;
                player.x -= 1;

                report.battleText += "Player Moved Up\n";
            }

        }
        public void moveRight(Player player, BattleReport report)
        {
            if (player.y != board.GetLength(1) - 1)
            {
                if (board[player.x, (player.y + 1)].isReal())
                {
                    player = battle(player, board[player.x, player.y + 1], report);
                }

                board[player.x, player.y] = new Player();
                board[player.x, player.y + 1] = player;
                player.y += 1;
                report.battleText += "Player Moved Right\n";
            }

        }

        public void moveLeft(Player player, BattleReport report)
        {
            if (player.y != 0)
            {
                if (board[player.x, (player.y - 1)].isReal())
                {
                    player = battle(player, board[player.x, player.y - 1], report);
                }
                board[player.x, player.y] = new Player();
                board[player.x, player.y - 1] = player;
                player.y -= 1;
                report.battleText += "Player Moved Left\n";
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
        public Player battle(Player p1, Player p2, BattleReport report)
        {
            Random random = new Random();
            Player winner = null;
            do
            {
                if (random.NextDouble() > 0.5)
                {
                    p2.hp -= 1 + p1.kills;
                    report.battleText += p1.name + " has attacked " + p2.name + " for " + (1 + p1.kills) + " damage\n";
                    winner = battleOutcome(p2, p1, report);
                }
                else if (random.NextDouble() > 0.5)
                {
                    p1.hp -= 1 + p2.kills;
                    report.battleText += p2.name + " has attacked " + p1.name + " for " + (1 + p2.kills) + " damage\n";
                    winner = battleOutcome(p1, p2, report);
                }
                else
                {
                    report.battleText += "Double Miss\n";
                }
            } while (winner == null);
            return winner;
        }
        public Player battleOutcome(Player p1, Player p2, BattleReport report)
        {
            if (p1.hp <= 0)
            {
                p1.isAlive = false;
                report.battleText += p2.name + " has defeated " + p1.name + "\n";
                p2.kills += 1 + p1.kills;
                if(shrinkBoard == 1 && boardSize != 2)
                {
                    shrink(report);
                }
                return p2;
            }
            else
            {
                return null;
            }
        }
        public Player checkBoard(Player[,] board)
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
                printBoard(board);
                Console.ReadLine();
                return winner;
            }
            else
            {
                return null;
            }
        }
        public void shrink(BattleReport report)
        {
            for(int i = 0; i < players.Length; i++)
            {
                if (players[i].x == boardSize - 1)
                {
                    if (board[(players[i].x - 1), players[i].y].isReal())
                    {
                        players[i] = battle(players[i], board[players[i].x - 1, players[i].y], report);
                    }

                    board[players[i].x, players[i].y] = new Player();
                    board[players[i].x - 1, players[i].y] = players[i];
                    players[i].x -= 1;
                }
                if (players[i].y == boardSize- 1)
                {
                    if (board[players[i].x, (players[i].y - 1)].isReal())
                    {
                        players[i] = battle(players[i], board[players[i].x, players[i].y - 1], report);
                    }
                    board[players[i].x, players[i].y] = new Player();
                    board[players[i].x, players[i].y - 1] = players[i];
                    players[i].y -= 1;
                }
            }
            if(playable == 1)
            {
                if (playableCharacter.x == boardSize - 1)
                {
                    if (board[(playableCharacter.x - 1), playableCharacter.y].isReal())
                    {
                        playableCharacter = battle(playableCharacter, board[playableCharacter.x - 1, playableCharacter.y], report);
                    }

                    board[playableCharacter.x, playableCharacter.y] = new Player();
                    board[playableCharacter.x - 1, playableCharacter.y] = playableCharacter;
                    playableCharacter.x -= 1;
                }
                if (playableCharacter.y == boardSize - 1)
                {
                    if (board[playableCharacter.x, (playableCharacter.y - 1)].isReal())
                    {
                        playableCharacter = battle(playableCharacter, board[playableCharacter.x, playableCharacter.y - 1], report);
                    }
                    board[playableCharacter.x, playableCharacter.y] = new Player();
                    board[playableCharacter.x, playableCharacter.y - 1] = playableCharacter;
                    playableCharacter.y -= 1;
                }
            }
            board = new Player[boardSize - 1, boardSize - 1];
            fillBoard(board);
            placePlayers();
            boardSize--;

            report.battleText += "The board size has shrunk\n";
        }
    }
}

