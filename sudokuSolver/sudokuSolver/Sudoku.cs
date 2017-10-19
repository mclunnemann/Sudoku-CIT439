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
            //string s;
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
            //Console.WriteLine(grid);
        }

        public void Solve_DrawGrid(string draw)
        {
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
            //Console.WriteLine(draw + "\n");
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
                //s += "\n";
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
            // Draw(ref grid2, out output);
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
            //Console.Write(outputStr);
            return puzzle = s;
        }


        //for printing random board

        //static void Main(string[] args) {
        //	s = "";
        //	string output;
        //	//Init(ref grid2);
        //	//Update(ref grid2, 10);
        //	//Draw(ref grid2, out output);
        //	//output = Randomize(ref grid2, s);
        //	//Console.Write(s);
        //	// Console.Write("\n" + output + "\n");
        //	//DrawGrid(output);
        //	//Solve_DrawGrid(output);
        //	//Solve_DrawGrid(s);
        //	Console.ReadKey();
        //}
    }
}
