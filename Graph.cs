using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DrawGraph
{
    class Graph
    {
        private readonly PointF center;
        private readonly Size size;
        private readonly float width;
        private readonly float heigth;
        private int numOfCells;
        private float step;

        public PointF Center
        {
            get { return center; }
        }
        public Size Size
        {
            get { return size; }
        }
        public int NumOfCells
        {
            get { return numOfCells; }
            set { numOfCells = value; }
        }
        public float CellSize
        {
            get { return width / numOfCells; }
        }
        public float Width
        {
            get { return width; }
        }
        public float Heigth
        {
            get { return heigth; }
        }
        public float TickSize
        {
            get { return CellSize/3; }
        }
        public float Step
        {
            get { return step; }
            set { step = value; }
        }

        public Graph(Size size, int numOfCells)
        {
            this.size = size;
            this.numOfCells = numOfCells;
            this.width = size.Width;
            this.heigth = size.Height;
            this.center = new PointF(size.Width / 2, size.Height / 2);
            this.step = 0.01f;            
        }
    }
}
