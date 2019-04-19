using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGameLogic.TetrisActions
{
    public class TetrisUpDownAction : TetrisAction
    {
        private int _row;
        private int _direction = -1;

        public TetrisUpDownAction(ITetrisMatrix tetrisMatrix) : base(tetrisMatrix)
        {
            _row = tetrisMatrix.Height - 1;
            PeriodMs = 100;
        }

        private void FillRow(int row, TetrisCellState state)
        {
            for (int col = 0; col < _tetrisMatrix.Width; col++)
            {
                var cell = _tetrisMatrix.Cell(row, col);
                cell.Set(state, TetrisCellType.Ordinal);
            }
        }

        protected override void Step()
        {
            FillRow(_row, _direction == -1 ? TetrisCellState.Static : TetrisCellState.Empty);
            if (_row == 0 && _direction == -1)
            {
                _direction = 1;
            }
            else
            {
                _row += _direction;
            }

            if (_row == _tetrisMatrix.Height)
            {
                IsFinished = true;
            }
        }
    }
}
