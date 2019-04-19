using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGameLogic.TetrisActions
{
    public interface ITetrisAction
    {
        bool IsFinished { get; }
        void Tick();
    }
}
