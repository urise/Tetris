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
        protected Graphics _graphics;
        protected TetrisCanvasOptions _options;

        public TetrisBaseCanvas(Graphics graphics, Rectangle fullRect)
        {
            _graphics = graphics;
            _fullRect = fullRect;
            _options = TetrisCanvasOptions.Instance;
        }

    }
}
