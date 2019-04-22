using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGameLogic.TetrisScore
{
    public interface ITetrisScoreCounter
    {
        void Reset();
        void ProcessEvent(TetrisScoreEvents scoreEvent);
        int Value { get; }
    }
}
