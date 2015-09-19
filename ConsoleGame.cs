using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class ConsoleGame
{
    static void Main()
    {
        Console.BufferHeight = Console.WindowHeight = 12;
        Console.BufferWidth = Console.WindowWidth = 14;

        int x = 0;
        int y = 0;
        int score = 0;
        int[,] arr =
        {
            { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 1, 0, 1, 0 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 1, 0, 1, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 },
            { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 },
            { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 1, 0, 1, 0 }
        };
        while (true)
        {
            while (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey();

                if (pressedKey.Key == ConsoleKey.UpArrow)
                {
                    if (y > 0)
                    {
                        y--;
                    }
                }
                else if (pressedKey.Key == ConsoleKey.DownArrow)
                {
                    if (y < 9)
                    {
                        y++;
                    }
                }
                else if (pressedKey.Key == ConsoleKey.LeftArrow)
                {
                    if (x > 0)
                    {
                        x--;
                    }
                }
                else if (pressedKey.Key == ConsoleKey.RightArrow)
                {
                    if (x < 9)
                    {
                        x++;
                    }
                }
            }
            if (arr[y, x] == 1)
            {
                arr[y, x] = 0;
                score++;
            }
            Console.Clear();
            Console.SetCursorPosition(x, y);
            Console.Write("O");
            Console.SetCursorPosition(0, 10);
            Console.Write("___________");
            Console.SetCursorPosition(11, 10);
            Console.Write("|");
            Console.SetCursorPosition(0, 11);
            Console.Write("Score: {0}", score);
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                Console.SetCursorPosition(11, i);
                Console.Write("|");
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j] == 1)
                    {
                        Console.SetCursorPosition(j, i);
                        Console.Write("$");

                    }
                }
            }
            Thread.Sleep(120);
        }
    }
}

