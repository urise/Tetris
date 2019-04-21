using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisGameLogic.TetrisShapes;

namespace TetrisGameLogic
{
    public class TetrisStartOptions
    {
        public int MatrixWidth { get; set; } = 10;
        public int MatrixHeight { get; set; } = 20;
        public ITetrisShapeLibrary ShapeLibrary { get; set; }
    }
}
