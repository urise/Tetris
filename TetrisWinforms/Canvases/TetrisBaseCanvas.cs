using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisWinforms.Canvases
{
    public abstract class TetrisBaseCanvas : ITetrisCanvas
    {
        protected Rectangle _fullRect;

        public TetrisBaseCanvas(Rectangle fullRect)
        {
            _fullRect = fullRect;
        }

    }
}
