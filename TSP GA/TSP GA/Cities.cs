using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP_GA
{
    internal class Cities
    {
        public int ID;
        public double X;
        public double Y;

        public Cities(int id, double x, double y)
        {
            ID = id;
            X = x;
            Y = y;
        }

        public double Distance(Cities other)
        {
            return Math.Sqrt(Math.Pow((other.X - this.X), 2) + Math.Pow((other.Y - this.Y), 2));
        }
    }
}
