using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DrawGraph
{
    internal class Drawer
    {
        private const int WheelConstant = 120;
        private const int ZoomMultiplier = 4;

        private readonly Pen _axesPen;
        private readonly Color _backColor;
        private readonly List<Function> _currentFunctions;
        private readonly Graph _graph;

        public Drawer(Graph graph, Pen graphPen, Pen axesPen, Pen functionPen, Color backColor)
        {
            _graph = graph;
            GraphPen = graphPen;
            FunctionPen = functionPen;
            _axesPen = axesPen;
            _backColor = backColor;
            _currentFunctions = new List<Function>();
            DrawGrid = DrawTicks = DrawAxes = true;
        }

        public Drawer(Graph graph)
            : this(graph, new Pen(Brushes.Black, 2), new Pen(Brushes.Black, 2), new Pen(Brushes.Black, 2), Color.White)
        {
        }

        public Drawer(Graph graph, Color backColor)
            : this(graph, new Pen(Brushes.Black, 2), new Pen(Brushes.Black, 2), new Pen(Brushes.Black, 2), backColor)
        {
        }

        public Drawer(Graph graph, Pen functionPen)
            : this(graph, new Pen(Brushes.Black, 2), new Pen(Brushes.Black, 2), functionPen, Color.White)
        {
        }

        public Drawer(Graph graph, Pen functionPen, Color backColor)
            : this(graph, new Pen(Brushes.Black, 2), new Pen(Brushes.Black, 2), functionPen, backColor)
        {
        }

        public Pen GraphPen { get; set; }

        public Pen FunctionPen { get; set; }

        public Graph Graph
        {
            get { return _graph; }
        }

        public bool DrawGrid { get; set; }
        public bool DrawTicks { get; set; }
        public bool DrawAxes { get; set; }

        private PointF TransformPoint(PointF point)
        {
            //the point (0,0) of the form is the upper left corner, so every graph point needs a conversion
            return new PointF(point.X*Graph.CellSize + Graph.Center.X, -point.Y*Graph.CellSize + Graph.Center.Y);
        }

        // draw a single function
        private void DrawFunction(Graphics g, Function f)
        {
            // a list of list of points is required to properly skip imaginary number(e.g sqrt(-1))     
            var pointsList = new List<List<PointF>> {new List<PointF>()};

            bool lastNan = false;
            for (float x = -Graph.NumOfCells/2; x < Graph.NumOfCells/2; x += Graph.Step)
                // from -X border to X border og the graph
            {
                float y = f.Apply(x); // the y value of x is calculated thanks to reflection
                PointF point = TransformPoint(new PointF(x, y));
                // then the point obtained is converted to "real" coordinates

                //when an imaginary number is discovered it creates a new separated list of points to draw
                //every list of points will be drawn singularly
                if (double.IsNaN(y))
                {
                    if (!lastNan) // it creates a new list of points only if the last number wasn't imaginary
                    {
                        pointsList.Add(new List<PointF>());
                        lastNan = true;
                    }
                }

                    // if a point exceeds the borders of the graph his Y components is set to not visible, to prevent overflow.
                else if (point.Y > Graph.Heigth)
                {
                    pointsList[pointsList.Count - 1].Add(new PointF(point.X, Graph.Heigth + FunctionPen.Width));
                }
                else if (point.Y < 0)
                {
                    pointsList[pointsList.Count - 1].Add(new PointF(point.X, -FunctionPen.Width));
                }
                else
                {
                    lastNan = false;
                    pointsList[pointsList.Count - 1].Add(point); //
                }
            }

            //draws each list of points only if the list has a number of elements > 1
            foreach (var list in pointsList.Where(list => list.Count > 1))
            {
                g.DrawLines(FunctionPen, list.ToArray());
            }
        }

        public void DrawGraph(Graphics g)
        {
            g.Clear(_backColor);

            if (DrawGrid || DrawTicks)
            {
                for (int i = 0; i <= Graph.NumOfCells; i++)
                {
                    if (DrawGrid)
                    {
                        //draw grid
                        g.DrawLine(Pens.Gray, i*Graph.CellSize, 0, i*Graph.CellSize, Graph.NumOfCells*Graph.CellSize);
                        g.DrawLine(Pens.Gray, 0, i*Graph.CellSize, Graph.NumOfCells*Graph.CellSize, i*Graph.CellSize);
                    }

                    if (DrawTicks)
                    {
                        //draw tick marks
                        g.DrawLine(_axesPen, Graph.Center.X - Graph.TickSize/2, i*Graph.CellSize,
                                   Graph.Center.X + Graph.TickSize/2, i*Graph.CellSize);
                        g.DrawLine(_axesPen, i*Graph.CellSize, Graph.Center.Y - Graph.TickSize/2, i*Graph.CellSize,
                                   Graph.Center.Y + Graph.TickSize/2);
                    }
                }
            }
            if (DrawAxes)
            {
                //draw axes
                g.DrawLine(_axesPen, 0, Graph.Center.Y, Graph.Width, Graph.Center.Y);
                g.DrawLine(_axesPen, Graph.Center.X, 0, Graph.Center.X, Graph.Heigth);
            }
        }

        public void DrawFunctions(Graphics g)
        {
            // draw all the current functions
            foreach (Function f in _currentFunctions)
            {
                DrawFunction(g, f);
            }
        }

        public void Zoom(int delta)
        {
            int value = (delta/WheelConstant)*ZoomMultiplier;

            if (Graph.NumOfCells - value <= Graph.MinNumOfCells)
            {
                Graph.NumOfCells = Graph.MinNumOfCells;
            }
            else if (Graph.NumOfCells - value >= Graph.MaxNumOfCells)
            {
                Graph.NumOfCells = Graph.MaxNumOfCells;
            }
            else
            {
                Graph.NumOfCells -= value;
            }
        }

        public void AddFunction(Function f)
        {
            _currentFunctions.Add(f);
        }

        public void ClearFunctionList()
        {
            if (_currentFunctions.Count > 0)
                _currentFunctions.Clear();
        }
    }
}