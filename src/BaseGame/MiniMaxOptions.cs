using System;
using System.IO;
using System.Collections.Generic;
public static class MiniMaxOptions
{
    public static int MaxMinMidgame(Node node, ref long stateCounter, bool white, int? alpha = null, int? beta = null)
    {
        stateCounter++;
        if (node.IsLeafNode()) return MorrisF.MidgameEndgameStaticEstimation(node, white);
        else
        {
            if (alpha != null & beta != null)
            {
                var value = -100000;
                foreach (var child in node.GetChildren())
                {
                    value = Math.Max(value, MinMaxMidgame(child, ref stateCounter, white, alpha: alpha, beta: beta));
                    if (value >= beta)
                    {
                        node.SetValue(value);
                        return value;
                    }
                    else alpha = Math.Max((int)alpha!, value);
                }
                node.SetValue(value);
                return value;
            }
            else
            {
                int value = -100000;
                foreach (var child in node.GetChildren())
                {
                    value = Math.Max(value, MinMaxMidgame(child, ref stateCounter, white));
                }
                node.SetValue(value);
                return value;
            }
        }


    }

    public static int MinMaxMidgame(Node node, ref long stateCounter, bool white, int? alpha = null, int? beta = null)
    {
        stateCounter++;
        if (node.IsLeafNode()) return MorrisF.MidgameEndgameStaticEstimation(node, white);
        else
        {
            if (alpha != null && beta != null)
            {
                var value = 100000;
                foreach (var child in node.GetChildren())
                {
                    value = Math.Min(value, MaxMinMidgame(child, ref stateCounter, white, alpha: alpha, beta: beta));
                    if (value <= alpha)
                    {
                        node.SetValue(value);
                        return value;
                    }
                    else beta = Math.Min((int)beta!, value);
                }
                node.SetValue(value);
                return value;
            }
            else
            {
                int value = 100000;
                foreach (var child in node.GetChildren())
                {
                    value = Math.Min(value, MaxMinMidgame(child, ref stateCounter, white));
                }
                node.SetValue(value);
                return value;
            }
        }
    }

    public static int MaxMinOpening(Node node, ref long stateCounter, bool white, int? alpha = null, int? beta = null)
    {
        stateCounter++;
        if (node.IsLeafNode()) return MorrisF.OpeningStaticEstimation(node, white);
        else
        {
            if (alpha != null && beta != null)
            {
                var value = -100000;
                foreach (var child in node.GetChildren())
                {
                    value = Math.Max(value, MinMaxOpening(child, ref stateCounter, white, alpha: alpha, beta: beta));
                    if (value >= beta)
                    {
                        node.SetValue(value);
                        return value;
                    }
                    else alpha = Math.Max((int)alpha!, value);
                }
                node.SetValue(value);
                return value;
            }
            else
            {
                var value = -100000;
                foreach (var child in node.GetChildren())
                {
                    value = Math.Max(value, MinMaxOpening(child, ref stateCounter, white));
                }
                node.SetValue(value);
                return value;
            }
        }
    }

    public static int MinMaxOpening(Node node, ref long stateCounter, bool white, int? alpha = null, int? beta = null)
    {
        stateCounter++;
        if (node.IsLeafNode()) return MorrisF.OpeningStaticEstimation(node, white);
        else
        {
            if (alpha != null && beta != null)
            {
                var value = 100000;
                foreach (var child in node.GetChildren())
                {
                    value = Math.Min(value, MaxMinOpening(child, ref stateCounter, white, alpha: alpha, beta: beta));
                    if (value <= alpha)
                    {
                        node.SetValue(value);
                        return value;
                    }
                    else beta = Math.Min((int)beta!, value);
                }
                node.SetValue(value);
                return value;
            }
            else
            {
                var value = 100000;
                foreach (var child in node.GetChildren())
                {
                    value = Math.Min(value, MaxMinOpening(child, ref stateCounter, white));
                }
                node.SetValue(value);
                return value;
            }
        }
    }


}