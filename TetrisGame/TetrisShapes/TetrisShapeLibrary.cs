﻿using System;
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

        public ITetrisShape GetNextShape()
        {
            var index = _random.Next(_items.Count);
            return _items[index];
        }
    }
}