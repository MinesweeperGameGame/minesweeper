namespace Minesweeper___game.Datebase
{
    public class Difficulty
    {
        public int height;
        public int width;
        public int mines;
        public int count;

        public Difficulty(int height, int width, int mines, int count)
        {
            this.height = height;
            this.width = width;
            this.mines = mines;
            this.count = count;
        }
    }
}
