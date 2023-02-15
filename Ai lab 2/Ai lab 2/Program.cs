using System.Diagnostics;
namespace Ai_lab_2
{
    class Program
    {
        public static void Main()
        {
            Stopwatch sw = Stopwatch.StartNew();
            Sudoku sudoku = new Sudoku();
            sudoku.PushSudokuFromFile(@"C:\Users\Drevora\source\repos\Ai lab 2\Ai lab 2\Sudoku.txt");
            sudoku.SolveEachSudokuIndividually();
            sw.Stop();
            Console.WriteLine("Time:" + sw.ElapsedMilliseconds + "ms");
        }
    }
}
