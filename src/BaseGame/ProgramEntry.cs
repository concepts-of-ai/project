using System;
using System.IO;
using System.Collections.Generic;

public abstract class ProgramEntry
{

    protected (int, StreamReader) SetUp(String[] args)
    {
        if (args.Length != 3)
        {
            Console.WriteLine("\n ***** Not enough input parameters -- try again.\n");
            System.Environment.Exit(1);
            return (-1, null);
        }

        StreamReader reader;
        try
        {
            reader = new StreamReader(args[0]);
        }
        catch (Exception)
        {
            Console.WriteLine("\n ***** Invalid input file.\n");
            System.Environment.Exit(1);
            return (-1, null);
        }

        int depth;
        try
        {
            depth = Int32.Parse(args[2]);
        }
        catch (Exception)
        {
            Console.WriteLine("\n ***** Invalid depth value.\n");
            System.Environment.Exit(1);
            return (-1, null);
        }
        return (depth, reader);
    }

    protected BoardState Read(StreamReader reader)
    {
        String? boardAsString = reader.ReadLine();
        if (boardAsString != null && boardAsString.Length! != 0)
        {
            return new BoardState(boardAsString);
        }
        else
        {
            Console.WriteLine("\n ***** Invalid input file value.\n");
            System.Environment.Exit(1);
            return null;
        }
    }

    protected void Write(Node root, Node bestChild, String outputFile, long stateCounter)
    {
        String output = "";
        output += "Input State: " + root?.GetBoard().ToString() + "\n";
        output += "Output State: " + bestChild?.GetBoard().ToString() + "\n";
        output += "States evaluated by static estimation: " + stateCounter + "\n";
        output += "MINIMAX estimate: " + root?.GetValue() + "\n";

        try
        {
            File.WriteAllText(outputFile, output);
        }
        catch (Exception)
        {
            Console.WriteLine("\n ***** Invalid output file.\n");
            return;
        }
    }

    protected abstract (Node, Node) ComputeMinMax(BoardState state, int depth, ref long stateCounter);
}