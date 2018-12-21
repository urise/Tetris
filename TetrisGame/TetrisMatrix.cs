using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGameLogic
{
    public class TetrisMatrix
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Random _random = new Random();

        private TetrisCell[,] _cells;

        public TetrisMatrix(int width, int height)
        {
            Width = width;
            Height = height;
            _cells = new TetrisCell[Width, Height];
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    _cells[i, j] = new TetrisCell { State = TetrisCellState.Empty };
                }
            }
            _cells[1, 1].State = TetrisCellState.Static;
            _cells[2, 2].State = TetrisCellState.Static;
        }

        public TetrisCell Cell(int row, int column)
        {
            return _cells[row, column];
        }

        public void Tick()
        {
            var x = _random.Next(0, Width);
            var y = _random.Next(0, Height);
            _cells[x, y].State = TetrisCellState.Static;
            x = _random.Next(0, Width);
            y = _random.Next(0, Height);
            _cells[x, y].State = TetrisCellState.Empty;
        }
    }
}
