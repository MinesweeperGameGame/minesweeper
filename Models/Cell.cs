namespace Minesweeper___game.Models
{
    public class Cell
    {
        public int minX;
        public int maxX;
        public int minY;
        public int maxY;
        public int boardX;
        public int boardY;
        // true = mine
        public int type;
        //notsure
        public bool isHidden;
        public bool isFlagged;

        public Cell(int minX, int maxX, int minY, int maxY, int boardX, int boardY, int type)
        {
            this.minX = minX;
            this.maxX = maxX;
            this.minY = minY;
            this.maxY = maxY;

            this.boardX = boardX;
            this.boardY = boardY;

            this.type = type;
            this.isHidden = true;
        }

        public bool isMinе()
        {
            if (this.type == -1)
            {
                return true;
            }
            return false;
        }
    }
}
