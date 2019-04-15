using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGameLogic
{
    public enum TetrisKeys
    {
        Left,
        Right,
        Turn,
        QuickFall,
        InstantFall
    }

    public enum TetrisEvents
    {
        KeyDown,
        KeyUp
    }
}
