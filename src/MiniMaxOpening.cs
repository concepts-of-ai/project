using System;
using System.IO;
using System.Collections.Generic;

class MiniMaxOpening
{
    public static long stateCounter;

    public static void Main(String[] args)
    {
        stateCounter = 0;
        if (args.Length != 3)
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
        BoardState state;
        string? boardAsString = reader.ReadLine();
        if (boardAsString != null && boardAsString.Length! != 0)
        {
            state = new BoardState(boardAsString);
        }
        else
        {
            Console.WriteLine("\n ***** Invalid input file value.\n");
            return;
        }

        // compute minimax
        MorrisF morrisF = new MorrisF();
        Node root = new Node(state);
        Node tree = morrisF.GenerateMovesOpening(root, depth, true);

        tree.SetValue(MaxMin(tree));
        Node bestChild = tree.findChildNode();
        
        String output = "";
        output += "Input State: " + root.GetBoard().ToString() + "\n";
        output += "Output State: " + bestChild.GetBoard().ToString() + "\n";
        output += "States evaluated by static estimation: " + stateCounter + "\n";
        output += "MINIMAX estimate: " + root.GetValue() + "\n";

        try
        {
            File.WriteAllText(args[1], output);
        }
        catch (Exception)
        {
            Console.WriteLine("\n ***** Invalid output file.\n");
            return;
        }
    }

    static int MaxMin(Node node)
    {
        stateCounter++;
        if (node.IsLeafNode()) return MorrisF.OpeningStaticEstimation(node);
        else
        {
            var value = -100000;
            foreach (var child in node.GetChildren())
            {
                value = Math.Max(value, MinMax(child));
            }
            node.SetValue(value);
            return value;
        }
    }

    static int MinMax(Node node)
    {
        stateCounter++;
        if (node.IsLeafNode()) return MorrisF.OpeningStaticEstimation(node);
        else
        {
            var value = 100000;
            foreach (var child in node.GetChildren())
            {
                value = Math.Min(value, MaxMin(child));
            }
            node.SetValue(value);
            return value;
        }
    }

}

// minimaxopening input.txt output.txt 2