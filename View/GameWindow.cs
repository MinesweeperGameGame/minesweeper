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
            if (Game.cells.CheckIsInCell(xMouseCords, yMouseCords))
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
                    CreateLevel(400, 300, 3);
                    break;
                case "button2":
                    //CreateLevel();
                    break;
                case "button3":
                    //CreateLevel();
                    break;
                default:
                    throw new ArgumentException("Wrong button!");
            }
        }

        private void CreateLevel(int windowWidth, int windowHeight, int minies) 
        {
            this.Controls.Clear();
            this.ClientSize = new System.Drawing.Size(windowWidth, windowHeight);
            this.CenterToScreen();

            int tableCellsX = windowWidth/40;
            int tableCellsY = windowHeight/50;

            DrawCells(tableCellsX, tableCellsY);
        }

        private void DrawCells(int tableCellsX, int tableCellsY)
        {
            this.Controls.Add(isCell);

            int brushPositionX = 10;
            int brushPositionY = 10;

            Rectangle cell;
            SolidBrush blueBrush = new SolidBrush(Color.Blue);

            for (int i = 0; i < tableCellsY; i++)
            {
                for (int j = 0; j < tableCellsX; j++)
                {
                    Game.cells.AddNewCell(brushPositionX, brushPositionY, 0);
                    cell = new Rectangle(brushPositionX, brushPositionY, 20, 20);
                    g.FillRectangle(blueBrush, cell);
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
