using board;

internal class Program
{
    private static void Main(string[] args)
    {
        while (true)
        {
            var Board = new Board();
            string? val;
            Console.Write("Enter Board: ");
            val = Console.ReadLine();
            if (val == null)
            {
                Console.WriteLine("==== Error: Reading null value");
                break;
            }
            Board.Set(val);
            Console.WriteLine(Board.ToString());
        }
    }
}