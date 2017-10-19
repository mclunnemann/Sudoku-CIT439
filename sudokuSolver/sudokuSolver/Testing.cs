namespace sudokuSolver
{
    using NUnit.Framework;

    [TestFixture]
    public class SudokuTest
    {
        [Test]
        public void DrawGrid()
        {
            Sudoku source = new Sudoku();
            string puzzle = "";
            string answer = "";
            source.Randomize(out answer, out puzzle);

            Assert.AreEqual("",answer);
            Assert.AreEqual(150, 150);
        }
    }
}