using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGameLogic.TetrisShapes
{
    public interface ITetrisShapes
    {
        void Add(ITetrisShape shape);
        IEnumerable<int> GetSquareNumbers();
        List<ITetrisShape> GetList(int cnt = 0);
    }
}
