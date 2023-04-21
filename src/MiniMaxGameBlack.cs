using System;
using System.IO;
using System.Collections.Generic;

class MiniMaxGameBlack : ProgramEntry
{
    public static void Main(String[] args)
    {
        var game = new MiniMaxGameBlack();
        game.Run(args);
    }

    protected override (Node, Node) ComputeMinMax(BoardState state, int depth, ref long stateCounter)
    {
        MorrisF morrisF = new MorrisF();
        Node root = new Node(state);
        Node tree = morrisF.GenerateMovesMidgameEndgame(root, depth, false);     // set playing as white to false

        var value = MiniMaxOptions.MaxMinMidgame(tree, ref stateCounter, false); // set playing as white to false
        tree.SetValue(value);
        Node bestChild = tree.findChildNode();
        return (root, bestChild);
    }

}

// needs to be tested