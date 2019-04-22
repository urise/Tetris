using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGameLogic.TetrisScore
{
    public enum TetrisScoreEvents
    {
        OneLine = 1, 
        TwoLines = 2,
        ThreeLines = 3,
        FourLines = 4
    }

    public class TetrisScoreCounter : ITetrisScoreCounter
    {
        private Dictionary<TetrisScoreEvents, int> _rates = new Dictionary<TetrisScoreEvents, int>
        {
            { TetrisScoreEvents.OneLine, 1 },
            { TetrisScoreEvents.TwoLines, 3 },
            { TetrisScoreEvents.ThreeLines, 8 },
            { TetrisScoreEvents.FourLines, 15 }
        };
        public int Value { get; private set; }

        public void Reset()
        {
            Value = 0;
        }

        public void ProcessEvent(TetrisScoreEvents scoreEvent)
        {
            Value += _rates[scoreEvent];
        }
    }
}
