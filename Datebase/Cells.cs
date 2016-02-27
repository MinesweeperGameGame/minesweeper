using System.Collections.Generic;
using Minesweeper___game.Models;

namespace Minesweeper___game.Datebase
{
    class Cells
    {
        public List<Cell> cells;

        public Cells()
        {
            cells = new List<Cell>();
        }

        public void AddNewCell(int x, int y, int type)
        {
            this.cells.Add(new Cell(x, x + 20, y, y + 20, type));
        }

        public bool CheckIsInCell(int x, int y)
        {
            foreach (Cell cell in cells)
            {
                if (cell.minX <= x && cell.maxX >= x && cell.minY <= y && cell.maxY >= y)
                {
                    return true;
                }
            }
            return false;
        }


    }
}
