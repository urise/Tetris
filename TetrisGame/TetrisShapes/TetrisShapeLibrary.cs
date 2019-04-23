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
        private List<ITetrisShape> _items = new List<ITetrisShape>();
        private Random _random = new Random();
        private List<ITetrisShape> _predictions = new List<ITetrisShape>();

        public TetrisShapeLibrary(string directory)
        {
            Load(directory);
        }

        public void Load(string directory)
        {
            var files = Directory.GetFiles(directory, "*.shp");
            foreach (var file in files)
            {
                var lines = File.ReadAllLines(Path.Combine(directory, file));
                var shape = new TetrisShape(lines);
                _items.Add(shape);
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
                var index = _random.Next(_items.Count);
                return _items[index];
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
