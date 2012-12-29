using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DrawGraph
{
    public sealed partial class Form1 : Form
    {
        private new const char Enter = (char) 13;
        private readonly Bitmap _buffer;
        private readonly Drawer _drawer;
        private readonly Graphics _formGraphics;
        private Graphics _graphics;

        public Form1()
        {
            InitializeComponent();
            CenterToScreen();
            SetStyle(
                ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            //creates a Graph object of quadratic size
            var graph = new Graph(new Size(ClientSize.Width, ClientSize.Width), 20);
            var blackPen = new Pen(Brushes.Black, 2);
            var bluePen = new Pen(Brushes.DodgerBlue, 2);
            //creates a Drawer object that will draw the graph and the functions
            _drawer = new Drawer(graph, blackPen, blackPen, bluePen, Color.White);
            //creates a BitMap used as a buffer
            _buffer = new Bitmap((int) graph.Width, (int) graph.Heigth);
            _formGraphics = CreateGraphics(); // creates the graphics object of the form
            _formGraphics.CompositingQuality = CompositingQuality.HighSpeed;

            LoadComboBox(comboBox1, GetColors());
            comboBox1.SelectedItem = comboBox1.Items[comboBox1.Items.IndexOf(Color.DodgerBlue.Name)];
        }

        private List<string> GetColors()
        {
            var colors = new List<string>();
            string[] colorNames = Enum.GetNames(typeof (KnownColor));

            foreach (string colorName in colorNames)
            {
                var knownColor = (KnownColor) Enum.Parse(typeof (KnownColor), colorName);

                if (knownColor > KnownColor.Transparent)
                {
                    colors.Add(colorName);
                }
            }
            return colors;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            UpdateScreen();
            base.OnPaint(e);
        }

        private void EnterPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != Enter) return;

            var f = new Function();
            try
            {
                f.Expression = functionBox.Text;
            }
            catch (Exception ex)
            {
                ErrorLbl.Text = ex.Message;
                return;
            }
            _drawer.AddFunction(f);
            ErrorLbl.Text = string.Empty;
            UpdateScreen();
        }

        private void DrawClick(object sender, EventArgs e)
        {
            _drawer.ClearFunctionList();
            var f = new Function();
            try
            {
                f.Expression = functionBox.Text;
            }
            catch (Exception ex)
            {
                ErrorLbl.Text = ex.Message;
                return;
            }
            _drawer.AddFunction(f);
            ErrorLbl.Text = string.Empty;
            UpdateScreen();
        }

        private void AddClick(object sender, EventArgs e)
        {
            var f = new Function();
            try
            {
                f.Expression = functionBox.Text;
            }
            catch (Exception ex)
            {
                ErrorLbl.Text = ex.Message;
                return;
            }
            _drawer.AddFunction(f);
            ErrorLbl.Text = string.Empty;
            UpdateScreen();
        }

        private void UpdateScreen()
        {
            DrawGraph();
            DrawFunctions();

            _formGraphics.DrawImageUnscaledAndClipped(_buffer, ClientRectangle);
        }

        private void DrawFunctions()
        {
            _graphics = Graphics.FromImage(_buffer);
            _graphics.SmoothingMode = SmoothingMode.AntiAlias;

            _drawer.DrawFunctions(_graphics);
        }

        private void DrawGraph()
        {
            _graphics = Graphics.FromImage(_buffer);
            _graphics.SmoothingMode = SmoothingMode.HighSpeed;

            _drawer.DrawGraph(_graphics);
        }

        private void Zoom(object sender, MouseEventArgs e)
        {
            _drawer.Zoom(e.Delta);
            UpdateScreen();
        }

        private void gridBox_CheckedChanged(object sender, EventArgs e)
        {
            _drawer.DrawGrid = gridBox.Checked;
            UpdateScreen();
        }

        private void ticksBox_CheckedChanged(object sender, EventArgs e)
        {
            _drawer.DrawTicks = ticksBox.Checked;
            UpdateScreen();
        }

        private void axesBox_CheckedChanged(object sender, EventArgs e)
        {
            _drawer.DrawAxes = axesBox.Checked;
            UpdateScreen();
        }

        private void controlCheck_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                if (control.Name != controlCheck.Name)
                    control.Visible = controlCheck.Checked;
            }
            Update(); // clean controls
            UpdateScreen();
        }

        private void colorBtn_Click(object sender, EventArgs e)
        {
            using (var colorDlg = new ColorDialog())
            {
                colorDlg.AllowFullOpen = true;
                colorDlg.AnyColor = true;
                colorDlg.SolidColorOnly = false;

                if (colorDlg.ShowDialog() == DialogResult.OK)
                {
                    _drawer.FunctionPen = new Pen(colorDlg.Color, 2);
                }
                UpdateScreen();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _drawer.FunctionPen = new Pen(Color.FromName(comboBox1.SelectedItem.ToString()), 2);
            UpdateScreen();
            functionBox.Focus(); // set the focus to the textBox to prevent scrolling
        }

        private void LoadComboBox<T>(ComboBox comboBox, IEnumerable<T> collection)
        {
            foreach (T item in collection)
            {
                comboBox1.Items.Add(item);
            }
        }
    }
}