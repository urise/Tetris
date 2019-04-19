using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGameLogic.TetrisActions
{
    public class TetrisFillFromDownAction : TetrisAction
    {
        private int _col;
        private int _direction = 1;

        public TetrisFillFromDownAction(ITetrisMatrix tetrisMatrix) : base(tetrisMatrix)
        {
            PeriodMs = 10;
        }

        protected override void Step()
        {
            if (_col >= 0 && _col < _tetrisMatrix.Width)
            {
                _tetrisMatrix.Cell(_tetrisMatrix.Height - 1, _col).Set(TetrisCellState.Static, TetrisCellType.Ordinal);
                _col += _direction;
                return;
            }

            _direction = _col == _tetrisMatrix.Width ? -1 : 1;
            _col += _direction;

            _tetrisMatrix.RemoveFullLines();
            if (_tetrisMatrix.IsEmpty())
            {
                IsFinished = true;
            }
        }
    }
}
