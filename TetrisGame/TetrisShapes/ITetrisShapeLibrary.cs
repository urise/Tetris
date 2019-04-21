using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGameLogic.TetrisShapes
{
    public interface ITetrisShapeLibrary
    {
        ITetrisShape GetNextShape();
    }
}
