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
        private int _cellSize;
        List<TetrisSingleShape> _shapes;

        public TetrisPredictionCanvas(Rectangle fullRect) : base(fullRect)
        {
            
        }

        public void Draw(List<TetrisSingleShape> shapes)
        {
            if (shapes.Count == 0) return;
            _shapes = shapes;
            DefineCellSize();
        }

        private void DefineCellSize()
        {
            int _cellSizeVer = _fullRect.Height / _shapes[0].FullSize;
            int _cellSizeHor = _fullRect.Width / ((_shapes[0].FullSize + 1) * _shapes.Count - 1);
            _cellSize = Math.Min(_cellSizeVer, _cellSizeHor);
        }
    }
}
