using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            var lines = File.ReadAllLines(@"E:\Work\Tetris\TetrisWinforms\Shapes\01.shp");
            var shapeTurns = new TetrisShapeTurns(lines);
            PrepareControls();
        }

        private void PrepareControls()
        {
            _canvas = new TetrisCanvas(pictureGame.Height - 4, _game.Matrix.Width, _game.Matrix.Height);
            //panelRight.Width = this.Width - _canvas.WidthPixels - 8;
            panelLeft.Width = _canvas.WidthPixels + 6;
            panelRight.Width = this.Width - panelLeft.Width - 18;
            _canvas.SetGraphics(pictureGame.CreateGraphics());
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
    }
}
