using board;
using node;
namespace morrisf;

public class MorrisF
{
    private int NumberOfPositions = 24;

    public Node GenerateMovesOpening(Node root, int depth, bool white)
    {
        //Node root = new Node(board);
        GenerateAdd(root, depth, white);
        return root;
    }

    public Node GenerateMovesMidgameEndgame(BoardState board, int depth)
    {
        Node root = new Node(board);
        if (board.StateCount(State.W) == 3) GenerateHopping(root, depth);
        else GenerateMove(root, depth);
        return root;
    }

    public static int OpeningStaticEstimation(Node root)
    {
        return root.GetBoard().StateCount(State.W) - root.GetBoard().StateCount(State.B);
    }
        

    public static int MidgameEndgameStaticEstimation(Node root)
    {   
        var numOfBlackMoves = root.Count();
        var numOfBlackPieces = root.GetBoard().StateCount(State.B);
        var numOfWhitePieces = root.GetBoard().StateCount(State.W);
       
        if (numOfBlackPieces <= 2) return 10000;
        else if (numOfWhitePieces <= 2) return -10000;
        else if (numOfBlackMoves == 0) return 10000;
        else return 1000 * (numOfWhitePieces - numOfBlackPieces) - numOfBlackMoves;
    }

    private void GenerateAdd(Node node, int depth, bool white)
    {
        if (depth < 0) return;

        for (int location = 0; location < NumberOfPositions; location++)
        {
            if (node.GetBoard().IsEmptyPosition(location))
            {
                var tempBoard = node.GetBoard().Copy();
                if(white) tempBoard.SetState(location, State.W);
                else tempBoard.SetState(location, State.B);
                Node tempNode = new Node(tempBoard);
                if (CloseMill(location, tempBoard)) GenerateRemove(tempNode, depth);
                else{
                    node.AddChild(tempNode);
                    //Console.WriteLine(tempNode.GetBoard().ToString());
                }
                GenerateMovesOpening(tempNode, depth - 1, !white);
            }
        }
    }

    private void GenerateMove(Node node, int depth)
    {
        if (depth < 0) return;

        for (int location = 0; location < NumberOfPositions; location++)
        {
            if (node.GetBoard().GetState(location) == State.W)
            {
                var neighbors = BoardLayout.GetNeighbors(location);
                for (int position = 0; position < neighbors.Count; position++) 
                {
                    if (node.GetBoard().IsEmptyPosition(neighbors[position]))
                    {
                        var tempBoard = node.GetBoard().Copy();
                        tempBoard.SetState(location, State.x);
                        tempBoard.SetState(position, State.W);
                        var tempNode = new Node(tempBoard);
                        if (CloseMill(position, tempBoard)) GenerateRemove(node, depth);
                        else node.AddChild(tempNode); 
                    }
                }
            }
        }
    }

    private void GenerateHopping(Node node, int depth)
    {
        for (int location = 0; location < NumberOfPositions; location++) {
            if (node.GetBoard().GetState(location) == State.W)
            {
                for (int location2 = 0; location2 < NumberOfPositions; location2++)
                {
                    if (node.GetBoard().IsEmptyPosition(location2))
                    {
                        var tempBoard = node.GetBoard().Copy();
                        tempBoard.SetState(location, State.x);
                        tempBoard.SetState(location2, State.W);
                        var tempNode = new Node(tempBoard);
                        if (CloseMill(location2, tempBoard)) GenerateRemove(node, depth);
                        else node.AddChild(tempNode);
                    }
                }
            }
        }
    }

    private void GenerateRemove(Node node, int depth)
    {
        var length = node.Count();
        for (int location = 0; location < NumberOfPositions; location++)
        {
            if (node.GetBoard().GetState(location) == State.B)
            {
                if (!CloseMill(location, node.GetBoard()))
                {
                    var tempBoard = node.GetBoard().Copy();
                    tempBoard.SetState(location, State.x);
                    var tempNode = new Node(tempBoard);
                    node.AddChild(tempNode);
                }
            }
        }
        if (node.Count() == length) 
        {
            for (int location = 0; location < NumberOfPositions; location++)
            {
                if (node.GetBoard().GetState(location) == State.B)
                {
                    var tempBoard = node.GetBoard().Copy();
                    tempBoard.SetState(location, State.x);
                    var tempNode = new Node(tempBoard);
                    node.AddChild(tempNode);
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
            if (s1 == pos && s2 == pos && s3 == pos) return true;
        }
        return false;
    }

}