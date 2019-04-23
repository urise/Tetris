using CommonHelpers.MathHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGameLogic.TetrisShapes
{
    public class TetrisShapeLibrary : ITetrisShapeLibrary
    {
        private ITetrisShapes _shapes = new TetrisShapes();
        private Random _random = new Random();
        private List<ITetrisShape> _predictions = new List<ITetrisShape>();
        private RandomByRate<int> _randomByRate;

        private int _index = 0;

        public TetrisShapeLibrary(string directory)
        {
            Load(directory);
            var rates = new Dictionary<int, int>
            {
                { 4, 85},
                { 5, 15 }
            };
            _randomByRate = new RandomByRate<int>(rates);
        }

        public void Load(string directory)
        {
            var files = Directory.GetFiles(directory, "*.shp");
            foreach (var file in files)
            {
                var lines = File.ReadAllLines(Path.Combine(directory, file));
                var shape = new TetrisShape(lines);
                _shapes.Add(shape);
            }
        }

        public ITetrisShape GetNextShape(bool fromPrediction = true)
        {
            if (fromPrediction && _predictions.Count > 0)
            {
                var result = _predictions[0];
                _predictions.RemoveAt(0);
                return result;
            }
            else
            {
                var squareCount = _randomByRate.Next();
                var list = _shapes.GetList(squareCount);
                var index = _random.Next(list.Count);
                return list[index];
            }
        }

        public List<ITetrisShape> GetPredictions(int count)
        {
            for (int i = 0; i < count - _predictions.Count; i++)
            {
                _predictions.Add(GetNextShape(false));
            }
            return _predictions.Take(count).ToList();
        }
    }
}
