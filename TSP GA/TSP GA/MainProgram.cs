using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP_GA
{
    class MainProgram
    {
        public List<Cities> cities = new List<Cities>();
        public List<Individuals> individuals= new List<Individuals>();
        public List<Individuals> BestIndividuals = new List<Individuals>();

        public void AddToDS(string path)
        {
            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines)
            {
                string[] values = line.Split(' ');
                cities.Add(new Cities(int.Parse(values[0])-1, double.Parse(values[1]), double.Parse(values[2])));
            }
        }

        /// <summary>
        /// Selects the best individual from the sample population. 
        /// </summary>
        /// <param name="TournamentSize"></param>
        public List<Individuals> TournamentSelection(int TournamentSize)
        {
            Random rnd = new Random();
            List<Individuals> tourneyList = new List<Individuals>();
            Individuals bestInd = new Individuals();
            List<Individuals> best = new List<Individuals>();
            List<Individuals> losers = new List<Individuals>();
            while(individuals.Count != 0)
            {
                for (int i = 0; i < TournamentSize; i++)
                {
                    int index = rnd.Next(0, individuals.Count);
                    var individual = individuals[index];
                    individuals.RemoveAt(index);
                    tourneyList.Add(individual);
                }
                for (int i = 0; i < TournamentSize; i++)
                {
                    if (tourneyList[i].Fitness > bestInd.Fitness)
                    {
                        bestInd = tourneyList[i];
                    }
                    else
                    {
                        losers.Add(tourneyList[i]);
                    }
                }
                best.Add(bestInd);
                individuals.AddRange(losers);

            }
            return best;
        }

        /// <summary>
        /// Selects two random parents from the population, and crossovers them.
        /// </summary>
        /// <param name="Parent1"></param>
        /// <param name="Parent2"></param>
        /// <returns>A crossed over int[]</returns>
        public Individuals CrossOver(Individuals Parent1, Individuals Parent2)
        {
            Random rand = new Random();
            int[] crossover = new int[52];
            int point1 = rand.Next(0, 52);
            int point2 = rand.Next(point1, 52);

            Parent2.CitiesVisited.CopyTo(crossover, 0);
            for (int j = point1; j < point2; j++)
            {
                crossover[j] = Parent1.CitiesVisited[j];
            }
            
            var returnvalue = new Individuals();
            returnvalue.CitiesVisited = crossover;
            return returnvalue;
        }

        /// <summary>
        /// Takes values between two points, and reverses them.
        /// </summary>
        /// <param name="Individual"></param>
        /// <returns></returns>
        public Individuals Mutation(Individuals Individual)
        {
            Random rand = new Random();
            
            int point1 = rand.Next(0, 52);
            int point2 = rand.Next(point1, 52);

            Individual.CitiesVisited[point1..point2].Reverse();

            return Individual;
        }
    }
}
