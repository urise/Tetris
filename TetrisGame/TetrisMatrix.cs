using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisGameLogic.TetrisActions;
using TetrisGameLogic.TetrisScore;
using TetrisGameLogic.TetrisShapes;

namespace TetrisGameLogic
{
    public enum GameState
    {
        NotStarted,
        InProgress,
        Won,
        Lost
    }

    public class TetrisMatrix : ITetrisMatrix
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int FallPeriodMs { get; private set; } = 500;
        public int MovePeriodMs { get; private set; } = 100;
        public GameState State { get; private set; } = GameState.NotStarted;
        public int Score => _scoreCounter.Value;

        private TetrisCell[,] _cells;
        private ITetrisShapeLibrary _shapeLibrary;
        private ITetrisPredictionShapes _predictions;
        private ITetrisShape _currentShape;
        private TetrisCoords _shapeCoords = new TetrisCoords(0, 0);
        private List<TetrisCoords> _fallingCells = new List<TetrisCoords>();
        private List<TetrisKeys> _pressedKeys = new List<TetrisKeys>();
        private ITetrisAction _wonAction;
        private ITetrisAction _lostAction;
        private ITetrisScoreCounter _scoreCounter = new TetrisScoreCounter(); 

        private DateTime _lastTickTime;
        private DateTime _lastFallTime;
        private DateTime _lastMoveTime;

        public TetrisMatrix(int width, int height, ITetrisShapeLibrary shapeLibrary)
        {
            Width = width;
            Height = height;
            _shapeLibrary = shapeLibrary;
            _predictions = new TetrisPredictionShapes(_shapeLibrary);
            _cells = new TetrisCell[Height, Width];
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    _cells[row, col] = new TetrisCell();
                }
            }
            _wonAction = new TetrisUpDownAction(this);
            _lostAction = new TetrisFillFromDownAction(this);
        }

        public TetrisCell Cell(int row, int col)
        {
            return _cells[row, col];
        }

        private bool InBounds(int row, int col)
        {
            return row >= 0 && row < Height && col >= 0 && col < Width;
        }

        public bool IsEmpty()
        {
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    if (_cells[row, col].State != TetrisCellState.Empty)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void PutSingleShape()
        {
            if (_currentShape == null)
            {
                return;
            }
            var singleShape = _currentShape.GetCurrent();
            _fallingCells.Clear();
            for (int row = 0; row < singleShape.FullSize; row++)
            {
                for (int col = 0; col < singleShape.FullSize; col++)
                {
                    if (InBounds(_shapeCoords.Row + row, _shapeCoords.Col + col))
                    {
                        var coords = new TetrisCoords(_shapeCoords.Row + row, _shapeCoords.Col + col);
                        if (singleShape.IsFull(row, col))
                        {
                            _fallingCells.Add(coords);
                            _cells[coords.Row, coords.Col].SetState(TetrisCellState.Falling);
                        }
                        
                    }
                }
            }
        }

        public void Tick()
        {
            switch (State)
            {
                case GameState.InProgress:
                    TickInProgress();
                    break;
                case GameState.Won:
                    TickWon();
                    break;
                case GameState.Lost:
                    TickLost();
                    break;
            }
        }

        public void KeyDown(TetrisKeys tetrisKey)
        {
            if (!_pressedKeys.Contains(tetrisKey))
            {
                _pressedKeys.Add(tetrisKey);
            }

            if (State == GameState.NotStarted && tetrisKey == TetrisKeys.InstantFall)
            {
                State = GameState.InProgress;
                return;
            }

            if (State != GameState.InProgress)
            {
                return;
            }

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

        public void FillWithRemoveForWinCells(int numberOfRows)
        {
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int col = 0; col < Width; col++)
                {
                    var cell = _cells[Height - i - 1, col];
                    if ((i + col) % 2 == 0)
                    {
                        cell.Set(TetrisCellState.Empty, TetrisCellType.Ordinal);
                    }
                    else
                    {
                        cell.Set(TetrisCellState.Static, TetrisCellType.RemoveForWin);
                    }
                }
            }
        }

        private void PutNextShape()
        {
            _currentShape = _shapeLibrary.GetNextShape();
            _shapeCoords.Set(0, Width / 2 - 2);
            var singleShape = _currentShape.GetCurrent();
            if (ShapeCanBePlaced(singleShape, _shapeCoords))
            {
                PutSingleShape();
            }
            else
            {
                State = GameState.Lost;
            }
        }

        private void TickInProgress()
        {
            var now = DateTime.Now;
            if (_currentShape == null)
            {
                PutNextShape();
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

        private void TickWon()
        {
            _wonAction.Tick();
            if (_wonAction.IsFinished)
            {
                State = GameState.NotStarted;
            }
        }

        private void TickLost()
        {
            _lostAction.Tick();
            if (_wonAction.IsFinished)
            {
                State = GameState.NotStarted;
            }
        }

        private bool IsKeyPressed(TetrisKeys tetrisKey)
        {
            return _pressedKeys.Contains(tetrisKey);
        }

        private bool ShapeCanBePlaced(ITetrisSingleShape shape, TetrisCoords coords)
        {
            for (int row = 0; row < shape.FullSize; row++)
            {
                for (int col = 0; col < shape.FullSize; col++)
                {
                    if (shape.IsFull(row, col) && InBounds(coords.Row + row, coords.Col + col) &&
                        _cells[coords.Row + row, coords.Col + col].State == TetrisCellState.Static)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool ShapeCanTurn()
        {
            if (_currentShape == null)
            {
                return false;
            }

            var nextShape = _currentShape.GetNext();
            return ShapeCanBePlaced(nextShape, _shapeCoords);
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
                _cells[coords.Row, coords.Col].Set(state, TetrisCellType.Ordinal);
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
            for (int i = row; i >= 0; i--)
            {
                for (int col = 0; col < Width; col++)
                {
                    var newState = i > 0 && _cells[i - 1, col].State == TetrisCellState.Static ? TetrisCellState.Static : TetrisCellState.Empty;
                    _cells[i, col].Set(newState, TetrisCellType.Ordinal);
                }
            }
        }

        public void RemoveFullLines()
        {
            int counter = 0;
            int row = Height - 1;
            while (row > 0)
            {
                if (LineIsFull(row))
                {
                    RemoveLine(row);
                    counter++;
                }
                else
                {
                    row--;
                }
            }
            if (State == GameState.InProgress && counter > 0)
            {
                _scoreCounter.ProcessEvent((TetrisScoreEvents)counter);
            }

            if (State == GameState.InProgress && IsWon())
            {
                State = GameState.Won;
            }
        }

        private int GetCellTypeCount(TetrisCellType cellType)
        {
            var n = 0;

            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    if (_cells[row, col].CellType == cellType)
                    {
                        n++;
                    }
                }
            }
            return n;
        }

        private bool IsWon()
        {
            return GetCellTypeCount(TetrisCellType.RemoveForWin) == 0;
        }
    }
}
