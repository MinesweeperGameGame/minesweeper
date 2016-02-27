using System.Collections.Generic;
using Minesweeper___game.Models;
using System;

namespace Minesweeper___game.Datebase
{
    class Cells
    {
        public Cell[,] board;

        public Cells()
        {

        }
        public void AddBoardSize(int columns, int rows)
        {
            board = new Cell[rows, columns];
        }
        public void AddNewCell(int x, int y, int type, int row, int column)
        {
            this.board[row, column] = new Cell(x, x + 20, y, y + 20, type);
        }

        public bool CheckIsInCell(int x, int y)
        {
            foreach (Cell cell in board)
            {
                if (cell.minX <= x && cell.maxX >= x && cell.minY <= y && cell.maxY >= y)
                {
                    return true;
                }
            }
            return false;
        }
        public void generateCellsType ()
        {
            generateMines();
            mineCheck();
        }
        public void generateMines()
        {
            int level = Game.level;
            int density = 0;
            int mineTotal = 0;
            Random rand = new Random();
            while (mineTotal < Game.levels.levelsList[level].mines)
            {
                for (int i = 0; i < Game.levels.levelsList[level].height; i++)
                {
                    for (int y = 0; y < Game.levels.levelsList[level].width; y++)
                    {
                        if (density == 0)
                        {
                            this.board[i, y].type = rand.Next(-1, 1);
                            if (this.board[i, y].type == -1)
                            {
                                mineTotal++;
                                density = Game.levels.levelsList[level].densityCheck;
                            }
                        }
                        else density--;
                    }
                    if (mineTotal == Game.levels.levelsList[level].mines)
                    {
                        break;
                    }
                }
            }
        }
        public void mineCheck ()
        {
            int level = Game.level;

            for (int i = 0; i < Game.levels.levelsList[level].height; i++)
            {
                for (int y = 0; y < Game.levels.levelsList[level].width; y++)
                {
                    if (this.board[i, y].type == -1)
                    {
                        neighbourCheck(i - 1, y);
                        neighbourCheck(i + 1, y);
                        neighbourCheck(i, y - 1);
                        neighbourCheck(i, y + 1);
                        neighbourCheck(i - 1, y - 1);
                        neighbourCheck(i + 1, y - 1);
                        neighbourCheck(i - 1, y + 1);
                        neighbourCheck(i + 1, y + 1);
                    }
                }
            }
        }
        public void neighbourCheck (int i, int y)
        {
            int level = Game.level;

            if (i>=0 && i<Game.levels.levelsList[level].height && y>=0 && y<Game.levels.levelsList[level].width)
            {
                if (this.board[i,y].type != -1)
                {
                    this.board[i, y].type++;
                }
            }
        }
    }
}
