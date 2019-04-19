using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGameLogic.TetrisActions
{
    public abstract class TetrisAction : ITetrisAction
    {
        protected ITetrisMatrix _tetrisMatrix;

        public bool IsFinished { get; protected set; }
        public int PeriodMs { get; set; } = 400;
        private DateTime _lastStepTime;

        public TetrisAction(ITetrisMatrix tetrisMatrix)
        {
            _tetrisMatrix = tetrisMatrix;
        }

        public void Tick()
        {
            if (IsFinished) return;

            if (_lastStepTime == default(DateTime))
            {
                _lastStepTime = DateTime.Now;
            }

            var ms = (DateTime.Now - _lastStepTime).TotalMilliseconds;
            if (ms >= PeriodMs)
            {
                _lastStepTime = _lastStepTime.AddMilliseconds(PeriodMs);
                Step();
            }
        }

        protected abstract void Step();
    }
}
