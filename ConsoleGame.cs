using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

struct Cordinates
{
    public int x;
    public int y;
    public int mapLengthX;
    public int mapLengthY;

    public void putCordinates(int p1, int p2)
    {
        x = p1;
        y = p2;
    }

    public void moveLeft()
    {
        x--;
    }

    public void moveRight()
    {
        x++;
    }

    public void moveUp()
    {
        y--;
    }

    public void moveDown()
    {
        y++;
    }
}

class ConsoleGame
{
    //Declare PlayerCords Obj
    public static Cordinates playerCords = new Cordinates();

    static void Main()
    {
        //Hide Cursor Visible
        Console.CursorVisible = false;

        int score = 0;

        //Map Size[x, y] (Minimum y = 10 !!!)
        playerCords.mapLengthY = 15;
        playerCords.mapLengthX = playerCords.mapLengthY * 2;

        //Console Size
        Console.BufferHeight = Console.WindowHeight = playerCords.mapLengthY + 7;
        Console.BufferWidth = Console.WindowWidth = playerCords.mapLengthX + 7;

        //Player's Start Cordinates(In The Middle of MAP)
        playerCords.putCordinates(playerCords.mapLengthX / 2, playerCords.mapLengthY / 2);

        //Count Of Dollars
        int CountOfDollars = playerCords.mapLengthY;

        //Print Startup text
        PrintStartupText();

        //Print Player
        PrintPlayer(playerCords.x, playerCords.y);

        //List of Cordinates of Dollars
        List<KeyValuePair<int, int>> dollarsCords = new List<KeyValuePair<int, int>>();

        //Spawn Random Dollars On The Map
        dollarsCords = SpawnRandomDollars(CountOfDollars, playerCords.mapLengthX, playerCords.mapLengthY);

        //Print Dollars
        score = PrintDollars(dollarsCords, playerCords.x, playerCords.y, score);

        //Print Score and Frame
        PrintScoreAndFrame(score, playerCords.mapLengthY, playerCords.mapLengthX);

        Stopwatch collectTimer = new Stopwatch();
        collectTimer.Start();

        //main loop
        while (true)
        {
            //Player Moves
            playerMoves();

            //Clear Console Before Print Items
            Console.Clear();

            //Print Dollars and Update Score
            score = PrintDollars(dollarsCords, playerCords.x, playerCords.y, score);

            PrintPlayer(playerCords.x, playerCords.y);

            PrintScoreAndFrame(score, playerCords.mapLengthY, playerCords.mapLengthX);

            if (score == CountOfDollars)
            {
                collectTimer.Stop();
                break;
            }

            Thread.Sleep(100);
        }
        PrintYouFinished(playerCords.mapLengthY, collectTimer);
    }

    public static void PrintStartupText()
    {
        Console.Write("Collect ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("DOLLARS($)");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("/for the shortest time/\n");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Controls:");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(" -LEFT ARROW");
        Console.WriteLine(" -RIGHT ARROW");
        Console.WriteLine(" -UP ARROW");
        Console.WriteLine(" -DOWN ARROW");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("PRESS [");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("ENTER");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("] TO START");
        Console.ReadLine();
        Console.Clear();
    }

    public static void playerMoves()
    {
        bool isPlayerMove = false;

        //While Player doesn't move TRUE ---> else FALSE
        while (!isPlayerMove)
        {
            ConsoleKeyInfo pressedKey = Console.ReadKey(true);
            switch(pressedKey.Key)
            {
                case ConsoleKey.UpArrow:
                    isPlayerMove = true;
                    if (playerCords.y > 0)
                    {
                        playerCords.moveUp();
                    }
                    else
                    {
                        playerCords.y = playerCords.mapLengthY;
                    }
                    break;

                case ConsoleKey.DownArrow:
                    isPlayerMove = true;
                    if (playerCords.y < playerCords.mapLengthY)
                    {
                        playerCords.moveDown();
                    }
                    else
                    {
                        playerCords.y = 0;
                    }
                    break;

                case ConsoleKey.LeftArrow:
                    isPlayerMove = true;
                    if (playerCords.x > 0)
                    {
                        playerCords.moveLeft();
                    }
                    else
                    {
                        playerCords.x = playerCords.mapLengthX;
                    }
                    break;

                case ConsoleKey.RightArrow:
                    isPlayerMove = true;
                    if (playerCords.x < playerCords.mapLengthX)
                    {
                        playerCords.moveRight();
                    }
                    else
                    {
                        playerCords.x = 0;
                    }
                    break;
            }
        }
    }

    public static void PrintPlayer(int x, int y)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(x, y);
        Console.Write("O");
    }

    public static int PrintDollars(List<KeyValuePair<int, int>> list, int x, int y, int score)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Key == x && list[i].Value == y)
            {
                score++;
                list.Remove(list[i]);
                i--;
            }
            else
            {
                Console.SetCursorPosition(list[i].Key, list[i].Value);
                Console.Write("$");
            }
        }
        return score;
    }

    public static void PrintScoreAndFrame(int score, int y, int x)
    {
        Console.ForegroundColor = ConsoleColor.White;
        y++;
        Console.SetCursorPosition(0, y);
        Console.WriteLine(new string('`', x+2));
        Console.SetCursorPosition(0, y+1);
        Console.Write("Collected ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("DOLLARS");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(": {0}", score);
        for (int i = 0; i < y; i++)
        {
            Console.SetCursorPosition(x + 1, i);
            Console.Write("|");
        }
    }

    public static List<KeyValuePair<int, int>> SpawnRandomDollars(int n, int maxX, int maxY)
    {
        Random random = new Random();
        int randX;
        int randY;
        int count = 0;
        List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();

        while (count < n)
        {
            randX = random.Next(0, maxX + 1);
            randY = random.Next(0, maxY + 1);
            if (!list.Contains(new KeyValuePair<int, int>(randX, randY)))
            {
                list.Add(new KeyValuePair<int, int>(randX, randY));
                count++;
            }
        }

        return list;
    }
    
    public static void PrintYouFinished(int y, Stopwatch timer)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.SetCursorPosition(0, y + 3);
        Console.WriteLine("You finished!\nTime: {0}seconds", timer.Elapsed.Seconds);
        while (true)
        {
            Console.SetCursorPosition(0, y + 5);
            Console.ReadLine();
        }
    }
}

