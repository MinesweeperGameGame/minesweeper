namespace Minesweeper___game.Models
{
    public class Cell
    {
        public int minX;
        public int maxX;
        public int minY;
        public int maxY;
        // true = mine
        public int type;
        //notsure
        public bool isHidden;
        public Cell(int minX, int maxX, int minY, int maxY, int type)
        {
            this.minX = minX;
            this.maxX = maxX;
            this.minY = minY;
            this.maxY = maxY;
            this.type = type;
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
