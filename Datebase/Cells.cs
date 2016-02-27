using System.Collections.Generic;
using Minesweeper___game.Models;

namespace Minesweeper___game.Datebase
{
    class Cells
    {
        public Cell[,] cells;

        public Cells()
        {
            
        }
        public void AddBoardSize (int columns, int rows)
        {
            cells = new Cell[rows, columns];
        }
        public void AddNewCell(int x, int y, int type, int row, int column)
        {
            this.cells[row,column] = new Cell(x, x + 20, y, y + 20, type);
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
