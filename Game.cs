﻿using System;
using System.Windows.Forms;
using Minesweeper___game.Datebase;
using Minesweeper___game.Models;

namespace Minesweeper___game
{
    class Game
    {
        public static int level = -1;
        public static Levels levels = new Levels();
        public static bool isAlive;

        public Game()
        {
            Game.levels.addDifficulty(9, 9, 10, 4);
            Game.levels.addDifficulty(16, 16, 40, 8);
            Game.levels.addDifficulty(16, 32, 99, 15);
        }
        public void Start()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GameWindow());
        }
    }
}
