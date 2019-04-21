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
        public const int BORDER_HEIGHT = 5;
        public const int CENTRAL_SEPARATOR_WIDTH = 10;
        public const int CELL_SEPARATOR_SIZE = 3;
        //public int WidthPixels { get; private set; }
        //public int HeightPixels { get; private set; }
        public int CellSize { get; private set; }
        private Graphics _graphics;
        private Brush _bordersBrush = new SolidBrush(Color.DarkBlue);
        private Brush _fallingBrush = new SolidBrush(Color.DarkGreen);
        private Brush _staticBrush = new SolidBrush(Color.FromArgb(0xFF, 0x11, 0x11, 0x11));
        private Brush _backgroundBrush = new SolidBrush(Color.FromArgb(0xFF, 0xFF, 0xBC, 0x00));
        private Brush _cellsForRemove = new SolidBrush(Color.Purple);
        private Brush _cellSeparatorBrush = new SolidBrush(Color.DarkGray);
        private int _leftWidth;
        private int _rightWidth;
        private Rectangle _leftRectangle;
        private Rectangle _gameRectangle;
        private Rectangle _gameRectangleWithBorders;

        private Pen _linePen = new Pen(Color.Red);

        public TetrisCanvas(int widthPixels, int heightPixels, int widthCells, int heightCells)
        {
            _leftWidth = (widthPixels - CENTRAL_SEPARATOR_WIDTH) / 2;
            _rightWidth = widthPixels - CENTRAL_SEPARATOR_WIDTH - _leftWidth;
            _leftRectangle = new Rectangle(0, 0, _leftWidth, heightPixels);

            int cellSizeByWidth = (_leftRectangle.Width - BORDER_WIDTH * 2 - CELL_SEPARATOR_SIZE * (widthCells - 1)) / widthCells;
            int cellSizeByHeight = (_leftRectangle.Height - BORDER_HEIGHT - CELL_SEPARATOR_SIZE * (heightCells - 1)) / heightCells;

            CellSize = Math.Min(cellSizeByWidth, cellSizeByHeight);

            var gameHeight = CellSize * heightCells + BORDER_HEIGHT + CELL_SEPARATOR_SIZE * (heightCells - 1);
            var gameWidth = CellSize * widthCells + BORDER_WIDTH * 2 + CELL_SEPARATOR_SIZE * (widthCells - 1);
            _gameRectangleWithBorders = new Rectangle((_leftRectangle.Width - gameWidth) / 2, (_leftRectangle.Height - gameHeight) / 2, gameWidth, gameHeight);
            var gb = _gameRectangleWithBorders;
            _gameRectangle = new Rectangle(gb.Left + BORDER_WIDTH, gb.Top, gb.Width - BORDER_WIDTH * 2, gb.Height - BORDER_HEIGHT);

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
            var gb = _gameRectangleWithBorders;
            _graphics.FillRectangle(_backgroundBrush, gb);
            _graphics.FillRectangle(_bordersBrush, gb.Left, gb.Top, BORDER_WIDTH, gb.Height);
            _graphics.FillRectangle(_bordersBrush, gb.Left, gb.Bottom - BORDER_HEIGHT, gb.Width, BORDER_HEIGHT);
            _graphics.FillRectangle(_bordersBrush, gb.Right - BORDER_WIDTH, gb.Top, BORDER_WIDTH, gb.Height);
            //var gameRectangle = new Rectangle(BORDER_WIDTH, 0, WidthPixels - 2 * BORDER_WIDTH, HeightPixels - BORDER_WIDTH);
            //_graphics.FillRectangle(new SolidBrush(Color.LightGreen), gameRectangle);

            var g = _gameRectangle;
            for (int row = 0; row < matrix.Height; row++)
            {
                for (int col = 0; col < matrix.Width; col++)
                {
                    var state = matrix.Cell(row, col).State;
                    if (state != TetrisCellState.Empty)
                    {
                        var brush = GetBrushByCell(matrix.Cell(row, col));
                        _graphics.FillRectangle(brush, g.Left + col * (CellSize + CELL_SEPARATOR_SIZE), g.Top + row * (CellSize + CELL_SEPARATOR_SIZE), CellSize, CellSize);
                        if (col > 0)
                        {
                            _graphics.FillRectangle(_cellSeparatorBrush, g.Left + col * CellSize + (col - 1) * CELL_SEPARATOR_SIZE, g.Top, CELL_SEPARATOR_SIZE, g.Bottom);
                        }
                    }
                }
                if (row > 0)
                {
                    _graphics.FillRectangle(_cellSeparatorBrush, g.Left, g.Top + row * CellSize + (row - 1) * CELL_SEPARATOR_SIZE, g.Width, CELL_SEPARATOR_SIZE);
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
