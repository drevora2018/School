namespace AI_lab_1._2
{
    public static class Program
    {
        public static void Main()
        {
            GreedyBFS greedy = new GreedyBFS();
            greedy.GetSLD(@"C:\Users\Drevora\Desktop\AI lab 1.2\AI lab 1.2\SLD.txt");
            greedy.GroupedNode(@"C:\Users\Drevora\Desktop\AI lab 1.2\AI lab 1.2\ABDistance.txt");
            greedy.GroupedNodeAddition();
            greedy.Greedy(greedy.BFS.Find(x => x.A == "Malaga"), "Valladolid");
        }
    }
}