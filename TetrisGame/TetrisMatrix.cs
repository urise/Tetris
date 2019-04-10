using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGameLogic
{
    public class TetrisMatrix
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Random _random = new Random();

        private TetrisCell[,] _cells;
        private ITetrisShapeLibrary _shapeLibrary;
        private TetrisShape _currentShape;
        private TetrisCoords _shapeCoords = new TetrisCoords(0, 0);

        public TetrisMatrix(int width, int height, ITetrisShapeLibrary shapeLibrary)
        {
            Width = width;
            Height = height;
            _shapeLibrary = shapeLibrary;
            _cells = new TetrisCell[Width, Height];
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    _cells[i, j] = new TetrisCell { State = TetrisCellState.Empty };
                }
            }
        }

        public TetrisCell Cell(int row, int column)
        {
            return _cells[row, column];
        }

        private void PutSingleShape()
        {
            var singleShape = _currentShape.Current();
            for (int x = 0; x < singleShape.Size; x++)
            {
                for (int y = 0; y < singleShape.Size; y++)
                {
                    _cells[_shapeCoords.X + x, _shapeCoords.Y + y].State = singleShape.IsFull(x, y) ? TetrisCellState.Static : TetrisCellState.Empty;
                }
            }
        }

        public void Tick()
        {
            if (_currentShape == null)
            {
                _currentShape = _shapeLibrary.GetNextShape();
                _shapeCoords.Set(Width / 2 - 2, 0);
                var singleShape = _currentShape.Next();
                PutSingleShape();
            }

            //var x = _random.Next(0, Width);
            //var y = _random.Next(0, Height);
            //_cells[x, y].State = TetrisCellState.Static;
            //x = _random.Next(0, Width);
            //y = _random.Next(0, Height);
            //_cells[x, y].State = TetrisCellState.Empty;
        }
    }
}
