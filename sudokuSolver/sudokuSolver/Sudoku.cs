using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudokuSolver
{
    class Sudoku
    {
        static int[,] grid2 = new int[9, 9];
        static string s;

        public Sudoku()
        {
            int[,] grid2 = new int[9, 9];
        }

        void Init(ref int[,] grid2)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    grid2[i, j] = (i * 3 + i / 3 + j) % 9 + 1;
                }
            }
        }

        public void DrawGrid(string draw)
        {
            string grid = "";
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    grid += draw.Substring(x + y, 1).ToString() + " ";
                }
                grid += "\n";
            }
        }

        public void Solve_DrawGrid(string draw)
        {
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
            string grid = "";
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    grid += draw.Substring(x + y, 1).ToString() + " ";
                }
                grid += "\n";
            }
            Console.WriteLine(grid);
        }

        //used for randomize method
        public string Draw(ref int[,] grid2, out string _s)
        {
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    s += grid2[x, y].ToString();
                }
            }
            _s = s;
            return s;
        }

        public void ChageTwoCell(ref int[,] grid2, int findValue1, int findValue2)
        {
            int xParam1, yParam1, xParam2, yParam2;
            xParam1 = yParam1 = xParam2 = yParam2 = 0;
            for (int i = 0; i < 9; i += 3)
            {
                for (int k = 0; k < 9; k += 3)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        for (int z = 0; z < 3; z++)
                        {
                            if (grid2[i + j, k + z] == findValue1)
                            {
                                xParam1 = i + j;
                                yParam1 = k + z;
                            }
                            if (grid2[i + j, k + z] == findValue2)
                            {
                                xParam2 = i + j;
                                yParam2 = k + z;
                            }
                        }
                    }
                    grid2[xParam1, yParam1] = findValue2;
                    grid2[xParam2, yParam2] = findValue1;
                }
            }
        }

        public void Update(ref int[,] grid2, int shuffleLevel)
        {
            for (int repeat = 0; repeat < shuffleLevel; repeat++)
            {
                Random rand = new Random(Guid.NewGuid().GetHashCode());
                Random rand2 = new Random(Guid.NewGuid().GetHashCode());
                ChageTwoCell(ref grid2, rand.Next(1, 9), rand2.Next(1, 9));
            }
        }

        public string Randomize(out string answer, out string puzzle)
        {
            Init(ref grid2);
            Update(ref grid2, 10);
            s = "";
            s = Draw(ref grid2, out answer);
            // above is getting the string  
            //below is randomly adding 0's to the string for the user to solve 
            /*
             * easy is 29
             * meduim is 39
             * hard is 49
             * 
             * */
            int numReplace = 0;
            if (Form1.easy == true)
            {
                numReplace = 29;
            }
            else if (Form1.medium == true)
            {
                numReplace = 39;
            }
            else if (Form1.hard == true)
            {
                numReplace = 49;
            }
            else
            {
                numReplace = 0;
            }
            int replacedAmt = 0;
            while (replacedAmt != numReplace)
            {
                Random rand = new Random(DateTime.Now.Millisecond);
                int replaceInt = rand.Next(1, 81);
                if (s.Substring(replaceInt, 1) != "0")
                {
                    s = s.Insert(replaceInt + 1, "0");
                    s = s.Remove(replaceInt, 1);
                    replacedAmt++;
                }
            }
            return puzzle = s;
        }

        //solver
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

        public string solver(string str)
        {
            try {
                int[] vals = new int[81];
                for (int q = 0; q < 81; q++)
                {
                    string s = str[q].ToString();
                    vals[q] = Int32.Parse(s);
                }
                int[,] puzzle = new int[9, 9];
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

                string fininshedPuzzle = "";

                if (SolveSudoku(puzzle, 0, 0))
                {
                    for (int row = 0; row < 9; row++)
                    {
                        for (int col = 0; col < 9; col++)
                        {
                            fininshedPuzzle += puzzle[row, col];
                        }
                    }
                    return fininshedPuzzle;
                }
                else
                {
                    return "False";
                }
            }catch(Exception e)
            {
                return "False";
            }
        }
    }
}
