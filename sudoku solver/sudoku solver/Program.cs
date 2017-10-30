using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku_solver
{
    class Program
    {      
        public static void PrintSudoku(int[,] puzzle)
        {
            Console.WriteLine("+-----+-----+-----+");

            for (int i = 1; i < 10; ++i)
            {
                for (int j = 1; j < 10; ++j)
                    Console.Write("|{0}", puzzle[i - 1, j - 1]);

                Console.WriteLine("|");
                if (i % 3 == 0) Console.WriteLine("+-----+-----+-----+");
            }
        }

        public static bool SolveSudoku(int[,] puzzle, int row, int col)
        {
            if (row < 9 && col < 9)
            {
                if (puzzle[row, col] != 0)
                {
                    if ((col + 1) < 9) return SolveSudoku(puzzle, row, col + 1);
                    else if ((row + 1) < 9) return SolveSudoku(puzzle, row + 1, 0);
                    else return true;
                }
                else
                {
                    for (int i = 0; i < 9; ++i)
                    {
                        if (IsAvailable(puzzle, row, col, i + 1))
                        {
                            puzzle[row, col] = i + 1;

                            if ((col + 1) < 9)
                            {
                                if (SolveSudoku(puzzle, row, col + 1)) return true;
                                else puzzle[row, col] = 0;
                            }
                            else if ((row + 1) < 9)
                            {
                                if (SolveSudoku(puzzle, row + 1, 0)) return true;
                                else puzzle[row, col] = 0;
                            }
                            else return true;
                        }
                    }
                }

                return false;
            }
            else return true;
        }

        private static bool IsAvailable(int[,] puzzle, int row, int col, int num)
        {
            int rowStart = (row / 3) * 3;
            int colStart = (col / 3) * 3;

            for (int i = 0; i < 9; ++i)
            {
                if (puzzle[row, i] == num) return false;
                if (puzzle[i, col] == num) return false;
                if (puzzle[rowStart + (i % 3), colStart + (i / 3)] == num) return false;
            }

            return true;
        }

        static void Main(string[] args)
        {
            string str = "607040509008530607030027008001400306080090201096201405714800960050902004060700850";
            int[] vals = new int[81];
            for (int q = 0; q < 81; q++)
            {
                string s = str[q].ToString();
                vals[q] = Int32.Parse(s);
            }
            int[,] puzzle = new int[9,9];
            //* test strings
            //470200809050069003009000201000008000010604002600700510025106007100900020000320080
            //473251869251869473869473251732518694518694732694732518325186947186947325947325186
             
            //607040509008530607030027008001400306080090201096201405714800960050902004060700850       
            //627148539148539627539627148271485396485396271396271485714853962853962714962714853

            int i = 0;

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    puzzle[row, col] = vals[i];
                    i++;
                }
            }

            if (SolveSudoku(puzzle, 0, 0))
            {
                PrintSudoku(puzzle);
            }

            string fininshedPuzzle = "";
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    fininshedPuzzle += puzzle[row, col];
                }
            }

            Console.WriteLine(fininshedPuzzle);
            Console.Read();
        }
    }
}
