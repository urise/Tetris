using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisWinforms.Canvases
{
    public class TetrisCanvasOptions
    {
        public Brush BordersBrush { get; private set; } = new SolidBrush(Color.DarkBlue);
        public Brush FallingBrush { get; private set; } = new SolidBrush(Color.DarkGreen);
        public Brush StaticBrush { get; private set; } = new SolidBrush(Color.FromArgb(0xFF, 0x11, 0x11, 0x11));
        public Brush BackgroundBrush { get; private set; } = new SolidBrush(Color.FromArgb(0xFF, 0xFF, 0xBC, 0x00));
        public Brush CellsForRemoveBrush { get; private set; } = new SolidBrush(Color.Purple);
        public Brush CellSeparatorBrush { get; private set; } = new SolidBrush(Color.DarkGray);
        public Brush ScoreBackgroundBrush { get; private set; } = new SolidBrush(Color.Azure);
        public Brush ScoreBrush { get; private set; } = new SolidBrush(Color.Black);
        public Font ScoreFont { get; private set; } = new Font("Arial", 22);

        private static TetrisCanvasOptions _instance;
        public static TetrisCanvasOptions Instance
        {
            get
            {
                return _instance ?? (_instance = new TetrisCanvasOptions());
            }
        }
    }
}
