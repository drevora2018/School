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
        public List<Individuals> TournamentSelection(int tournamentSize, int numLosersToKeep)
        {
            Random rnd = new Random();
            List<Individuals> best = new List<Individuals>();
            List<Individuals> losers = new List<Individuals>();

            while (individuals.Count > 0)
            {
                if (individuals.Count < tournamentSize) 
                    break;
                List<Individuals> tournament = new List<Individuals>();
                for (int i = 0; i < tournamentSize; i++)
                {
                    int index = rnd.Next(0, individuals.Count);
                    tournament.Add(individuals[index]);
                    individuals.RemoveAt(index);
                }

                Individuals bestInd = tournament[0];
                for (int i = 1; i < tournament.Count; i++)
                {
                    if (tournament[i].Fitness > bestInd.Fitness)
                    {
                        bestInd = tournament[i];
                    }
                }

                best.Add(bestInd);

                // Add some of the losers back to the population
                if (tournament.Count > numLosersToKeep)
                {
                    tournament.Sort((x, y) => y.Fitness.CompareTo(x.Fitness));
                    losers.AddRange(tournament.GetRange(numLosersToKeep, tournament.Count - numLosersToKeep));
                }
            }
            individuals.AddRange(losers);
            

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
            //select a random range in crossover
            int point1 = rand.Next(0, 52);
            int point2 = rand.Next(point1 + 1, 52);
            
            for (int j = point1; j < point2; j++)
            {
                crossover[j] = Parent1.CitiesVisited[j];
            }
            
            List<int> remainingGenes = new List<int>();
            for (int i = 0; i < 52; i++)
            {
                if (!crossover.Contains(Parent2.CitiesVisited[i]))
                {
                    remainingGenes.Add(Parent2.CitiesVisited[i]);
                }
            }

            for (int i = 0; i < crossover.Length; i++)
            {
                if (crossover[i] == 0)
                {
                    crossover[i] = remainingGenes[i];
                }
            }

            return new Individuals(crossover, cities);
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
