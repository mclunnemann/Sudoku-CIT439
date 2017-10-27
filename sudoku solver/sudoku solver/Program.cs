using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku_solver
{
    class Program
    {   /*    
        private static SudokuGrid grid = new SudokuGrid();
        static void Main(string[] args)
        {
            grid.PopulateGrid();
            Console.WriteLine(grid.ToString());
            Console.WriteLine("Hit Enter to Begin Solving!!");
            Console.Read();
            // Start Solving.
            grid.Solve();
            Console.WriteLine("Done !!!");
            Console.ReadLine();
            Console.ReadLine();
        }

        public class SudokuGrid
        {
            private int[] vals = new int[81];
            public int this[int row, int column]
            {
                get { return vals[FindIndex(row, column)]; }
                set
                {
                    vals[FindIndex(row, column)] = value;
                }
            }
            public void PopulateGrid()
            {
                
                //string str = "001020600900305001001806400008102900700000008006708200002609500800203009005010300";
                string str = "470200809050069003009000201000008000010604002600700510025106007100900020000320080";

                //* test strings
                //470200809050069003009000201000008000010604002600700510025106007100900020000320080
                //473251869251869473869473251732518694518694732694732518325186947186947325947325186 
                 

                for (int i = 0; i < 81; i++)
                {
                    string s = str[i].ToString();
                    vals[i] = Int32.Parse(s);
                }
            }
            // Notice i changed row to column 
            // this aligns the grid correctly 
            private static int FindIndex(int row, int column)
            {
                return (((row - 1) * 9) + column - 1);
            }

            public override string ToString()
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.AppendLine();
                for (int row = 1; row <= 9; row++)
                {
                    for (int column = 1; column <= 9; column++)
                    {
                        sb.Append(this[row, column] + " ");
                    }
                    sb.AppendLine();
                }
                return sb.ToString();
            }

            public bool Solve()
            {
                for (int row = 1; row <= 9; row++)
                {
                    for (int column = 1; column <= 9; column++)
                    {
                        if (TrySolving(row, column))
                        {
                            Console.Clear();
                            Console.WriteLine(this.ToString());
                            System.Threading.Thread.Sleep(200);
                            // restart 
                            return Solve();
                        }
                    }
                }
                return true;
            }
            private bool TrySolving(int row, int column)
            {
                List<RowColumnValue> possibleValuesFound = new List<RowColumnValue>();
                if (this[row, column] == 0)
                {
                    for (int possiblevalues = 1; possiblevalues <= 9; possiblevalues++)
                    {
                        if (!DoesRowContainValue(possiblevalues, row, column))
                        {
                            if (!DoesColumnContainValue(possiblevalues, row, column))
                            {
                                if (!DoesSquareContainValue(possiblevalues, row, column))
                                {
                                    possibleValuesFound.Add(new RowColumnValue(row, column, possiblevalues));
                                }
                            }
                        }
                    }
                    if (possibleValuesFound.Count >= 1)
                    {                       
                        this[possibleValuesFound[0].Row, possibleValuesFound[0].Column] = possibleValuesFound[0].Value;
                        return true;
                    }                    
                }
                return false;
            }

            private bool DoesRowContainValue(int value, int row, int column)
            {
                for (int columnindex = 1; columnindex <= 9; columnindex++)
                {
                    if ((this[row, columnindex] == value) & column != columnindex)
                    {
                        return true;
                    }
                }
                return false;
            }

            private bool DoesColumnContainValue(int value, int row, int column)
            {
                for (int rowindex = 1; rowindex <= 9; rowindex++)
                {
                    if ((this[rowindex, column] == value) & row != rowindex)
                    {
                        return true;
                    }
                }
                return false;
            }

            private bool DoesSquareContainValue(int value, int row, int column)
            {
                //identify square
                int rowStart = ((row - 1) / 3) + 1;
                int columnStart = ((column - 1) / 3) + 1;
                int rowIndexEnd = rowStart * 3;
                if (rowIndexEnd == 0) rowIndexEnd = 3;
                int rowIndexStart = rowIndexEnd - 2;
                int columnIndexEnd = columnStart * 3;
                if (columnIndexEnd == 0) columnIndexEnd = 3;
                int columnIndexStart = columnIndexEnd - 2;

                for (int rowIndex = rowIndexStart; rowIndex <= rowIndexEnd; rowIndex++)
                {
                    for (int columnIndex = columnIndexStart; columnIndex <= columnIndexEnd; columnIndex++)
                    {
                        if ((this[rowIndex, columnIndex] == value) & (columnIndex != column) & (rowIndex != row))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        internal class RowColumnValue
        {
            private int row;
            internal int Row
            {
                get { return row; }
            }
            private int column;
            internal int Column
            {
                get { return column; }
            }
            private int value;
            internal int Value
            {
                get { return value; }
            }
            internal RowColumnValue(int r, int c, int v)
            {
                row = r;
                column = c;
                value = v;
            }
        }
        */
        public int[,] puzzle;
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
            string str = "470200809050069003009000201000008000010604002600700510025106007100900020000320080";
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
