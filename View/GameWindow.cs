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
            if (Game.level > -1)
            {
                base.OnMouseMove(e);
                xMouseCords = e.X;
                yMouseCords = e.Y;
                Cell currentClick = Board.cells.getCellByCords(xMouseCords, yMouseCords);
                if (currentClick != null)
                {
                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    {

                        if (!Board.isGenCellsType)
                        {
                            Board.cells.generateCellsType(currentClick);
                        }
                        if (currentClick.type == -1)
                        {

                        }
                        else
                        {
                            //work in progress
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

            DrawCells(tableCellsX, tableCellsY);
        }

        private void DrawCells(int tableCellsX, int tableCellsY)
        {
            g.Clear(Color.White);

            Rectangle cell;
            SolidBrush brush = new SolidBrush(Color.Gray);

            int brushPositionX = CellsStartingPoint;
            int brushPositionY = CellsStartingPoint;

            for (int i = 0; i < tableCellsY; i++)
            {
                for (int j = 0; j < tableCellsX; j++)
                {
                    brush.Color = Color.Gray;

                    if (Board.cells.board[i, j].type == -1)
                    {
                        brush.Color = Color.Red;
                    }
                    if (Board.cells.board[i, j].isFlagged)
                    {
                        brush.Color = Color.Green;
                    }

                    //string cellType = "";
                    //Font drawFont = new Font("Arial", 15);
                    //SolidBrush drawBrush = new SolidBrush(Color.Black);
                    cell = new Rectangle(brushPositionX, brushPositionY, 20, 20);

                    g.FillRectangle(brush, cell);
                    //g.DrawString(cellType, drawFont, drawBrush, brushPositionX, brushPositionY);
                    brushPositionX += 22;
                }
                brushPositionX = CellsStartingPoint;
                brushPositionY += 22;
            }
        }
        //Method to stop redraw when press ALT
        protected override void WndProc(ref Message m)
        {
            // Suppress the WM_UPDATEUISTATE message
            if (m.Msg == 0x128) return;
            base.WndProc(ref m);
        }
    }
}
