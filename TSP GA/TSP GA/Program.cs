namespace TSP_GA
{
    public class Program
    {
        public static void Main()
        {
            MainProgram program = new MainProgram();
            int generations = 0;
            Individuals best = new Individuals();
            program.AddToDS(@"C:\Users\Drevora\source\repos\TSP GA\TSP GA\berlin52.txt");
            for (int i = 0; i < 52; i++)
            {
                program.individuals.Add(new Individuals());
                for (int j = 0; j < 52; j++)
                {
                    program.individuals[i].CitiesVisited[j] = j;
                }
            }
            while(generations < 100000)
            {
                var list = program.TournamentSelection(30);
                for (int i = 0; i < list.Count; i = i + 2)
                {
                    var parent1 = list[i];
                    var parent2 = list[i + 1];
                    var individualToAdd = program.Mutation(program.CrossOver(parent1, parent2));
                    program.individuals.Add(individualToAdd);
                }
                foreach (var item in program.individuals)
                {
                    if (item.Fitness > best.Fitness)
                    {
                        best = item;
                    }
                }

                Console.WriteLine($"Best Fitness: {best.Fitness}");
            }
            
            
        }
    }
}
