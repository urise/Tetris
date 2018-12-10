using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisWinforms
{
    public class TetrisCanvas
    {
        public const int BORDER_WIDTH = 5;
        public int WidthPixels { get; private set; }
        public int HeightPixels { get; private set; }
        public int CellSize { get; private set; }

        public TetrisCanvas(int heightPixels, int widthCells, int heightCells)
        {
            HeightPixels = heightPixels;
            CellSize = heightPixels / heightCells;
            WidthPixels = CellSize * widthCells + 2 * BORDER_WIDTH;
        }

        public void Draw(Graphics g)
        {
            var myBrush = new SolidBrush(Color.Blue);
            g.FillRectangle(myBrush, 0, 0, BORDER_WIDTH, HeightPixels);
            g.FillRectangle(myBrush, 0, HeightPixels, WidthPixels, BORDER_WIDTH);
            g.FillRectangle(myBrush, WidthPixels - BORDER_WIDTH, 0, WidthPixels, HeightPixels);
            myBrush.Dispose();
        }
    }
}
