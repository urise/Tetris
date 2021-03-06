﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisGameLogic.TetrisShapes;

namespace TetrisGameLogic
{
    public interface ITetrisMatrix
    {
        int Width { get; }
        int Height { get; }
        TetrisCell Cell(int row, int col);
        void RemoveFullLines();
        bool IsEmpty();
        int Score { get; }
        List<ITetrisShape> GetPredictions();
    }
}
