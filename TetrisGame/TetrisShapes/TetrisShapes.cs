using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGameLogic.TetrisShapes
{
    public class TetrisShapes : ITetrisShapes
    {
        List<ITetrisShape> _list = new List<ITetrisShape>();
        Dictionary<int, List<ITetrisShape>> _dict = new Dictionary<int, List<ITetrisShape>>();

        public void Add(ITetrisShape shape)
        {
            _list.Add(shape);
            var cnt = shape.SquareCount;
            if (!_dict.ContainsKey(cnt))
            {
                _dict.Add(cnt, new List<ITetrisShape>());
            }
            _dict[cnt].Add(shape);
        }

        public IEnumerable<int> GetSquareNumbers()
        {
            return _dict.Keys;
        }

        public List<ITetrisShape> GetList(int cnt = 0)
        {
            return cnt == 0 ? _list : _dict[cnt];
        }
    }
}
