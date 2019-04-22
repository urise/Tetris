using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGameLogic.TetrisShapes
{
    public class TetrisPredictionShapes : ITetrisPredictionShapes
    {
        private const int MAX_PREDICTIONS = 10;
        private ITetrisShapeLibrary _shapeLibrary;
        private List<ITetrisSingleShape> _shapes = new List<ITetrisSingleShape>();

        public TetrisPredictionShapes(ITetrisShapeLibrary shapeLibrary)
        {
            _shapeLibrary = shapeLibrary;
            for (int i = 0; i < MAX_PREDICTIONS; i++)
            {
                AddShapeFromLibrary();
            }
        }

        public void Next()
        {
            _shapes.RemoveAt(0);
            AddShapeFromLibrary();
        }

        public List<ITetrisSingleShape> GetPredictions(int count)
        {
            var cnt = Math.Min(count, MAX_PREDICTIONS);
            return _shapes.Take(cnt).ToList();
        }

        private void AddShapeFromLibrary()
        {
            _shapes.Add(_shapeLibrary.GetNextShape().GetCurrent());
        }
    }
}
