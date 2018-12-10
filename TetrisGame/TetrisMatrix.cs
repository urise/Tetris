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

        public TetrisMatrix(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
