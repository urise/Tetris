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
        public int FallPeriodMs { get; private set; } = 500;
        public int MovePeriodMs { get; private set; } = 100;

        private TetrisCell[,] _cells;
        private ITetrisShapeLibrary _shapeLibrary;
        private TetrisShape _currentShape;
        private TetrisCoords _shapeCoords = new TetrisCoords(0, 0);
        private Random _random = new Random();
        private List<TetrisCoords> _fallingCells = new List<TetrisCoords>();
        private List<TetrisKeys> _pressedKeys = new List<TetrisKeys>();

        private DateTime _lastTickTime;
        private DateTime _lastFallTime;
        private DateTime _lastMoveTime;

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
            var singleShape = _currentShape.GetCurrent();
            _fallingCells.Clear();
            for (int row = 0; row < singleShape.Size; row++)
            {
                for (int col = 0; col < singleShape.Size; col++)
                {
                    if (InBounds(_shapeCoords.Row + row, _shapeCoords.Col + col))
                    {
                        var coords = new TetrisCoords(_shapeCoords.Row + row, _shapeCoords.Col + col);
                        if (singleShape.IsFull(row, col))
                        {
                            _fallingCells.Add(coords);
                            _cells[coords.Row, coords.Col].State = TetrisCellState.Falling;
                        }
                        
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
                var singleShape = _currentShape.GetCurrent();
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
                if (IsKeyPressed(TetrisKeys.Left) ^ IsKeyPressed(TetrisKeys.Right))
                {
                    var timeMoveSpentMs = (now - _lastMoveTime).TotalMilliseconds;
                    if (timeMoveSpentMs > MovePeriodMs)
                    {
                        ShapeMove(IsKeyPressed(TetrisKeys.Left) ? -1 : 1);
                    }
                }
            }
            _lastTickTime = now;
        }

        public void KeyDown(TetrisKeys tetrisKey)
        {
            _pressedKeys.Add(tetrisKey);
            switch (tetrisKey)
            {
                case TetrisKeys.Left:
                    ShapeMove(-1);
                    break;
                case TetrisKeys.Right:
                    ShapeMove(1);
                    break;
                case TetrisKeys.Turn:
                    TurnShape();
                    break;
                case TetrisKeys.InstantFall:
                    InstantFall();
                    break;
            }
        }

        public void KeyUp(TetrisKeys tetrisKey)
        {
            _pressedKeys.Remove(tetrisKey);
        }

        public void InstantFall()
        {
            while (ShapeCanMove(1, 0))
            {
                ShapeFall();
            }
        }

        public void TurnShape()
        {
            if (ShapeCanTurn())
            {
                TransformShape(TetrisCellState.Empty);
                _currentShape.SwitchToNext();
                PutSingleShape();
            }
        }

        private bool IsKeyPressed(TetrisKeys tetrisKey)
        {
            return _pressedKeys.Contains(tetrisKey);
        }

        private bool ShapeCanTurn()
        {
            var nextShape = _currentShape.GetNext();
            for (int row = 0; row < nextShape.Size; row++)
            {
                for (int col = 0; col < nextShape.Size; col++)
                {
                    if (nextShape.IsFull(row, col) && InBounds(_shapeCoords.Row + row, _shapeCoords.Col + col) && 
                        _cells[_shapeCoords.Row + row, _shapeCoords.Col + col].State == TetrisCellState.Static)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool ShapeCanMove(int directionVer, int directionHor)
        {
            foreach (var coords in _fallingCells)
            {
                var row = coords.Row + directionVer;
                var col = coords.Col + directionHor;
                if (!InBounds(row, col) || _cells[row, col].State == TetrisCellState.Static)
                {
                    return false;
                }
            }
            return true;
        }

        private void TransformShape(TetrisCellState state)
        {
            foreach (var coords in _fallingCells)
            {
                _cells[coords.Row, coords.Col].State = state;
            }
        }

        private void ShapeFall()
        {
            if (ShapeCanMove(1, 0))
            {
                TransformShape(TetrisCellState.Empty);
                _shapeCoords.Row += 1;
                PutSingleShape();
            }
            else
            {
                TransformShape(TetrisCellState.Static);
                _currentShape = null;
                RemoveFullLines();
            }
        }

        private void ShapeMove(int directionHor)
        {
            if (ShapeCanMove(0, directionHor))
            {
                TransformShape(TetrisCellState.Empty);
                _shapeCoords.Col += directionHor;
                PutSingleShape();
                _lastMoveTime = DateTime.Now;
            }
        }

        private bool LineIsFull(int row)
        {
            for (int col = 0; col < Width; col++)
            {
                if (_cells[row, col].State != TetrisCellState.Static)
                {
                    return false;
                }
            }
            return true;
        }

        private void RemoveLine(int row)
        {
            for (int i = row; i > 0; i--)
            {
                for (int col = 0; col < Width; col++)
                {
                    _cells[i, col].State = _cells[i - 1, col].State == TetrisCellState.Static ? TetrisCellState.Static : TetrisCellState.Empty;
                }
            }
        }

        private void RemoveFullLines()
        {
            int row = Height - 1;
            while (row > 0)
            {
                if (LineIsFull(row))
                {
                    RemoveLine(row);
                }
                else
                {
                    row--;
                }
            }
        }
    }
}
