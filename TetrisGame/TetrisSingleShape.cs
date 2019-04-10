using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGameLogic
{
    public class TetrisSingleShape
    {
        private bool[,] _cells;

        public int Size { get; private set; }
        
        public TetrisSingleShape(List<string> lines)
        {
            ValidateLines(lines);

            Size = lines.Count;
            _cells = new bool[Size, Size];

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
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

        public bool IsFull(int x, int y)
        {
            return _cells[x, y];
        }
    }
}
