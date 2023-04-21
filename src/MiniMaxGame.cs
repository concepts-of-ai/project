using System;
using System.IO;
using System.Collections.Generic;

class MiniMaxGame : ProgramEntry
{
    public static void Main(String[] args)
    {
        var game = new MiniMaxGame();
        game.Run(args);
    }

    protected override (Node, Node) ComputeMinMax(BoardState state, int depth, ref long stateCounter) {
        MorrisF morrisF = new MorrisF();
        Node root = new Node(state);
        Node tree = morrisF.GenerateMovesMidgameEndgame(root, depth, true);

        var value = MiniMaxOptions.MaxMinMidgame(tree,  ref stateCounter, true);
        tree.SetValue(value);
        Node bestChild = tree.findChildNode();
        return (root, bestChild);
    }

}