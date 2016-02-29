using Minesweeper___game.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper___game.Models
{
    public partial class GameWindow : Form
    {
        private const int CellsStartingPoint = 10;
        private Graphics g;
        private int xMouseCords = 0;
        private int yMouseCords = 0;

        public GameWindow()
        {
            InitializeComponent();
            g = CreateGraphics();
        }

        private void HomeWindow_Load(object sender, EventArgs e)
        {
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (Game.level > -1  && Game.isAlive)
            {
                base.OnMouseMove(e);
                xMouseCords = e.X;
                yMouseCords = e.Y;
                Cell currentClick = Board.cells.getCellByCords(xMouseCords, yMouseCords);
                if (currentClick != null)
                {
                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        if (currentClick.type == -1)
                        {
                            Game.isAlive = false;
                            DrawCells(Game.levels.levelsList[Game.level].width, Game.levels.levelsList[Game.level].height);
                        }
                        if (!Board.isGenCellsType)
                        {
                            Board.cells.generateCellsType(currentClick);
                        }
                        Board.revealCells(currentClick.boardX, currentClick.boardY);
                        if (Board.cellsCount == Board.minesCount)
                        {
                            this.restart.Text = "AGAIN";
                        }

                    }

                    else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    {
                        if (!currentClick.isFlagged)
                        {
                            currentClick.isFlagged = true;
                        }
                        else
                        {
                            currentClick.isFlagged = false;
                        }
                    }

                    DrawCells(Game.levels.levelsList[Game.level].width, Game.levels.levelsList[Game.level].height);
                }
            }
        }

        private void ChoiceLevel(object sender, EventArgs e)
        {
            Button button = sender as Button;
            switch (button.Name)
            {
                case "button1":
                    CreateLevel(0);
                    break;
                case "button2":
                    CreateLevel(1);
                    break;
                case "button3":
                    CreateLevel(2);
                    break;
                default:
                    throw new ArgumentException("Wrong button!");
            }
        }

        private void CreateLevel(int difficulty)
        {
            int windowWidth = Game.levels.levelsList[difficulty].width * 30;
            int windowHeight = Game.levels.levelsList[difficulty].height * 30;
            int tableCellsX = windowWidth / 30;
            int tableCellsY = windowHeight / 30;

            Game.level = difficulty;
            Board.cells.GenerateCels(tableCellsX, tableCellsY, CellsStartingPoint, CellsStartingPoint);

            this.Controls.Clear();
            this.ClientSize = new System.Drawing.Size(windowWidth, windowHeight);
            this.CenterToScreen();

            this.restart.Visible = true;
            this.restart.Location = new Point(10, windowHeight - 50);
            this.Controls.Add(this.restart);

            DrawCells(tableCellsX, tableCellsY);
        }

        private void DrawCells(int tableCellsX, int tableCellsY)
        {
            Rectangle cell;
            SolidBrush brush = new SolidBrush(Color.Gray);

            int brushPositionX = CellsStartingPoint;
            int brushPositionY = CellsStartingPoint;

            for (int i = 0; i < tableCellsY; i++)
            {
                for (int j = 0; j < tableCellsX; j++)
                {
                    Cell currentCell = Board.cells.board[i, j];
                    brush.Color = Color.Gray;
                    string cellType = String.Empty;

                    if (currentCell.isFlagged)
                    {
                        brush.Color = Color.Green;
                    }
                    if (!currentCell.isHidden)
                    {
                        if (currentCell.type >= 0)
                        {
                            brush.Color = Color.White;
                        }
                        else
                        {
                            brush.Color = Color.Red;
                        }
                        if (currentCell.type > 0)
                        {
                            cellType = currentCell.type.ToString();
                        }
                    }
                    else if(!Game.isAlive && currentCell.type == -1)
                    {
                        brush.Color = Color.Red;
                    }

                    Font drawFont = new Font("Arial", 15);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    cell = new Rectangle(brushPositionX, brushPositionY, 20, 20);

                    g.FillRectangle(brush, cell);
                    g.DrawString(cellType, drawFont, drawBrush, brushPositionX, brushPositionY);
                    brushPositionX += 21;
                }
                brushPositionX = CellsStartingPoint;
                brushPositionY += 21;
            }
        }
        //Method to stop redraw when press ALT
        protected override void WndProc(ref Message m)
        {
            // Suppress the WM_UPDATEUISTATE message
            if (m.Msg == 0x128) return;
            base.WndProc(ref m);
        }

        private void ResetLevel(object sender, EventArgs e)
        {
            int windowWidth = Game.levels.levelsList[Game.level].width * 30;
            int windowHeight = Game.levels.levelsList[Game.level].height * 30;
            int tableCellsX = windowWidth / 30;
            int tableCellsY = windowHeight / 30;
            
            Board.cells.GenerateCels(tableCellsX, tableCellsY, CellsStartingPoint, CellsStartingPoint);
            DrawCells(tableCellsX, tableCellsY);
        }

        private void ResetLevel()
        {
            int windowWidth = Game.levels.levelsList[Game.level].width * 30;
            int windowHeight = Game.levels.levelsList[Game.level].height * 30;
            int tableCellsX = windowWidth / 30;
            int tableCellsY = windowHeight / 30;

            Board.cells.GenerateCels(tableCellsX, tableCellsY, CellsStartingPoint, CellsStartingPoint);
            DrawCells(tableCellsX, tableCellsY);
        }
    }
}
