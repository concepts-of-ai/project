using board;
using minimaxgame;
using minimaxopening;

internal class ProjectEntryPoint
{
    private static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Enter function name followed by parameters: ");
            Console.WriteLine("      example: MiniMaxOpening <intput-file> <output-file> <depth>");
            Console.Write("\nCommand: ");
            var val = Console.ReadLine();
            if (val == null)
            {
                Console.WriteLine("==== Error: Reading null value");
                break;
            }

            var parameters = val.Split(' ');
            string function = parameters[0];
            function = function.ToLower();
            parameters = parameters.Where((item, index) => index != 0).ToArray();

            if (function == "minimaxgame")
            {
                minimaxgame.MiniMaxGame.MiniMaxGameRun(parameters);
            }
            else if (function == "minimaxopening")
            {
                minimaxopening.MiniMaxOpening.MiniMaxOpeningRun(parameters);
            }
            // ... put other cases here
            else {
                Console.WriteLine("\n***** Not a valid program name -- try again.\n");
            }
        }
    }
}