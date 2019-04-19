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
        private Brush _fallingBrush = new SolidBrush(Color.DarkGreen);
        private Brush _staticBrush = new SolidBrush(Color.FromArgb(0xFF, 0x11, 0x11, 0x11));
        private Brush _backgroundBrush = new SolidBrush(Color.FromArgb(0xFF, 0xFF, 0xBC, 0x00));
        private Brush _cellsForRemove = new SolidBrush(Color.Purple);

        private Pen _linePen = new Pen(Color.Red);

        public TetrisCanvas(int heightPixels, int widthCells, int heightCells)
        {
            CellSize = (heightPixels - BORDER_WIDTH) / heightCells;
            HeightPixels = CellSize * heightCells + BORDER_WIDTH;
            
            WidthPixels = CellSize * widthCells + 2 * BORDER_WIDTH;
        }

        public void SetGraphics(Graphics graphics)
        {
            _graphics = graphics;
        }

        private Brush GetBrushByCell(TetrisCell tetrisCell)
        {
            switch (tetrisCell.State)
            {
                case TetrisCellState.Falling:
                    return _fallingBrush;
                case TetrisCellState.Static:
                    switch (tetrisCell.CellType)
                    {
                        case TetrisCellType.Ordinal:
                            return _staticBrush;
                        case TetrisCellType.RemoveForWin:
                            return _cellsForRemove;
                    }
                    break;
            }
            return _backgroundBrush;
        }

        public void Draw(TetrisMatrix matrix)
        {
            _graphics.FillRectangle(_backgroundBrush, 0, 0, WidthPixels, HeightPixels);
            _graphics.FillRectangle(_bordersBrush, 0, 0, BORDER_WIDTH, HeightPixels);
            _graphics.FillRectangle(_bordersBrush, 0, HeightPixels - BORDER_WIDTH, WidthPixels, BORDER_WIDTH);
            _graphics.FillRectangle(_bordersBrush, WidthPixels - BORDER_WIDTH, 0, BORDER_WIDTH, HeightPixels);
            //var gameRectangle = new Rectangle(BORDER_WIDTH, 0, WidthPixels - 2 * BORDER_WIDTH, HeightPixels - BORDER_WIDTH);
            //_graphics.FillRectangle(new SolidBrush(Color.LightGreen), gameRectangle);

            for (int row = 0; row < matrix.Height; row++)
            {
                for (int col = 0; col < matrix.Width; col++)
                {
                    var state = matrix.Cell(row, col).State;
                    if (state != TetrisCellState.Empty)
                    {
                        var brush = GetBrushByCell(matrix.Cell(row, col));
                        _graphics.FillRectangle(brush, BORDER_WIDTH + col * CellSize, row * CellSize, CellSize, CellSize);
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
