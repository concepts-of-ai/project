namespace minimaxopening;
using morrisf;
using node;
using System.IO;

class MiniMaxOpening
{
    public static void MiniMaxOpeningRun(string[] args)
    {
        if (args.Count() != 3)
        {
            Console.WriteLine("\n ***** Not enough input parameters -- try again.\n");
            return;
        }

        StreamReader? reader;
        try
        {
            reader = new StreamReader(args[0]);
        }
        catch (Exception)
        {
            Console.WriteLine("\n ***** Invalid input file.\n");
            return;
        }

        StreamWriter? writer;
        try
        {
            writer = new StreamWriter(args[1]);
        } 
        catch (Exception) 
        {
            Console.WriteLine("\n ***** Invalid output file.\n");
            return;
        }

        int depth;
        try 
        {
            depth = Int32.Parse(args[2]);
        }
        catch (Exception)
        {
            Console.WriteLine("\n ***** Invalid depth value.\n");
            return;
        }

        // read input board from input file
        board.BoardState state;
        string? boardAsString = reader.ReadLine();
        if (boardAsString != null && boardAsString.Length! != 0)
        {
            state = new board.BoardState(boardAsString);
        } else {
            Console.WriteLine("\n ***** Invalid input file value.\n");
            return;
        }

        // compute minimax
        MorrisF morrisF = new MorrisF();
        Node root = new Node(state);
        Node tree = morrisF.GenerateMovesOpening(root, depth, true);
        double value = MaxMin(root);
        Console.WriteLine(value);

        // write output to output file

    }

    static int MaxMin(Node node)
    {
        if (node.IsLeafNode()) return morrisf.MorrisF.OpeningStaticEstimation(node);
        else {
            var value = -100000;
            foreach (var child in node.GetChildren())
            {
                value = Math.Max(value, MinMax(child));
                //Console.WriteLine("--- node: " + node.GetBoard().ToString() + ", value: " + value);
            }
            return value;
        }
    }

    static int MinMax(Node node)
    {
        if (node.IsLeafNode()) return morrisf.MorrisF.OpeningStaticEstimation(node);
        else {
            var value = 100000;
            foreach (var child in node.GetChildren())
            {
                value = Math.Min(value, MaxMin(child));
                //Console.WriteLine("--- node: " + node.GetBoard().ToString() + ", value: " + value);
            }
            return value;
        }
    }

}

// minimaxopening input.txt output.txt 2