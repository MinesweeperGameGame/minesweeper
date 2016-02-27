namespace Minesweeper___game.Datebase
{
    public class Difficulty
    {
        public int height;
        public int width;
        public int mines;
        public int densityCheck;

        public Difficulty(int height, int width, int mines, int densityCheck)
        {
            this.height = height;
            this.width = width;
            this.mines = mines;
            this.densityCheck = densityCheck;
        }
    }
}
