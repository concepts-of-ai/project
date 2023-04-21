using System;
using System.IO;
using System.Collections.Generic;

class ABGame : ProgramEntry
{
    public static long stateCounter = 0;
    public static void Main(String[] args)
    {   
        var game = new ABGame();
        var (depth, reader) = game.SetUp(args);
        BoardState state = game.Read(reader);
        var (root, bestChild) = game.ComputeMinMax(state, depth, ref stateCounter);
        game.Write(root, bestChild, args[1], ABGame.stateCounter);
    }

    protected override (Node, Node) ComputeMinMax(BoardState state, int depth, ref long stateCounter) {
        MorrisF morrisF = new MorrisF();
        Node root = new Node(state);
        Node tree = morrisF.GenerateMovesMidgameEndgame(root, depth, true);

        var value = MiniMaxOptions.MaxMinMidgame(tree, -100000, 1000000,  ref stateCounter);
        tree.SetValue(value);
        Node bestChild = tree.findChildNode();
        return (root, bestChild);
    }
}