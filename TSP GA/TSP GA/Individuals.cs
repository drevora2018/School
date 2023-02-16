using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP_GA
{
    internal class Individuals
    {
        public double Fitness = 0;
        public int[] CitiesVisited = new int[52];
        MainProgram main = new MainProgram();
        
        
        public void CalculcateFitness()
        {
            for (int i = 0; i < CitiesVisited.Length; i = i + 2)
            {
                var other = main.cities.Find(x => x.ID == CitiesVisited[i]); 
                var me = main.cities.Find(x => x.ID == CitiesVisited[i + 1]);
                Fitness += Math.Sqrt(Math.Pow((other.X - me.X), 2) + Math.Pow((other.Y - me.Y), 2));
            }
        }

    }
}
