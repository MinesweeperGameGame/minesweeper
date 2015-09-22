using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

struct Coordinates
{
    public int x;
    public int y;
    public int mapLengthX;
    public int mapLengthY;

    public void putCoordinates(int p1, int p2)
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
    public static Coordinates playerCords = new Coordinates();

    static void Main()
    {
        //Choose Level Console Size
        Console.BufferWidth = Console.WindowWidth = 70;
        //Choose Level
        ChooseLevel();

        //Hide Cursor Visible
        Console.CursorVisible = false;

        int score = 0;

        //List of Cordinates of Dollars
        List<KeyValuePair<int, int>> dollarsCords = new List<KeyValuePair<int, int>>();

        //Map Size[x, y] 
        playerCords.mapLengthX = playerCords.mapLengthY * 2;

        //Console Size
        Console.BufferHeight = Console.WindowHeight = playerCords.mapLengthY + 7;
        Console.BufferWidth = Console.WindowWidth = playerCords.mapLengthX + 7;

        //Player's Start Cordinates(In The Middle of MAP)
        playerCords.putCoordinates(playerCords.mapLengthX / 2, playerCords.mapLengthY / 2);

        //Count Of Dollars
        int CountOfDollars = playerCords.mapLengthY;

        //Print Startup text
        PrintStartupText();

        //Print Player
        PrintPlayer(playerCords.x, playerCords.y, score, dollarsCords);

        //Spawn Random Dollars On The Map
        dollarsCords = SpawnRandomDollars(CountOfDollars, playerCords.mapLengthX, playerCords.mapLengthY);

        //Print Dollars
        PrintDollars(dollarsCords);

        //Print Score and Frame
        PrintScoreAndFrame(score, playerCords.mapLengthY, playerCords.mapLengthX);

        Stopwatch collectTimer = new Stopwatch();
        collectTimer.Start();

        //main loop
        while (true)
        {
            //Player Moves
            playerMoves();

            //Update Score
            //-----

            score = PrintPlayer(playerCords.x, playerCords.y, score, dollarsCords);

            PrintScore(score, playerCords.mapLengthY);

            if (score == CountOfDollars)
            {
                collectTimer.Stop();
                break;
            }

            Thread.Sleep(25);
        }
        PrintYouFinished(playerCords.mapLengthY, collectTimer);
    }

    public static void ChooseLevel()
    {
        Console.Write("Write 1, 2, 3 or 4 to choose level: ");
        ConsoleKeyInfo level = Console.ReadKey(false);
        if (level.Key == ConsoleKey.D1)
        {
            playerCords.mapLengthY = 10;
        }
        else if (level.Key == ConsoleKey.D2)
        {
            playerCords.mapLengthY = 15;
        }
        else if (level.Key == ConsoleKey.D3)
        {
            playerCords.mapLengthY = 20;
        }
        else if (level.Key == ConsoleKey.D4)
        {
            playerCords.mapLengthY = 25;
        }
        else
        {
            playerCords.mapLengthY = 15;
        }
        Console.Clear();
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
            Console.SetCursorPosition(playerCords.x, playerCords.y);
            switch (pressedKey.Key)
            {
                case ConsoleKey.UpArrow:
                    Console.Write(" ");
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
                    Console.Write(" ");
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
                    Console.Write(" ");
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
                    Console.Write(" ");
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

    public static int PrintPlayer(int x, int y, int score, List<KeyValuePair<int, int>> list)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(x, y);
        Console.Write("O");
        if(list.Contains(new KeyValuePair<int, int>(x, y)))
        {
            list.Remove(new KeyValuePair<int, int>(x, y));
            score++;
        }
        return score;
    }

    public static void PrintDollars(List<KeyValuePair<int, int>> list)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        for (int i = 0; i < list.Count; i++)
        {
                Console.SetCursorPosition(list[i].Key, list[i].Value);
                Console.Write("$");
        }
    }

    public static void PrintScore(int score, int y)
    {
        Console.SetCursorPosition(19, y + 2);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(score);
    }

    public static void PrintScoreAndFrame(int score, int y, int x)
    {
        Console.ForegroundColor = ConsoleColor.White;
        y++;
        Console.SetCursorPosition(0, y);
        Console.WriteLine(new string('`', x + 2));
        Console.SetCursorPosition(0, y + 1);
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
        if(timer.Elapsed.Minutes == 0)
        {
            Console.WriteLine("You finished!\nTime: {0} seconds", timer.Elapsed.Seconds);
        }
        else
        {
            Console.WriteLine("You finished!\nTime: {0} min {1} sec", timer.Elapsed.Minutes, timer.Elapsed.Seconds);
        }
        while (true)
        {
            Console.SetCursorPosition(0, y + 5);
            Console.ReadLine();
        }
    }
}