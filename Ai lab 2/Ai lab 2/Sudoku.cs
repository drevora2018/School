using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ai_lab_2
{
    /*
     * Sudoku using BackTracking Algorithm
     */
    internal class Sudoku
    {
        //list of 2D arrays that store the grids
        List<int[,]> Sudokus = new List<int[,]>();
        //temporary 2D array that store the sudoku we are working with right now
        int[,] SudokuArray = new int[9,9];

        /// <summary>
        /// Pushes a sudoku from file to an array.
        /// </summary>
        /// <param name="path"></param>
        public void PushSudokuFromFile(string path)
        {
            var sr = new StreamReader(path);
            var stringinput = new string[9];
            while(!sr.EndOfStream)
            {
                for (int i = 0; i < stringinput.Length; i++)
                {
                    var line = sr.ReadLine();
                    stringinput[i] = line;
                }
                sr.ReadLine();
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        SudokuArray[i, j] = int.Parse(stringinput[i][j].ToString());
                    }
                }
                Sudokus.Add(SudokuArray);
                SudokuArray = new int[9,9];
            }

            
        }

        /// <summary>
        /// Checks if any given solution is valid
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <param name="num"></param>
        /// <returns>True if solution is valid, False otherwise</returns>
        public bool IsSolutionValid(int col, int row, int num)
        {
            //iterates through row, and see if any equal numbers are in a row
            for (int i = 0; i < 9; i++)
            {
                if (SudokuArray[row, i] == num)
                {
                    return false;
                }
            }
            //same as above, but columns
            for (int i = 0; i < 9; i++)
            {
                if (SudokuArray[i, col] == num)
                {
                    return false;
                }
            }
            //assign subgrid's column and row
            int SubGridCol = col - col % 3;
            int SubGridRow = row - row % 3;
            //checks the subgrid to see if a solution is valid.
            for (int i = SubGridRow; i < SubGridRow + 3; i++)
            {
                for (int j = SubGridCol; j < SubGridCol + 3; j++)
                {
                    if (SudokuArray[i, j] == num)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Takes a list of sudokus and solves them individually.
        /// </summary>
        public void SolveEachSudokuIndividually()
        {
            int i = 1;
            foreach (var item in Sudokus)
            {
                SudokuArray = new int[9, 9];
                SudokuArray = item;
                Console.WriteLine($"\nSudoku {i} Solution: ");
                SolveSudoku();
                i++;
            }
        }

        /// <summary>
        /// Uses backtracking to find the solution to a sudoku. Tests all individual numbers (0-9) on each cell and test if that solution is viable.
        /// If not, will backtrack to change numbers on previous slots.
        /// </summary>
        /// <returns>True if sudoku is solved, false if backtrack</returns>
        public bool SolveSudoku()
        {
            //First two for loops: check row and column and look for 0's
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (SudokuArray[row, col] == 0)
                    {
                        //if there's a zero there, we try all numbers from 1 to 9. If it is a valid number, we assign that number to that column,
                        //and recursively check if we haven't messed up along the way
                        for (int i = 0; i < 10; i++)
                        {
                            if (IsSolutionValid(col, row, i))
                            {
                                SudokuArray[row, col] = i;
                                if (SolveSudoku())
                                {
                                    //if we didn't, then we're all good.
                                    return true;
                                }
                                //otherwise, put it back to 0 (this, and the if statement above makes the backtracking part)
                                SudokuArray[row, col] = 0;
                            }
                        }
                        //if we messed up along the way, we go back.
                        return false;
                    }
                }
                
            }
            //print out the final solution
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(SudokuArray[i, j]);
                }
                Console.WriteLine();
            }
            return true;
        }
    }
}
