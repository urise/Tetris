﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGameLogic.TetrisShapes
{
    public interface ITetrisSingleShape
    {
        bool IsFull(int row, int col);
        int FullSize { get; }
        int SquareCount { get; }
        ITetrisSingleShape Clone();
    }
}
