using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGameLogic
{
    public class TetrisShapeLibrary : ITetrisShapeLibrary
    {
        private List<TetrisShape> _items = new List<TetrisShape>();
        private Random _random = new Random();

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

        public TetrisShape GetNextShape()
        {
            var index = _random.Next(_items.Count - 1);
            return _items[index];
        }
    }
}
