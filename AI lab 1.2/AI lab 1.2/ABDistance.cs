using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_lab_1._2
{
    internal class ABDistance
    {
        public string A;
        public string B;
        public double Distance;
        public PriorityQueue<ABDistance, double> GroupedNodeCollection = new PriorityQueue<ABDistance, double>();


        public ABDistance(string name1, string name2, double distance)
        {
            A = name1;
            B = name2;
            Distance = distance;
        }
    }
}
