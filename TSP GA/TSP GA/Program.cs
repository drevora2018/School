using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace TSP_GA
{
    public class Program
    {
        public static void Main()
        {
            List<Individuals> BestIndividualPerRun = new List<Individuals>();
            int[] Variables = { 2,              //Tournament Size
                                1,              //Elitism
                                500,            //Denominator (Mutation and crossover chance), 5 means 1/5 chance, 10 means 1/10 chance
                                230,            //Generations to run for 
                                3500,           //Population size
                                10,              //Amount of tries to do
            };
            var gen = Variables[5];
            Parallel.For(0, gen, x =>
            {
                Random rand = new Random();
                MainProgram program = new MainProgram();
                int generations = 0;
                Individuals best = new Individuals();
                program.AddToDS(@"C:\Users\Drevora\Desktop\Lab31\TSP GA\TSP GA\berlin52.txt");
                for (int i = 0; i < Variables[4]; i++)
                {
                    int[] cities = new List<int>(Enumerable.Range(1, 51)).ToArray();
                    var ActualCities = new List<Cities>();
                    int n = cities.Count();
                    while (n > 1)
                    {
                        n--;
                        int k = rand.Next(n + 1);
                        int value = cities[k];
                        cities[k] = cities[n];
                        cities[n] = value;
                    }
                    int[] citiesNew = new int[cities.Length + 2];
                    int j = 0;
                    foreach (var item in cities.Append(0).Prepend(0))
                    {
                        ActualCities.Add(program.cities.Find(x => x.ID == item));
                        citiesNew[j] = item; j++;
                    }
                    program.individuals.Add(new Individuals(citiesNew, ActualCities));



                }
                foreach (var item in program.individuals)
                {
                    item.CalculcateExternalFitness();
                }



                while (generations < Variables[3])
                {
                    var list = program.TournamentSelection(Variables[0], Variables[1], Variables[2]);
                    program.individuals.Clear();
                    program.individuals.AddRange(list);

                    //print the individual with highest fitness
                    foreach (var item in program.individuals.OrderBy(x => x.Fitness).Take(1))
                    {
                        Console.WriteLine($"Best Fitness: {item.Fitness}");

                    }
                    generations++;
                }

                foreach (var item in program.individuals.OrderBy(x => x.Fitness).Take(1))
                {
                    Console.WriteLine($"Best Fitness: {item.Fitness}");
                    BestIndividualPerRun.Add(item);
                }

                program.individuals.Clear();
            });
            
            for (int i = 0; i < BestIndividualPerRun.Count; i++)
            {
                Console.WriteLine($"BestIndividual in run{i}: \nFitness: {BestIndividualPerRun[i].Fitness}\nCities (In order): \n");
                foreach (var item in BestIndividualPerRun[i].CitiesVisited)
                {
                    Console.Write(item + "-");
                }
                Console.WriteLine("\n----------------------------------------------------------");
            }

            Console.WriteLine($"Average Fitness:");
            double average = 0;
            foreach (var item in BestIndividualPerRun)
            {
                average += item.Fitness;
            }
            average = average / BestIndividualPerRun.Count;
            Console.Write(average);
        }

    }
}
