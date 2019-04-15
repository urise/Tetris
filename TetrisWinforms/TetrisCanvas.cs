using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisGameLogic;

namespace TetrisWinforms
{
    public class TetrisCanvas: IDisposable
    {
        public const int BORDER_WIDTH = 5;
        public int WidthPixels { get; private set; }
        public int HeightPixels { get; private set; }
        public int CellSize { get; private set; }
        private Graphics _graphics;
        private Brush _bordersBrush = new SolidBrush(Color.DarkBlue);
        private Brush _cellBrush = new SolidBrush(Color.DarkGreen);
        private Brush _backgroundBrush = new SolidBrush(Color.FromArgb(0xFF, 0xFF, 0xBC, 0x00));
        private Pen _linePen = new Pen(Color.Red);

        public TetrisCanvas(int heightPixels, int widthCells, int heightCells)
        {
            HeightPixels = heightPixels;
            CellSize = heightPixels / heightCells;
            WidthPixels = CellSize * widthCells + 2 * BORDER_WIDTH;
        }

        public void SetGraphics(Graphics graphics)
        {
            _graphics = graphics;
        }

        public void Draw(TetrisMatrix matrix)
        {
            _graphics.FillRectangle(_backgroundBrush, 0, 0, WidthPixels, HeightPixels);
            _graphics.FillRectangle(_bordersBrush, 0, 0, BORDER_WIDTH, HeightPixels);
            _graphics.FillRectangle(_bordersBrush, 0, HeightPixels - BORDER_WIDTH, WidthPixels, BORDER_WIDTH);
            _graphics.FillRectangle(_bordersBrush, WidthPixels - BORDER_WIDTH, 0, BORDER_WIDTH, HeightPixels);

            for (int row = 0; row < matrix.Height; row++)
            {
                for (int col = 0; col < matrix.Width; col++)
                {
                    if (matrix.Cell(row, col).State != TetrisCellState.Empty)
                    {
                        _graphics.FillRectangle(_cellBrush, BORDER_WIDTH + col * CellSize, row * CellSize, CellSize, CellSize);
                    }
                }
            }
        }

        private void DrawBorders()
        {

        }

        public void Dispose()
        {
            _bordersBrush.Dispose();
            _graphics.Dispose();
        }
    }
}
