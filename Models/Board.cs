using Minesweeper___game.Datebase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minesweeper___game.Models;

namespace Minesweeper___game.View
{
    class Board
    {
        public static Cells cells = new Cells();
        public static bool isGenCellsType;

        public static void revealCells(int cellX, int cellY)
        {
            if (cellX >= 0 && cellX < Game.levels.levelsList[Game.level].height && cellY >= 0 && cellY < Game.levels.levelsList[Game.level].width)
            {
                Cell currentCell = Board.cells.board[cellX, cellY];

                if (currentCell != null && !currentCell.isFlagged && currentCell.isHidden)
                {
                    if (currentCell.type == -1)
                    {
                        return;
                    }
                    currentCell.isHidden = false;
                    if (currentCell.type == 0)
                    {
                        revealCells(cellX - 1, cellY);
                        revealCells(cellX + 1, cellY);
                        revealCells(cellX, cellY - 1);
                        revealCells(cellX, cellY + 1);
                        revealCells(cellX - 1, cellY - 1);
                        revealCells(cellX + 1, cellY - 1);
                        revealCells(cellX - 1, cellY + 1);
                        revealCells(cellX + 1, cellY + 1);
                    }
                }
            }
        }
    }
}
