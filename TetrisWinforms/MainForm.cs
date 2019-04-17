using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
        private Bitmap _backImage; 

        public MainForm()
        {
            InitializeComponent();

            //_drawArea = new Bitmap(pictureGame.Size.Width, pictureGame.Size.Height);
            //pictureGame.Image = _drawArea;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var shapeLibrary = new TetrisShapeLibrary(@"d:\Work\Tetris\TetrisWinforms\Shapes");
            var startOptions = new TetrisStartOptions
            {
                ShapeLibrary = shapeLibrary
            };
            _game = new TetrisGame(startOptions);
            _game.Matrix.FillWithRemoveForWinCells(6);
            PrepareControls();
        }

        private void PrepareControls()
        {
            _canvas = new TetrisCanvas(pictureGame.Height - 4, _game.Matrix.Width, _game.Matrix.Height);
            
            //panelRight.Width = this.Width - _canvas.WidthPixels - 8;
            panelLeft.Width = _canvas.WidthPixels + 6;
            panelRight.Width = this.Width - panelLeft.Width - 18;
            _backImage = new Bitmap(pictureGame.Width, pictureGame.Height, PixelFormat.Format24bppRgb);
            _canvas.SetGraphics(Graphics.FromImage(_backImage));
            //var g = pictureGame.CreateGraphics();
            //_canvas.SetGraphics(pictureGame.CreateGraphics());

            //pictureGame.Width = _canvas.WidthPixels;
            Log($"panelLeft = {panelLeft.Width} : {panelLeft.Height}");
            Log($"panelRight = {panelRight.Width} : {panelRight.Height}");
            Log($"mainForm = {this.Width} : {this.Height}");
            Log($"pictureGame = {pictureGame.Width} : {pictureGame.Height}");
            Log($"canvas = {_canvas.WidthPixels} : {_canvas.HeightPixels}");
            Log($"txtLog = ({txtLog.Left} : {txtLog.Top}; {txtLog.Width} : {txtLog.Height})");
            timer1.Start();
        }

        private void DrawTetris()
        {
            _canvas.Draw(_game.Matrix);
            using (var g = pictureGame.CreateGraphics())
            {
                g.DrawImageUnscaled(_backImage, Point.Empty);
            }

            //pictureGame.Image = _backImage;
        }

        private void pictureGame_Paint(object sender, PaintEventArgs e)
        {
            DrawTetris();
            
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            DrawTetris();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DrawTetris();
        }

        private void Log(string s)
        {
            txtLog.AppendText($"{s}\n");
        }

        private void pictureGame_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _game.Matrix.Tick();
            DrawTetris();
        }

        private TetrisKeys? GetTetrisKeyByKeyCode(Keys keyCode)
        {
            switch (keyCode)
            {
                case Keys.Left:
                    return TetrisKeys.Left;
                case Keys.Right:
                    return TetrisKeys.Right;
                case Keys.Down:
                    return TetrisKeys.QuickFall;
                case Keys.Up:
                    return TetrisKeys.Turn;
                case Keys.Space:
                    return TetrisKeys.InstantFall;
                default:
                    return null;
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            var tetrisKey = GetTetrisKeyByKeyCode(e.KeyCode);
            if (tetrisKey != null)
            {
                _game.Matrix.KeyDown((TetrisKeys)tetrisKey);
            }
            e.Handled = true;
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            var tetrisKey = GetTetrisKeyByKeyCode(e.KeyCode);
            if (tetrisKey != null)
            {
                _game.Matrix.KeyUp((TetrisKeys)tetrisKey);
            }
            e.Handled = true;
        }
    }
}
