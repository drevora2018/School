using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP_GA
{
    /*class NewestGA
    {

            public List<Cities> cities = new List<Cities>();
            public List<Individuals> individuals = new List<Individuals>();
            public List<Individuals> BestIndividuals = new List<Individuals>();

            public void AddToDS(string path)
            {
                string[] lines = File.ReadAllLines(path);
                foreach (string line in lines)
                {
                    string[] values = line.Split(' ');
                    cities.Add(new Cities(int.Parse(values[0]) - 1, double.Parse(values[1]), double.Parse(values[2])));
                }
            }

            /// <summary>
            /// Selects the best individual from the sample population. 
            /// </summary>
            /// <param name="TournamentSize"></param>
            public List<Individuals> TournamentSelection(int tournamentSize, int Elitism)
            {
                Random rnd = new Random();
                List<Individuals> best = new List<Individuals>();
                List<Individuals> losers = new List<Individuals>();

                while (individuals.Count >= tournamentSize + Elitism)
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

                    //pick out parents
                    Individuals bestInd = tournament[0];
                    for (int i = 1; i < tournament.Count; i++)
                    {
                        if (tournament[i].CalculcateExternalFitness() < bestInd.CalculcateExternalFitness())
                        {
                            bestInd = tournament[i];
                        }
                    }

                    best.Add(bestInd);
                }

                //mutate and crossover based on the parents
                var ReturnList = new List<Individuals>();
                for (int i = 0; i < best.Count; i++)
                {
                    if (rnd.Next(0, 10) == 1)
                    {
                        var parent1 = best[i];
                        var parent2 = best[i + 1];
                        var individualToAdd = Mutation(CrossOver(parent1, parent2));
                        ReturnList.Add(individualToAdd);
                    }
                }

                //elitism
                foreach (var item in best.OrderBy(x => x.Fitness).Take(Elitism))
                {
                    ReturnList.Add(item);
                }
                return ReturnList;
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
                var ActualCities = new List<Cities>();

                for (int i = 0; i < crossover.Length; i++)
                {
                    crossover[i] = -1;
                }
                //select a random range in crossover
                int point1 = rand.Next(0, 52);
                int point2 = rand.Next(point1 + 1, 52);

                for (int j = point1; j < point2; j++)
                {
                    crossover[j] = Parent1.CitiesVisited[j];
                }

                List<int> remainingGenes = Parent2.CitiesVisited.Except(crossover).ToList();
                for (int i = 0; i < crossover.Length; i++)
                {
                    if (crossover[i] == -1)
                    {
                        crossover[i] = remainingGenes[0];
                        remainingGenes.RemoveAt(0);
                    }
                }

                foreach (var item in crossover)
                {
                    ActualCities.Add(cities.Find(x => x.ID == item));
                }


                return new Individuals(crossover, ActualCities);
            }

            /// <summary>
            /// Takes values between two points, and reverses them.
            /// </summary>
            /// <param name="Individual"></param>
            /// <returns></returns>
            public Individuals Mutation(Individuals Individual)
            {
                Random rand = new Random();
                var ActualCities = new List<Cities>();

                int point1 = rand.Next(0, 52);
                int point2 = rand.Next(point1, 52);

                Individual.CitiesVisited[point1..point2].Reverse();
                Individual.Cities.Clear();
                foreach (var item in Individual.CitiesVisited)
                {
                    Individual.Cities.Add(cities.Find(x => x.ID == item));
                }
                Individual.CalculcateExternalFitness();
                return Individual;
            }
        }
    }*/
}
