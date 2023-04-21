using System;
using System.IO;
using System.Collections.Generic;
public static class MiniMaxOptions
{
    public static int MaxMinMidgame(Node node, int alpha, int beta, ref long stateCounter)
    {
        stateCounter++;
        if (node.IsLeafNode()) return MorrisF.MidgameEndgameStaticEstimation(node);
        else
        {
            var value = -100000;
            foreach (var child in node.GetChildren())
            {
                value = Math.Max(value, MinMaxMidgame(child, alpha, beta, ref stateCounter));
                if (value >= beta)
                {
                    node.SetValue(value);
                    return value;
                }
                else alpha = Math.Max(alpha, value);
            }
            node.SetValue(value);
            return value;
        }
    }

    public static int MinMaxMidgame(Node node, int alpha, int beta, ref long stateCounter)
    {
        stateCounter++;
        if (node.IsLeafNode()) return MorrisF.MidgameEndgameStaticEstimation(node);
        else
        {
            var value = 100000;
            foreach (var child in node.GetChildren())
            {
                value = Math.Min(value, MaxMinMidgame(child, alpha, beta, ref stateCounter));
                if (value <= alpha)
                {
                    node.SetValue(value);
                    return value;
                }
                else beta = Math.Min(value, beta);
            }
            node.SetValue(value);
            return value;
        }
    }
}