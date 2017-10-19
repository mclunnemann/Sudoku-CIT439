using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sudokuSolver
{

    public partial class Form1 : Form
    {
        public static bool easy;
        public static bool medium;
        public static bool hard;

        string puzzle, answer;
        const int cColWidth = 45;
        const int cRowHeigth = 45;
        const int cMaxCell = 9;
        const int cSidelength = cColWidth * cMaxCell + 3;
        DataGridView DGV;
        Sudoku sudoku = new Sudoku();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            setUpBoard();
        }


        private void newGame()
        {
            resetBoard();
            fillGrid();
        }

        public void setUpGrid(string grid)
        {
            foreach (var c in grid)
            {
                int num = c - '0';

            }
        }
        private void fillGrid()
        {
            puzzle = "";
            answer = "";
            sudoku.Randomize(out answer, out puzzle);
            label1.Text = puzzle;
            label2.Text = answer;
            int letter = 0;
            char[] letters = new char[puzzle.Length];
            for (int i = 0; i < letters.Length; i++)
            {
                letters[i] = puzzle[i];
            }
            foreach (DataGridViewRow i in DGV.Rows)
            {
                foreach (DataGridViewCell j in i.Cells)
                {
                    if (letters[letter] == '0')
                    {
                        letters[letter] = ' ';
                    }
                    else
                    {
                        j.Value = letters[letter];
                        j.Style.BackColor = Color.LightGray;
                        j.ReadOnly = true;
                    }
                    letter++;
                }
            }
        }
        private void setUpBoard()
        {
            DGV = new DataGridView();
            DGV.Name = "DGV";
            DGV.AllowUserToResizeColumns = false;
            DGV.AllowUserToResizeRows = false;
            DGV.AllowUserToAddRows = false;
            DGV.RowHeadersVisible = false;
            DGV.ColumnHeadersVisible = false;
            DGV.GridColor = Color.DarkBlue;
            DGV.DefaultCellStyle.BackColor = Color.AliceBlue;
            DGV.ScrollBars = ScrollBars.None;
            DGV.Size = new Size(cSidelength, cSidelength);
            DGV.Location = new Point(50, 50);
            DGV.Font = new System.Drawing.Font("Calibri", 16.2F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            DGV.ForeColor = Color.DarkBlue;
            DGV.SelectionMode = DataGridViewSelectionMode.CellSelect;
            // add 81 cells
            for (int i = 0; i < cMaxCell; i++)
            {
                DataGridViewTextBoxColumn TxCol = new DataGridViewTextBoxColumn();
                TxCol.MaxInputLength = 1;   // only one digit allowed in a cell
                DGV.Columns.Add(TxCol);
                DGV.Columns[i].Name = "Col " + (i + 1).ToString();
                DGV.Columns[i].Width = cColWidth;
                DGV.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                DataGridViewRow row = new DataGridViewRow();
                row.Height = cRowHeigth;
                DGV.Rows.Add(row);
            }
            // mark the 9 square areas consisting of 9 cells
            DGV.Columns[2].DividerWidth = 2;
            DGV.Columns[5].DividerWidth = 2;
            DGV.Rows[2].DividerHeight = 2;
            DGV.Rows[5].DividerHeight = 2;
            Controls.Add(DGV);
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newGame();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public void easyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mediumToolStripMenuItem.Checked == true)
            {
                mediumToolStripMenuItem.Checked = false;
                easy = true;
                medium = false;
                hard = false;
                easyToolStripMenuItem.Checked = !easyToolStripMenuItem.Checked;
            }
            else if (hardToolStripMenuItem.Checked == true)
            {
                hardToolStripMenuItem.Checked = false;
                easyToolStripMenuItem.Checked = !easyToolStripMenuItem.Checked;
                easy = true;
                medium = false;
                hard = false;
            }
            else
            {
                easyToolStripMenuItem.Checked = !easyToolStripMenuItem.Checked;
                easy = true;
                medium = false;
                hard = false;
            }
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (easyToolStripMenuItem.Checked == true)
            {
                easyToolStripMenuItem.Checked = false;
                mediumToolStripMenuItem.Checked = !mediumToolStripMenuItem.Checked;
                easy = false;
                medium = true;
                hard = false;
            }
            else if (hardToolStripMenuItem.Checked == true)
            {
                hardToolStripMenuItem.Checked = false;
                mediumToolStripMenuItem.Checked = !mediumToolStripMenuItem.Checked;
                easy = false;
                medium = true;
                hard = false;
            }
            else
            {
                mediumToolStripMenuItem.Checked = !mediumToolStripMenuItem.Checked;
                easy = false;
                medium = true;
                hard = false;
            }
        }

        private void hardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mediumToolStripMenuItem.Checked == true)
            {
                mediumToolStripMenuItem.Checked = false;
                hardToolStripMenuItem.Checked = !hardToolStripMenuItem.Checked;
                easy = false;
                medium = false;
                hard = true;
            }
            else if (easyToolStripMenuItem.Checked == true)
            {
                easyToolStripMenuItem.Checked = false;
                hardToolStripMenuItem.Checked = !hardToolStripMenuItem.Checked;
                easy = false;
                medium = false;
                hard = true;
            }
            else
            {
                hardToolStripMenuItem.Checked = !hardToolStripMenuItem.Checked;
                easy = false;
                medium = false;
                hard = true;
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this.inputText.Text);
        }

        private void resetBoard()
        {
            Controls.Remove(DGV);
            setUpBoard();
        }
    }
}
