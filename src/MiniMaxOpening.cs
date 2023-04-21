using System;
using System.IO;
using System.Collections.Generic;

class MiniMaxOpening : ProgramEntry
{
    public static void Main(String[] args)
    {
       var game = new MiniMaxOpening();
       game.Run(args);
    }

    protected override (Node, Node) ComputeMinMax(BoardState state, int depth, ref long stateCounter)
    {
        MorrisF morrisF = new MorrisF();
        Node root = new Node(state);
        Node tree = morrisF.GenerateMovesOpening(root, depth, true);

        var value = MiniMaxOptions.MaxMinOpening(tree,  ref stateCounter);
        tree.SetValue(value);
        Node bestChild = tree.findChildNode();
        return (root, bestChild);
    }

}
