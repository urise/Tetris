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
        public int FallPeriodMs { get; private set; } = 200;

        private TetrisCell[,] _cells;
        private ITetrisShapeLibrary _shapeLibrary;
        private TetrisShape _currentShape;
        private TetrisCoords _shapeCoords = new TetrisCoords(0, 0);
        private Random _random = new Random();

        private DateTime _lastTickTime;
        private DateTime _lastFallTime;

        public TetrisMatrix(int width, int height, ITetrisShapeLibrary shapeLibrary)
        {
            Width = width;
            Height = height;
            _shapeLibrary = shapeLibrary;
            _cells = new TetrisCell[Height, Width];
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    _cells[row, col] = new TetrisCell { State = TetrisCellState.Empty };
                }
            }
        }

        public TetrisCell Cell(int row, int col)
        {
            return _cells[row, col];
        }

        private bool InBounds(int row, int col)
        {
            return row >= 0 && row < Height && col >= 0 && col < Width;
        }

        private void PutSingleShape()
        {
            var singleShape = _currentShape.Current();
            for (int row = 0; row < singleShape.Size; row++)
            {
                for (int col = 0; col < singleShape.Size; col++)
                {
                    if (InBounds(_shapeCoords.Row + row, _shapeCoords.Col + col))
                    {
                        _cells[_shapeCoords.Row + row, _shapeCoords.Col + col].State = singleShape.IsFull(row, col) ? TetrisCellState.Falling : TetrisCellState.Empty;
                    }
                }
            }
        }

        public void Tick()
        {
            var now = DateTime.Now;
            if (_currentShape == null)
            {
                _currentShape = _shapeLibrary.GetNextShape();
                _shapeCoords.Set(0, Width / 2 - 2);
                var singleShape = _currentShape.Next();
                PutSingleShape();
                _lastFallTime = now;
            }
            else
            {
                var timeSpentMs = (now - _lastFallTime).TotalMilliseconds;
                if (timeSpentMs > FallPeriodMs)
                {
                    ShapeFall();
                    _lastFallTime = _lastFallTime.AddMilliseconds(FallPeriodMs);
                }
            }
            _lastTickTime = now;
        }

        private bool ShapeCanFall()
        {
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    if (_cells[row, col].State != TetrisCellState.Falling) continue;

                    if (row == Height - 1) return false;
                }
            }
            return true;
        }

        private void ClearShape()
        {
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    if (_cells[row, col].State == TetrisCellState.Falling)
                    {
                        _cells[row, col].State = TetrisCellState.Empty;
                    }
                }
            }
        }

        private void ShapeFall()
        {
            if (ShapeCanFall())
            {
                ClearShape();
                _shapeCoords.Row += 1;
                PutSingleShape();
            }
            else
            {
                ClearShape();
                _currentShape = null;
            }
        }
    }
}
