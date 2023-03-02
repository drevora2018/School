using System.Security.Cryptography.X509Certificates;

namespace TSP_GA
{
    public class Program
    {
        public static void Main()
        {
            int[] Variables = { 2,         //Tournament Size
                                1,          //Elitism
                                10,          //Denominator (Mutation and crossover chance), 5 means 1/5 chance, 10 means 1/10 chance
                                10000,     //Generations to run for (*2 fitness calculations)
                                1000        //Population size
            };
            Random rand = new Random();
            MainProgram program = new MainProgram();
            int generations = 0;
            Individuals best = new Individuals();
            program.AddToDS(@"C:\Users\Drevora\Documents\GitHub\School\TSP GA\TSP GA\berlin52.txt");
            for (int i = 0; i < Variables[4]; i++)
            {
                int[] cities = new List<int>(Enumerable.Range(0, 52)).ToArray();
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
                foreach (var item in cities)
                {
                    ActualCities.Add(program.cities.Find(x => x.ID == item));
                }
                program.individuals.Add(new Individuals(cities, ActualCities));

            }

            
            while (generations < Variables[3])
            {
                var list = program.TournamentSelection(Variables[0], Variables[1]);
                program.individuals.Clear();
                program.individuals.AddRange(list);
                

                foreach ( var item in program.individuals )
                {
                    item.Fitness = item.CalculcateExternalFitness();
                }
                //print the individual with highest fitness
                foreach (var item in program.individuals.OrderBy(x => x.Fitness).Take(1))
                {
                    Console.WriteLine($"Best Fitness: {item.Fitness}");

                }

                generations++;
            }
            

        }
    }
}
