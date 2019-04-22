using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelpers.MathHelpers
{
    public class IntMinMaxFinder
    {
        public int Min { get; private set; }
        public int Max { get; private set; }
        public int Count { get; private set; }
        public int Distance => Max - Min + 1;

        public void Process(int value)
        {
            if (Count == 0)
            {
                Min = value;
                Max = value;
            }
            else
            {
                if (value < Min) Min = value;
                if (value > Max) Max = value;
            }
        }
    }
}
