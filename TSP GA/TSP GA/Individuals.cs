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
        public int[] CitiesVisited = new int[53];
        public List<Cities> Cities = new List<Cities>();
        Program pro = new Program();
        
        public Individuals(int[] genes, List<Cities> cities)
        {
            Cities = cities;
            CitiesVisited = genes;
            //CalculcateFitness();
        }
        public Individuals()
        {
            //CalculcateFitness();
        }
        
        

        public double CalculcateExternalFitness()
        {
            var fit = 0.0;
            for (int i = 0; i < CitiesVisited.Length-1; i = i + 1)
            {
                var other = Cities[i];
                var me = Cities[i + 1];
                if (other != null || me != null) { fit += Math.Sqrt(Math.Pow((other.X - me.X), 2) + Math.Pow((other.Y - me.Y), 2)); }
                else break;

            }
            Fitness = fit;
            return fit;
        }

    }
}
