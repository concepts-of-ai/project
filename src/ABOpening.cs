using System;
using System.IO;
using System.Collections.Generic;

class ABOpening : ProgramEntry
{
    public static void Main(String[] args)
    {
        var game = new ABOpening();
        game.Run(args);
    }

    protected override (Node, Node) ComputeMinMax(BoardState state, int depth, ref long stateCounter)
    {
        MorrisF morrisF = new MorrisF();
        Node root = new Node(state);
        Node tree = morrisF.GenerateMovesOpening(root, depth, true);

        var value = MiniMaxOptions.ABMaxMinOpening(tree, -100000, 1000000, ref stateCounter, true);
        tree.SetValue(value);
        Node bestChild = tree.findChildNode();
        return (root, bestChild);
    }

}
