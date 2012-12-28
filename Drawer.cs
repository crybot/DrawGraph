using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DrawGraph
{
    class Drawer
    {
        
        private Graph graph;
        private Pen graphPen;
        private Pen axesPen;
        private Pen functionPen;
        private Color backColor;
        private List<Function> currentFunctions;

        private readonly int zoomMultiplier = 4;
        private const int wheelConstant = 120;


        public Pen FunctionPen
        {
            get { return functionPen; }
            set { functionPen = value; }
        }

        public Drawer(Graph graph, Pen graphPen, Pen axesPen, Pen functionPen, Color backColor)
        {
            this.graph = graph;
            this.graphPen = graphPen;
            this.axesPen = axesPen;
            this.functionPen = functionPen;
            this.backColor = backColor;
            this.currentFunctions = new List<Function>();
        }

        public Drawer(Graph graph)
            : this(graph, new Pen(Brushes.Black, 2), new Pen(Brushes.Black, 2), new Pen(Brushes.Black, 2), Color.White) { }

        public Drawer(Graph graph, Color backColor)
            : this(graph, new Pen(Brushes.Black, 2), new Pen(Brushes.Black, 2), new Pen(Brushes.Black, 2), backColor) { }

        public Drawer(Graph graph, Pen functionPen)
            : this(graph, new Pen(Brushes.Black, 2), new Pen(Brushes.Black, 2), functionPen, Color.White) { }

        public Drawer(Graph graph, Pen functionPen, Color backColor)
            : this(graph, new Pen(Brushes.Black, 2), new Pen(Brushes.Black, 2), functionPen, backColor) { }


        private PointF TransformPoint(PointF point)
        {
            return new PointF(point.X * graph.CellSize + graph.Center.X, -point.Y * graph.CellSize + graph.Center.Y);
        }

        private void drawFunction(Graphics g, Function f)
        {
            List<List<PointF>> pointsList = new List<List<PointF>>();
            pointsList.Add(new List<PointF>());

            int i = 0;
            bool lastNan = false;
            for (float x = -graph.NumOfCells; x < graph.NumOfCells; x += graph.Step, i++)
            {

                float y = f.Apply(x);
                PointF point = TransformPoint(new PointF(x, y));

                if (double.IsNaN(y))
                {
                    if (!lastNan)
                    {
                        pointsList.Add(new List<PointF>());
                        lastNan = true;
                    }
                }
                else if (point.Y > graph.Heigth)
                {
                    pointsList[pointsList.Count - 1].Add(new PointF(point.X, graph.Heigth + this.functionPen.Width));
                }
                else if (point.Y < 0)
                {
                    pointsList[pointsList.Count - 1].Add(new PointF(point.X, -this.functionPen.Width));
                }
                else
                {
                    lastNan = false;
                    pointsList[pointsList.Count - 1].Add(point);
                }
            }

            foreach (List<PointF> list in pointsList)
            {
                if (list.Count > 1)
                {
                    g.DrawLines(functionPen, list.ToArray());
                }
            }
        }

        public void DrawGraph(Graphics g)
        {
            g.Clear(backColor);

            for (int i = 0; i <= graph.NumOfCells; i++)
            {
                //grid
                g.DrawLine(Pens.Gray, i * graph.CellSize, 0, i * graph.CellSize, graph.NumOfCells * graph.CellSize);
                g.DrawLine(Pens.Gray, 0, i * graph.CellSize, graph.NumOfCells * graph.CellSize, i * graph.CellSize);

                //ticks
                g.DrawLine(axesPen, graph.Center.X - graph.TickSize / 2, i * graph.CellSize, graph.Center.X + graph.TickSize / 2, i * graph.CellSize);
                g.DrawLine(axesPen, i * graph.CellSize, graph.Center.Y - graph.TickSize / 2, i * graph.CellSize, graph.Center.Y + graph.TickSize / 2);
            }
            //axes
            g.DrawLine(axesPen, 0, graph.Center.Y, graph.Width, graph.Center.Y);
            g.DrawLine(axesPen, graph.Center.X, 0, graph.Center.X, graph.Heigth);
        }
        public void DrawFunctions(Graphics g)
        {
            foreach (Function f in this.currentFunctions)
            {
                this.drawFunction(g, f);
            }
        }
        public void Zoom(int delta)
        {
            int value = (delta / wheelConstant) * zoomMultiplier;

            if (graph.NumOfCells - value <= 4)
            {
                graph.NumOfCells = 4;
            }
            else if (graph.NumOfCells - value >= 100)
            {
                graph.NumOfCells = 100;
            }
            else
            {
                graph.NumOfCells -= value;
            }
        }

        public void AddFunction(Function f)
        {
            this.currentFunctions.Add(f);
        }
        public void ClearFunctionList()
        {
            this.currentFunctions.Clear();
        }
    }
}
