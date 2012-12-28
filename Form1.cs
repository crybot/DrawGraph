using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawGraph
{
    public partial class Form1 : Form
    {
        private Graphics g;
        private Graph graph;
        private Drawer drawer;
        private Bitmap buffer;
        private const char enter = (char)13;

        public Form1()
        {
            InitializeComponent();
            CenterToScreen();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.DoubleBuffered = true;

            this.graph = new Graph(new Size(this.ClientSize.Width, this.ClientSize.Width), 20);
            this.drawer = new Drawer(graph, new Pen(Brushes.Black, 2), new Pen(Brushes.Black, 2), new Pen(Brushes.Black, 2), this.BackColor);
            this.buffer = new Bitmap((int)graph.Width, (int)graph.Heigth);
        }

        private void MainForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            this.UpdateScreen();
        }
        private void functionBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == enter)
            {
                Function f = new Function();
                try
                {
                    f.Expression = this.functionBox.Text;
                }
                catch (Exception ex)
                {
                    this.ErrorLbl.Text = ex.Message;
                    return;
                }
                this.drawer.AddFunction(f);
                this.ErrorLbl.Text = string.Empty;
                this.UpdateScreen();
            }
        }
        private void DrawBtn_Click(object sender, EventArgs e)
        {
            this.drawer.ClearFunctionList();
            Function f = new Function();
            try
            {
                f.Expression = this.functionBox.Text;
            }
            catch (Exception ex)
            {
                this.ErrorLbl.Text = ex.Message;
                return;
            }
            this.drawer.AddFunction(f);
            this.ErrorLbl.Text = string.Empty;
            this.UpdateScreen();
        }
        private void AddBtn_Click(object sender, EventArgs e)
        {
            Function f = new Function();
            try
            {
                f.Expression = this.functionBox.Text;
            }
            catch (Exception ex)
            {
                this.ErrorLbl.Text = ex.Message;
                return;
            }
            this.drawer.AddFunction(f);
            this.ErrorLbl.Text = string.Empty;
            this.UpdateScreen();
        }

        private void UpdateScreen()
        {
            this.DrawGraph();
            this.DrawFunctions();

            this.CreateGraphics().DrawImage(this.buffer, Point.Empty);
        }
        private void DrawFunctions()
        {
            this.g = Graphics.FromImage(this.buffer);
            this.g.SmoothingMode = SmoothingMode.AntiAlias;

            this.drawer.DrawFunctions(g);
        }

        private void DrawGraph()
        {
            this.g = Graphics.FromImage(this.buffer);
            this.g.SmoothingMode = SmoothingMode.AntiAlias;

            this.drawer.DrawGraph(g);
        }

        private void Zoom(object sender, MouseEventArgs e)
        {
            this.drawer.Zoom(e.Delta);
            this.UpdateScreen();
        }
    }
}
