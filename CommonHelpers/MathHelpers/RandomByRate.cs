using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelpers.MathHelpers
{
    public class RandomByRate<T>
    {
        Random _random = new Random();
        Dictionary<T, int> _percents = new Dictionary<T, int>();

        public RandomByRate(Dictionary<T, int> percents)
        {
            if (percents.Values.Sum(p => p) != 100)
            {
                throw new ArgumentException("Sum of percents shoud be 100");
            }
            _percents = percents;
        }

        public T Next()
        {
            var r = _random.Next(1, 100);
            var sum = 0;
            foreach (var key in _percents.Keys.OrderBy(k => _percents[k]))
            {
                sum += _percents[key];
                if (sum >= r)
                {
                    return key;
                }
            }
            throw new Exception("Something wrong with Next :-(");
        }
    }
}
