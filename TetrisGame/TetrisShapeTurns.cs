using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGameLogic
{
    public class TetrisShape
    {
        private TetrisSingleShape[] _shapes = new TetrisSingleShape[TetrisConstants.TURNS_COUNT];

        public TetrisShape()
        {

        }

        public TetrisShape(IEnumerable<TetrisSingleShape> shapes)
        {

        }

        public TetrisShape(IEnumerable<string> lines)
        {
            Init(lines);
        }

        public void Init(IEnumerable<string> lines)
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

        public TetrisSingleShape Next()
        {
            return _shapes[0];
        }

        public TetrisSingleShape Current()
        {
            return _shapes[0];
        }
    }
}
