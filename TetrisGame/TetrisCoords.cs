using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGameLogic
{
    public class TetrisCoords
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public TetrisCoords(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public void Set(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }
}
