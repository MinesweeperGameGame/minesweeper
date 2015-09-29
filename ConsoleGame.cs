using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

struct PlayerCoordinates
{
    public int x;
    public int y;
    public int mapLengthX;
    public int mapLengthY;
    public int direction;

    public void PutCoordinates(int put, int put2)
    {
        x = put;
        y = put2;
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

struct Shoot
{
    public int x;
    public int y;
    public int direction;
    public bool isShoot;
}

class ConsoleGame
{
    //Declare PlayerCords Obj
    public static PlayerCoordinates playerCords = new PlayerCoordinates();
    //Declare PlayerShoot
    public static Shoot shoot = new Shoot();
    //List of Cordinates of Dollars
    public static List<KeyValuePair<int, int>> dollarsCords = new List<KeyValuePair<int, int>>();
    //score
    public static int score = 0;

    static void Main()
    {
        //diasble shoot
        shoot.isShoot = false;
        //default player direction
        playerCords.direction = 1;

        //Hide Cursor Visible
        Console.CursorVisible = false;

        //Choose Level Window(Console Size)
        Console.BufferWidth = Console.WindowWidth = 70;
        //Choose Level
        ChooseLevel();

        //Map Size[x, y] 
        playerCords.mapLengthX = playerCords.mapLengthY * 2;

        //Console Size
        Console.BufferHeight = Console.WindowHeight = playerCords.mapLengthY + 7;
        Console.BufferWidth = Console.WindowWidth = playerCords.mapLengthX + 7;

        //Player's Start Cordinates(In The Middle of MAP)
        playerCords.PutCoordinates(playerCords.mapLengthX / 2, playerCords.mapLengthY / 2);

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

        //Creating timer
        Stopwatch collectTimer = new Stopwatch();
        collectTimer.Start();

        //main loop
        while (true)
        {
            //Player Moves
            PlayerMoves();

            //Move Shoot
            MoveShoot();

            //Update Score and Print Player
            score = PrintPlayer(playerCords.x, playerCords.y, score, dollarsCords);
            
            //Print Score
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
        Console.Write("----------------------------------\nPress 1, 2, 3 or 4 to choose level\n----------------------------------");
        ConsoleKeyInfo level = Console.ReadKey(false);
        if (level.Key == ConsoleKey.D1 || level.Key == ConsoleKey.NumPad1)
        {
            playerCords.mapLengthY = 10;
        }
        else if (level.Key == ConsoleKey.D2 || level.Key == ConsoleKey.NumPad2)
        {
            playerCords.mapLengthY = 15;
        }
        else if (level.Key == ConsoleKey.D3 || level.Key == ConsoleKey.NumPad3)
        {
            playerCords.mapLengthY = 20;
        }
        else if (level.Key == ConsoleKey.D4 || level.Key == ConsoleKey.NumPad4)
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
        Console.WriteLine("Moves");
        Console.WriteLine(" -LEFT ARROW");
        Console.WriteLine(" -RIGHT ARROW");
        Console.WriteLine(" -UP ARROW");
        Console.WriteLine(" -DOWN ARROW");
        Console.WriteLine("Shoot");
        Console.WriteLine(" -SPACE");
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
    public static void MoveShoot()
    {
        if(shoot.isShoot)
        {
            if (dollarsCords.Contains(new KeyValuePair<int, int>(shoot.x, shoot.y)))
            {
                dollarsCords.Remove(new KeyValuePair<int, int>(shoot.x, shoot.y));
                score++;
            }
            Console.SetCursorPosition(shoot.x, shoot.y);
            Console.Write(" ");
            switch (shoot.direction)
            {
                case 1:
                    if (shoot.y > 0)
                    {
                        shoot.y = --shoot.y;
                        Console.SetCursorPosition(shoot.x, shoot.y);
                        Console.Write("|");
                    }
                    else
                    {
                        shoot.isShoot = false;
                        Console.SetCursorPosition(shoot.x, shoot.y);
                        Console.Write(" ");
                    }
                    break;
                case 2:
                    if (shoot.x < playerCords.mapLengthX)
                    {
                        shoot.x = ++shoot.x;
                        Console.SetCursorPosition(shoot.x, shoot.y);
                        Console.Write("-");
                    }
                    else
                    {
                        shoot.isShoot = false;
                        Console.SetCursorPosition(shoot.x, shoot.y);
                        Console.Write(" ");
                    }
                    break;
                case 3:
                    if (shoot.y < playerCords.mapLengthY)
                    {
                        shoot.y = ++shoot.y;
                        Console.SetCursorPosition(shoot.x, shoot.y);
                        Console.Write("|");
                    }
                    else
                    {
                        shoot.isShoot = false;
                        Console.SetCursorPosition(shoot.x, shoot.y);
                        Console.Write(" ");
                    }
                        break;
                case 4:
                    if(shoot.x > 0)
                    {
                        shoot.x = --shoot.x;
                        Console.SetCursorPosition(shoot.x, shoot.y);
                        Console.Write("-");
                    }
                    else
                    {
                        shoot.isShoot = false;
                        Console.SetCursorPosition(shoot.x, shoot.y);
                        Console.Write(" ");
                    }
                    break;
            }
        }
    }
    public static void PlayerMoves()
    {

        //While Player doesn't move TRUE ---> else FALSE
        while (Console.KeyAvailable)
        {
            ConsoleKeyInfo pressedKey = Console.ReadKey(true);
            Console.SetCursorPosition(playerCords.x, playerCords.y);
            switch (pressedKey.Key)
            {
                case ConsoleKey.Spacebar:
                    if(!shoot.isShoot)
                    {
                        shoot.x = playerCords.x;
                        shoot.y = playerCords.y;
                        shoot.direction = playerCords.direction;
                        shoot.isShoot = true;
                        MoveShoot();
                    }
                    break;
                case ConsoleKey.UpArrow:
                    Console.Write(" ");
                    playerCords.direction = 1;
                    if (playerCords.y > 0)
                    {
                        playerCords.moveUp();
                    }
                    else
                    {
                        playerCords.y = playerCords.mapLengthY;
                    }
                    break;

                case ConsoleKey.RightArrow:
                    Console.Write(" ");
                    playerCords.direction = 2;
                    if (playerCords.x < playerCords.mapLengthX)
                    {
                        playerCords.moveRight();
                    }
                    else
                    {
                        playerCords.x = 0;
                    }
                    break;

                case ConsoleKey.DownArrow:
                    Console.Write(" ");
                    playerCords.direction = 3;
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
                    playerCords.direction = 4;
                    if (playerCords.x > 0)
                    {
                        playerCords.moveLeft();
                    }
                    else
                    {
                        playerCords.x = playerCords.mapLengthX;
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