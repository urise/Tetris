﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGameLogic
{
    public enum TetrisCellState
    {
        Empty,
        Full
    }

    public class TetrisCell
    {
        public TetrisCellState State { get; set; }
    }
}
