using board;

internal class ProjectEntryPoint
{
    private static void Main(string[] args)
    {
        while (true)
        {
            BoardState board;
            string? val;
            Console.Write("Enter Board: ");
            val = Console.ReadLine();
            if (val == null)
            {
                Console.WriteLine("==== Error: Reading null value");
                break;
            }
            board = new BoardState(val);
            Console.WriteLine("Original: " + board);

            var board2 = board.Copy();
            Console.WriteLine("Copy: " + board2);
        }
    }
}