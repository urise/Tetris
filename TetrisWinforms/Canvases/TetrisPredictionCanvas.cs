using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisGameLogic.TetrisShapes;

namespace TetrisWinforms.Canvases
{
    public class TetrisPredictionCanvas : TetrisBaseCanvas
    {
        private const int BORDER_SIZE = 10;
        private int _cellPixels;
        List<ITetrisSingleShape> _shapes;

        public TetrisPredictionCanvas(Graphics graphics, Rectangle fullRect) : base(graphics, fullRect)
        {
            
        }

        public void Draw(List<ITetrisShape> shapes)
        {
            if (shapes.Count == 0) return;
            _shapes = shapes.Select(s => s.GetCurrent()).ToList();
            DefineCellSize();
            var shapeSize = _shapes.Max(s => s.FullSize);
            var fullWidth = _cellPixels * (shapeSize + 1) * _shapes.Count - _cellPixels;
            var fullHeight = _cellPixels * shapeSize;
            var left = _fullRect.Left + (_fullRect.Width - fullWidth) / 2;
            var top = _fullRect.Top + (_fullRect.Height - fullHeight) / 2;

            _graphics.FillRectangle(_options.BackgroundBrush, _fullRect);

            for (var i = 0; i < shapes.Count; i++)
            {
                DrawShape(_shapes[i], left + i * (shapeSize + 1) * _cellPixels, top);
            }
        }

        private void DrawShape(ITetrisSingleShape shape, int left, int top)
        {
            for (int row = 0; row < shape.FullSize; row++)
            {
                for (int col = 0; col < shape.FullSize; col++)
                {
                    if (shape.IsFull(row, col))
                    {
                        _graphics.FillRectangle(_options.FallingBrush, left + col * _cellPixels, top + row * _cellPixels, _cellPixels, _cellPixels);
                    }
                }
            }
        }

        private void DefineCellSize()
        {
            int _cellSizeVer = (_fullRect.Height - BORDER_SIZE * 2)/ _shapes[0].FullSize;
            int _cellSizeHor = (_fullRect.Width - BORDER_SIZE * 2)/ ((_shapes[0].FullSize + 1) * _shapes.Count - 1);
            _cellPixels = Math.Min(_cellSizeVer, _cellSizeHor);
        }
    }
}
