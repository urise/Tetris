using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGameLogic.TetrisShapes
{
    public class TetrisSingleShape : ITetrisSingleShape
    {
        private bool[,] _cells;

        public int FullSize { get; private set; }
        public int RealSizeVer { get; private set; }
        public int RealSizeHor { get; private set; }
        
        public TetrisSingleShape(List<string> lines)
        {
            ValidateLines(lines);

            FullSize = lines.Count;
            _cells = new bool[FullSize, FullSize];

            for (int i = 0; i < FullSize; i++)
            {
                for (int j = 0; j < FullSize; j++)
                {
                    _cells[i, j] = lines[i][j] == 'X';
                }
            }
        }

        private void ValidateLines(List<string> lines)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Length != lines.Count)
                {
                    throw new ArgumentException($"Lines[{i}].Length is equal to {lines[i].Length}, but should be equal to {lines.Count}");
                }
            }
        }

        public bool IsFull(int row, int col)
        {
            return _cells[row, col];
        }
    }
}
