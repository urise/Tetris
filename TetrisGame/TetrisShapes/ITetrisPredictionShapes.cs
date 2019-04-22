using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGameLogic.TetrisShapes
{
    public interface ITetrisPredictionShapes
    {
        void Next();
        List<ITetrisSingleShape> GetPredictions(int count);
    }
}
