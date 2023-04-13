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
        Node treeRoot;
        string? boardAsString = reader.ReadLine();
        if (boardAsString != null && boardAsString.Length! != 0)
        {
            board.BoardState state = new board.BoardState(boardAsString);
            treeRoot = new Node(state);
        } else {
            Console.WriteLine("\n ***** Invalid input file value.\n");
            return;
        }

        // compute minimax



        // write output to output file

    }

    double MaxMin(Node node)
    {
        if (node.IsLeafNode()) return morrisf.MorrisF.OpeningStaticEstimation(node.GetBoard());
        else {
            var value = double.NegativeInfinity;
            foreach (var child in node.GetChildren())
            {
                value = Math.Max(value, MinMax(child));
            }
            return value;
        }
    }

    double MinMax(Node node)
    {
        if (node.IsLeafNode()) return morrisf.MorrisF.OpeningStaticEstimation(node.GetBoard());
        else {
            var value = double.PositiveInfinity;
            foreach (var child in node.GetChildren())
            {
                value = Math.Min(value, MaxMin(child));
            }
            return value;
        }
    }

}