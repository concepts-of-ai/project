using System;
using System.IO;
using System.Collections.Generic;
public static class MiniMaxOptions
{
    public static int ABMaxMinMidgame(Node node, int alpha, int beta, ref long stateCounter, bool white)
    {
        stateCounter++;
        if (node.IsLeafNode()) return MorrisF.MidgameEndgameStaticEstimation(node, white);
        else
        {
            var value = -100000;
            foreach (var child in node.GetChildren())
            {
                value = Math.Max(value, ABMinMaxMidgame(child, alpha, beta, ref stateCounter, white));
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

    public static int ABMinMaxMidgame(Node node, int alpha, int beta, ref long stateCounter, bool white)
    {
        stateCounter++;
        if (node.IsLeafNode()) return MorrisF.MidgameEndgameStaticEstimation(node, white);
        else
        {
            var value = 100000;
            foreach (var child in node.GetChildren())
            {
                value = Math.Min(value, ABMaxMinMidgame(child, alpha, beta, ref stateCounter, white));
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


    public static int MaxMinMidgame(Node node, ref long stateCounter, bool white)
    {
        stateCounter++;
        if (node.IsLeafNode()) return MorrisF.MidgameEndgameStaticEstimation(node, white);
        else {
            int value = -100000;
            foreach (var child in node.GetChildren())
            {
                value = Math.Max(value, MinMaxMidgame(child, ref stateCounter, white));
            }
            node.SetValue(value);
            return value;
        }
    }

    public static int MinMaxMidgame(Node node, ref long stateCounter, bool white)
    {
        stateCounter++;
        if (node.IsLeafNode()) return MorrisF.MidgameEndgameStaticEstimation(node, white);
        else {
            int value = 100000;
            foreach (var child in node.GetChildren())
            {
                value = Math.Min(value, MaxMinMidgame(child, ref stateCounter, white));
            }
            node.SetValue(value);
            return value;
        }
    }

    public static int MaxMinOpening(Node node, ref long stateCounter, bool white)
    {
        stateCounter++;
        if (node.IsLeafNode()) return MorrisF.OpeningStaticEstimation(node, white);
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

    public static int MinMaxOpening(Node node, ref long stateCounter, bool white)
    {
        stateCounter++;
        if (node.IsLeafNode()) return MorrisF.OpeningStaticEstimation(node, white);
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

     public static int ABMaxMinOpening(Node node, int alpha, int beta, ref long stateCounter, bool white)
    {
        stateCounter++;
        if (node.IsLeafNode()) return MorrisF.OpeningStaticEstimation(node, white);
        else {
            var value = -100000;
            foreach (var child in node.GetChildren())
            {
                value = Math.Max(value, ABMinMaxOpening(child, alpha, beta, ref stateCounter, white));
				if(value >= beta){
		            node.SetValue(value);
					return value;
				}
				else alpha = Math.Max(alpha, value);
            }
            node.SetValue(value);
            return value;
        }
    }

    public static int ABMinMaxOpening(Node node, int alpha, int beta, ref long stateCounter, bool white)
    {
        stateCounter++;
        if (node.IsLeafNode()) return MorrisF.OpeningStaticEstimation(node, white);
        else {
            var value = 100000;
            foreach (var child in node.GetChildren())
            {
                value = Math.Min(value, ABMaxMinOpening(child, alpha, beta, ref stateCounter, white));
				if(value <= alpha){
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