using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper___game.Datebase
{
    class Levels
    {
        public List<Difficulty> levelsList;

        public Levels()
        {
            this.levelsList = new List<Difficulty>();
        }
        public void addDifficulty (int height, int width, int mines, int densityCheck)
        {
            this.levelsList.Add(new Difficulty(height, width, mines, densityCheck));

        }

    }
}
