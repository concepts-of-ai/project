using System;
using System.Collections.Generic;

public class MorrisF
{
    private int NumberOfPositions = 24;

    public Node GenerateMovesOpening(Node root, int depth, bool white)
    {
        GenerateAdd(root, depth, white);
        return root;
    }

    public Node GenerateMovesMidgameEndgame(Node root, int depth, bool white)
    {
        State currentState;
        if (white) currentState = State.W;
        else currentState = State.B;

        if (root.GetBoard().StateCount(currentState) == 3) GenerateHopping(root, depth, white);
        else GenerateMove(root, depth, white);
        return root;
    }

    public static int OpeningStaticEstimation(Node root)
    {
        var value = root.GetBoard().StateCount(State.W) - root.GetBoard().StateCount(State.B);
        return root.SetValue(value);
    }


    public static int MidgameEndgameStaticEstimation(Node root)
    {
        var numOfBlackMoves = root.Count();
        var numOfBlackPieces = root.GetBoard().StateCount(State.B);
        var numOfWhitePieces = root.GetBoard().StateCount(State.W);

        if (numOfBlackPieces <= 2) return root.SetValue(10000);
        else if (numOfWhitePieces <= 2) return root.SetValue(-10000);
        else if (numOfBlackMoves == 0) return root.SetValue(10000);
        else return root.SetValue(1000 * (numOfWhitePieces - numOfBlackPieces) - numOfBlackMoves);
    }

    private void GenerateAdd(Node node, int depth, bool white)
    {
        if (depth == 0) return;
        State currentState;
        if (white) currentState = State.W;
        else currentState = State.B;

        for (int location = 0; location < NumberOfPositions; location++)
        {
            if (node.GetBoard().IsEmptyPosition(location))
            {
                var tempBoard = node.GetBoard().Copy();
                tempBoard.SetState(location, currentState);
                Node tempNode = new Node(tempBoard);
                if (CloseMill(location, tempBoard)) 
                {
                    GenerateRemove(node, tempNode, depth, white, () => GenerateMovesOpening(node, depth - 1, !white));
                }
                else
                {
                    node.AddChild(tempNode);
                    //Console.WriteLine(tempNode.GetBoard().ToString());
                }
                GenerateMovesOpening(tempNode, depth - 1, !white);
            }
        }
    }

    private void GenerateMove(Node node, int depth, bool white)
    {
        if (depth == 0) return;

        State currentState;
        if (white) currentState = State.W;
        else currentState = State.B;

        for (int location = 0; location < NumberOfPositions; location++)
        {
            if (node.GetBoard().GetState(location) == currentState)
            {
                var neighbors = BoardLayout.GetNeighbors(location);
                for (int position = 0; position < neighbors.Count; position++)
                {
                    if (node.GetBoard().IsEmptyPosition(neighbors[position]))
                    {
                        var tempBoard = node.GetBoard().Copy();
                        tempBoard.SetState(location, State.x);
                        tempBoard.SetState(position, currentState);
                        var tempNode = new Node(tempBoard);
                        if (CloseMill(position, tempBoard))
                        {
                            GenerateRemove(node, tempNode, depth, white, () => GenerateMovesMidgameEndgame(node, depth - 1, !white));
                        }
                        else node.AddChild(tempNode);
                        GenerateMovesMidgameEndgame(node, depth - 1, !white);
                    }
                }
            }
        }
    }

    private void GenerateHopping(Node node, int depth, bool white)
    {
        if (depth == 0) return;

        State currentState;
        if (white) currentState = State.W;
        else currentState = State.B;

        for (int location = 0; location < NumberOfPositions; location++)
        {
            if (node.GetBoard().GetState(location) == currentState)
            {
                for (int location2 = 0; location2 < NumberOfPositions; location2++)
                {
                    if (node.GetBoard().IsEmptyPosition(location2))
                    {
                        var tempBoard = node.GetBoard().Copy();
                        tempBoard.SetState(location, State.x);
                        tempBoard.SetState(location2, currentState);
                        var tempNode = new Node(tempBoard);
                        if (CloseMill(location2, tempBoard)) 
                        { 
                            GenerateRemove(node, tempNode, depth, white, () => GenerateMovesMidgameEndgame(node, depth - 1, !white)); 
                        }
                        else node.AddChild(tempNode);
                        GenerateMovesMidgameEndgame(node, depth - 1, !white);
                    }
                }
            }
        }
    }

    private void GenerateRemove(Node root, Node node, int depth, bool white, System.Action callBack)
    {
        State stateToRemove;
        if (white) stateToRemove = State.B;
        else stateToRemove = State.W;

        var length = node.Count();
        for (int location = 0; location < NumberOfPositions; location++)
        {
            if (node.GetBoard().GetState(location) == stateToRemove)
            {
                if (!CloseMill(location, node.GetBoard()))
                {
                    var tempBoard = node.GetBoard().Copy();
                    tempBoard.SetState(location, State.x);
                    //Console.WriteLine("======= " + tempBoard.ToString());
                    var tempNode = new Node(tempBoard);
                    root.AddChild(tempNode);
                    callBack();
                }
            }
        }
        if (node.Count() == length)
        {
            for (int location = 0; location < NumberOfPositions; location++)
            {
                if (node.GetBoard().GetState(location) == stateToRemove)
                {
                    var tempBoard = node.GetBoard().Copy();
                    tempBoard.SetState(location, State.x);
                    var tempNode = new Node(tempBoard);
                    root.AddChild(tempNode);
                    callBack();
                }
            }
        }

    }

    private List<int> Neighbors(int location)
    {
        return BoardLayout.GetNeighbors(location);
    }

    private bool CloseMill(int location, BoardState board)
    {   // refactor to make this more efficent later
        State pos = board.GetState(location);
        for (int i = 0; i < BoardLayout.NumberOfMills(); i++)
        {
            var (a, b, c) = BoardLayout.GetMill(i);
            State s1 = board.GetState(a);
            State s2 = board.GetState(b);
            State s3 = board.GetState(c);
            if (s1 == pos && s2 == pos && s3 == pos)
            {
                //Console.WriteLine("Close Mill: " + a.ToString("00") + " " + b.ToString("00") + " " + c.ToString("00") + " || state: " + pos.ToString());
                return true;
            }
        }
        return false;
    }

}