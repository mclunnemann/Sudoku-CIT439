using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku_solver
{
    class Program
    {
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
                string str = Randomize(ref grid2, s).ToString();                
                //Console.Write(str);
                for (int i = 0; i < 81; i++)
                {
                    string q = str[i].ToString();
                    vals[i] = Int32.Parse(q);
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
                for (int column = 1; column <= 9; column++)
                {
                    for (int row = 1; row <= 9; row++)
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
                    if (possibleValuesFound.Count == 1)
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
        static int[,] grid2 = new int[9, 9];
        static string s;
        static void Init(ref int[,] grid2)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    grid2[i, j] = (i * 3 + i / 3 + j) % 9 + 1;
                }
            }
        }

        static string Draw(ref int[,] grid2, out string _s)
        {
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    s += grid2[x, y].ToString();
                }
            }
            //s += "\n";
            //Console.WriteLine(s);
            _s = s;
            return s;
            s = "";
        }

        static void ChageTwoCell(ref int[,] grid2, int findValue1, int findValue2)
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

        static void Update(ref int[,] grid2, int shuffleLevel)
        {
            for (int repeat = 0; repeat < shuffleLevel; repeat++)
            {
                Random rand = new Random(Guid.NewGuid().GetHashCode());
                Random rand2 = new Random(Guid.NewGuid().GetHashCode());
                ChageTwoCell(ref grid2, rand.Next(1, 9), rand2.Next(1, 9));
            }
        }
        
        static string Randomize(ref int[,] grid2, string s)
        {
            s = "";            
            Init(ref grid2);
            Update(ref grid2, 10);
            s = Draw(ref grid2, out s);
            string outputStr = s;
            //string outputStr = Draw(ref grid2, out s);
            // above is getting the string  
            //below is randomly adding 0's to the string for the user to solve 

            int numReplace = 49;
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
            
            Console.Write(outputStr);
            return s;
        }
        
    }
}
