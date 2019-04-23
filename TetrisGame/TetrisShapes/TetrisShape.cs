using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGameLogic.TetrisShapes
{
    public class TetrisShape : ITetrisShape
    {
        private ITetrisSingleShape[] _shapes = new TetrisSingleShape[TetrisConstants.TURNS_COUNT];
        private int _currentIndex = 0;

        public int SquareCount => _shapes[0].SquareCount;

        public TetrisShape()
        {

        }

        public TetrisShape(IEnumerable<string> lines)
        {
            Init(lines);
        }

        private void Init(IEnumerable<string> lines)
        {
            var oneShapeLines = new List<string>();
            int size = 0;
            int currentIndex = 0;

            foreach (var line  in lines.Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s.Trim()))
            {
                if (size == 0) size = line.Length;
                if (line.Length != size)
                {
                    throw new ArgumentException($"TetrisShape.Init: wrong line size {line}");
                }
                oneShapeLines.Add(line);
                if (oneShapeLines.Count == size)
                {
                    _shapes[currentIndex++] = new TetrisSingleShape(oneShapeLines);
                    oneShapeLines.Clear();
                }
            }
        }

        public ITetrisSingleShape SwitchToNext()
        {
            _currentIndex = (_currentIndex + 1) % _shapes.Length;
            return _shapes[_currentIndex];
        }

        public ITetrisSingleShape GetCurrent()
        {
            return _shapes[_currentIndex];
        }

        public ITetrisSingleShape GetNext()
        {
            return _shapes[(_currentIndex + 1) % _shapes.Length];
        }
    }
}
