﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGameLogic
{
    public class TetrisShapeTurns
    {
        private TetrisShape[] _shapes = new TetrisShape[TetrisConstants.TURNS_COUNT];

        public TetrisShapeTurns()
        {

        }

        public TetrisShapeTurns(IEnumerable<TetrisShape> shapes)
        {

        }

        public TetrisShapeTurns(IEnumerable<string> lines)
        {
            Init(lines);
        }

        public void Init(IEnumerable<string> lines)
        {
            var oneShapeLines = new List<string>();
            int size = 0;
            int currentIndex = 0;

            foreach (var line  in lines.Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s.Trim()))
            {
                if (size == 0) size = line.Length;
                if (line.Length != size)
                {
                    throw new ArgumentException($"TetrisShapeTurns.Init: wrong line size {line}");
                }
                oneShapeLines.Add(line);
                if (oneShapeLines.Count == size)
                {
                    _shapes[currentIndex++] = new TetrisShape(oneShapeLines);
                    oneShapeLines.Clear();
                }
            }
        }
    }
}
