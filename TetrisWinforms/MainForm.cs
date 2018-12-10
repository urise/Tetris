using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TetrisGameLogic;

namespace TetrisWinforms
{
    public partial class MainForm : Form
    {
        private TetrisGame _game;
        private TetrisCanvas _canvas;
        private Bitmap _drawArea;

        public MainForm()
        {
            InitializeComponent();

            //_drawArea = new Bitmap(pictureGame.Size.Width, pictureGame.Size.Height);
            //pictureGame.Image = _drawArea;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var startOptions = new TetrisStartOptions();
            _game = new TetrisGame(startOptions);
            PrepareControls();
        }

        private void PrepareControls()
        {
            _canvas = new TetrisCanvas(pictureGame.Height, _game.Matrix.Width, _game.Matrix.Height);
            panelRight.Width = this.Width - _canvas.WidthPixels;

        }

        private void pictureGame_Paint(object sender, PaintEventArgs e)
        {
            var g = pictureGame.CreateGraphics();
            _canvas.Draw(g);
            g.Dispose();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            
        }
    }
}
