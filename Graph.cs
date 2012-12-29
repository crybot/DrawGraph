using System.Drawing;

namespace DrawGraph
{
    internal class Graph
    {
        public const int MaxNumOfCells = 80;
        public const int MinNumOfCells = 4;
        private readonly PointF _center;
        private readonly float _height;
        private readonly Size _size;
        private readonly float _width;
        private int _numOfCells;

        public Graph(Size size, int numOfCells)
        {
            _size = size;
            _numOfCells = numOfCells;
            _width = size.Width;
            _height = size.Height;
            _center = new PointF((float) size.Width/2, (float) size.Height/2);
            Step = 0.01F;
        }

        public PointF Center
        {
            get { return _center; }
        }

        public Size Size
        {
            get { return _size; }
        }

        public int NumOfCells
        {
            get { return _numOfCells; }
            set { _numOfCells = value; }
        }

        public float CellSize
        {
            get { return _width/_numOfCells; }
        }

        public float Width
        {
            get { return _width; }
        }

        public float Heigth
        {
            get { return _height; }
        }

        public float TickSize
        {
            get { return CellSize/3; }
        }

        public float Step { get; set; }
    }
}