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
        private Graphics g;
        private Label isCell = new Label();
        private int xMouseCords = 0;
        private int yMouseCords = 0;

        public GameWindow()
        {
            InitializeComponent();
            g = CreateGraphics();

            //testing output label
            isCell.Location = new System.Drawing.Point(10, 200);
            isCell.Name = "label";
            isCell.Text = "";
            isCell.Size = new System.Drawing.Size(77, 21);
        }

        private void HomeWindow_Load(object sender, EventArgs e)
        {
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            xMouseCords = e.X;
            yMouseCords = e.Y;
            if (Game.level > -1 && Game.cells.CheckIsInCell(xMouseCords, yMouseCords))
            {
                isCell.Text = "TRUE";
            }
            else
            {
                isCell.Text = "FLASE";
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
            Game.level = difficulty;
            int windowWidth = Game.levels.levelsList[difficulty].width*30;
            int windowHeight = Game.levels.levelsList[difficulty].height*30;
            this.Controls.Clear();
            this.ClientSize = new System.Drawing.Size(windowWidth, windowHeight);
            this.CenterToScreen();

            int tableCellsX = windowWidth/30;
            int tableCellsY = windowHeight/30;

            DrawCells(tableCellsX, tableCellsY);
        }

        private void DrawCells(int tableCellsX, int tableCellsY)
        {
            this.Controls.Add(isCell);

            int brushPositionX = 10;
            int brushPositionY = 10;

            Rectangle cell;
            SolidBrush blueBrush = new SolidBrush(Color.Blue);

            Game.cells.AddBoardSize(tableCellsX, tableCellsY);
            for (int i = 0; i < tableCellsY; i++)
            {
                for (int j = 0; j < tableCellsX; j++)
                {
                    Game.cells.AddNewCell(brushPositionX, brushPositionY, 0, i, j);
                    cell = new Rectangle(brushPositionX, brushPositionY, 20, 20);
                    brushPositionX += 22;
                }
                brushPositionX = 10;
                brushPositionY += 22;
            }
            Game.cells.generateCellsType();
            brushPositionX = brushPositionY = 10;
            for (int i = 0; i < tableCellsY; i++)
            {
                for (int j = 0; j < tableCellsX; j++)
                {
                    string cellType = "";
                    Font drawFont = new Font("Arial", 16);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    cell = new Rectangle(brushPositionX, brushPositionY, 20, 20);
                    blueBrush.Color = Color.White;
                    if (Game.cells.board[i, j].type == -1)
                    {
                        blueBrush.Color = Color.Red;
                    }
                    if (Game.cells.board[i, j].type > 0)
                    {
                        cellType = Game.cells.board[i, j].type.ToString();
                    }

                    g.FillRectangle(blueBrush, cell);
                    g.DrawString(cellType, drawFont, drawBrush, brushPositionX, brushPositionY);
                    brushPositionX += 22;
                }
                brushPositionX = 10;
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
