using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGameLogic
{
    public class TetrisGame
    {
        private TetrisStartOptions _startOptions;
        public TetrisMatrix Matrix { get; private set; }

        public TetrisGame(TetrisStartOptions startOptions)
        {
            _startOptions = startOptions;
            Matrix = new TetrisMatrix(_startOptions.MatrixWidth, _startOptions.MatrixHeight, _startOptions.ShapeLibrary);
        }
    }
}
