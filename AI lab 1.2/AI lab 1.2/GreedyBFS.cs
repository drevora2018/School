using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AI_lab_1._2
{
    internal class GreedyBFS
    {
        //Greedypath is a list of all places one can go to from A to B
        public List<ABDistance> BFS = new List<ABDistance>();
        PriorityQueue<ABDistance, double> GreedyPath = new PriorityQueue<ABDistance, double>();
        //This is a list of all straightlines from A to goal
        List<KeyValuePair<string, double>> _distances = new List<KeyValuePair<string, double>>();
        public List<ABDistance> VisitedAandB = new List<ABDistance>();
        public void GetSLD(string path)
        {
            var sr = new StreamReader(path);
            while(!sr.EndOfStream)
            {
                var line = sr.ReadLine().Split(' ');
                _distances.Add(new KeyValuePair<string, double>(line[0], double.Parse(line[1])));
            }
        }

        public void GroupedNode(string path)
        {
            var sr = new StreamReader(path);
            while(!sr.EndOfStream)
            {
                var line = sr.ReadLine().Split(' ');
                BFS.Add(new ABDistance(line[0], line[1], Int32.Parse(line[2])));
                BFS.Add(new ABDistance(line[1], line[0], Int32.Parse(line[2])));
                GreedyPath.Enqueue(new ABDistance(line[0], line[1], Int32.Parse(line[2])), _distances.Find(x => x.Key == line[0]).Value);
            }
        }
        public void GroupedNodeAddition()
        {
            foreach (var item in BFS)
            {
                foreach (var items in BFS.FindAll(x => x.A == item.A))
                {
                    item.GroupedNodeCollection.Enqueue(items, items.Distance);
                }
            }
        }

        public void Greedy(ABDistance start, string Goal) 
        {
            
            if (start.A == Goal)
            {
                return;
            }
            VisitedAandB.Add(start);
            Console.WriteLine(start.A);
            PriorityQueue<ABDistance, double> nodesToVisit = new PriorityQueue<ABDistance, double>();
            nodesToVisit = start.GroupedNodeCollection;
            while (nodesToVisit.Count > 0)
            {
                ABDistance nextNode = nodesToVisit.Dequeue();
                if (!VisitedAandB.Contains(nextNode))
                {
                    VisitedAandB.Add(nextNode);
                    Console.WriteLine(nextNode.A);
                    Greedy(BFS.Find(x => x.A == nextNode.B), Goal);
                }
            }
        }
    }
}
