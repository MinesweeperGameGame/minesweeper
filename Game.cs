using System;
using System.Windows.Forms;
using Minesweeper___game.Datebase;
using Minesweeper___game.Models;

namespace Minesweeper___game
{
    class Game
    {
        public static Cells cells = new Cells();

        public Game()
        {
        }
        public void Start()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GameWindow());
        }
    }
}
