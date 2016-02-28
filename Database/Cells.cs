using System.Collections.Generic;
using Minesweeper___game.Models;
using System;
using Minesweeper___game.View;

namespace Minesweeper___game.Datebase
{
    class Cells
    {
        public Cell[,] board;
        public List<Cell> notMineCells = new List<Cell>();
        public Cells()
        {

        }
        public void GenerateCels(int columns, int rows, int cellPositionX, int cellPositionY)
        {
            Game.isAlive = true;
            Board.isGenCellsType = false;
            board = new Cell[rows, columns];
            Board.cellsCount = Game.levels.levelsList[Game.level].height*Game.levels.levelsList[Game.level].height;
            Board.minesCount = Game.levels.levelsList[Game.level].mines;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Board.cells.AddNewCell(cellPositionX, cellPositionY, 0, i, j);
                    cellPositionX += 21;
                }
                cellPositionX = 10;
                cellPositionY += 21;
            }
        }
        public void AddNewCell(int x, int y, int type, int row, int column)
        {
            this.board[row, column] = new Cell(x, x + 20, y, y + 20, row, column, type);
        }

        public Cell getCellByCords(int x, int y)
        {
            foreach (Cell cell in board)
            {
                if (cell.isHidden && cell.minX <= x && cell.maxX >= x && cell.minY <= y && cell.maxY >= y)
                {
                    return cell;
                }
            }
            return null;
        }
        public void generateCellsType(Cell notMineCell)
        {

            Board.isGenCellsType = true;
            notMineCells.Add(notMineCell);
            FirstClickNeighbours(notMineCell.boardX - 1, notMineCell.boardY);
            FirstClickNeighbours(notMineCell.boardX + 1, notMineCell.boardY);
            FirstClickNeighbours(notMineCell.boardX, notMineCell.boardY - 1);
            FirstClickNeighbours(notMineCell.boardX, notMineCell.boardY + 1);
            FirstClickNeighbours(notMineCell.boardX - 1, notMineCell.boardY - 1);
            FirstClickNeighbours(notMineCell.boardX + 1, notMineCell.boardY - 1);
            FirstClickNeighbours(notMineCell.boardX - 1, notMineCell.boardY + 1);
            FirstClickNeighbours(notMineCell.boardX + 1, notMineCell.boardY + 1);
            generateMines();
            mineCheck();
        }
        public bool NeighboursTrack(int x, int y)
        {
            foreach (Cell cell in notMineCells)
            {
                if (Object.ReferenceEquals(this.board[x, y], cell))
                {
                    return false;
                }
            }
            return true;
        }
        public void FirstClickNeighbours(int x, int y)
        {
            if (x >= 0 && x < Game.levels.levelsList[Game.level].height
                && y >= 0 && y < Game.levels.levelsList[Game.level].width)
            {
                notMineCells.Add(this.board[x, y]);
            }
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
                        if (density == 0 && this.board[i, y].type != -1 && NeighboursTrack(i, y))
                        {
                            this.board[i, y].type = rand.Next(-1, 1);
                            if (this.board[i, y].type == -1)
                            {
                                mineTotal++;
                                density = Game.levels.levelsList[level].densityCheck;
                                if (mineTotal == Game.levels.levelsList[level].mines)
                                {
                                    return;
                                }
                            }
                        }
                        else if (density != 0)
                        {
                            density--;
                        }
                    }
                }
            }

        }
        public void mineCheck()
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
        public void neighbourCheck(int i, int y)
        {
            int level = Game.level;

            if (i >= 0 && i < Game.levels.levelsList[level].height && y >= 0 && y < Game.levels.levelsList[level].width)
            {
                if (this.board[i, y].type != -1)
                {
                    this.board[i, y].type++;
                }
            }
        }
    }
}
