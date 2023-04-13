using board;
namespace morrisf;

public class MorrisF
{
    private const int NumberOfPositions = 24;

    public List<BoardState> GenerateMovesOpening(BoardState board, int depth)
    {
        return GenerateAdd(board, depth);
    }

    public List<BoardState> GenerateMovesMidgameEndgame(BoardState board, int depth)
    {
        if (board.StateCount(State.W) == 3) return GenerateHopping(board, depth);
        else return GenerateMove(board, depth);
    }

    public int OpeningStaticEstimation(BoardState board)
    {
        return board.StateCount(State.W) - board.StateCount(State.B);
    }
        

    public int MidgameEndgameStaticEstimation(BoardState board,  List<BoardState> states)
    {   
        var numOfBlackMoves = states.Count();
        var numOfBlackPieces = board.StateCount(State.B);
        var numOfWhitePieces = board.StateCount(State.W);
       
        if (numOfBlackPieces <= 2) return 10000;
        else if (numOfWhitePieces <= 2) return -10000;
        else if (numOfBlackMoves == 0) return 10000;
        else return 1000 * (numOfWhitePieces - numOfBlackPieces) - numOfBlackMoves;
    }

    private List<BoardState> GenerateAdd(BoardState board, int depth)
    {
        if (depth < 0) return new List<BoardState>();

        var states = new List<BoardState>();
        for (int location = 0; location < NumberOfPositions; location++)
        {
            if (board.IsEmptyPosition(location))
            {
                var tempBoard = board.Copy();
                tempBoard.SetState(location, State.W);
                if (CloseMill(location, tempBoard)) GenerateRemove(tempBoard, states, depth - 1);
                else states.Add(tempBoard);
            }
        }
        return new List<BoardState>();
    }

    private List<BoardState> GenerateMove(BoardState board, int depth)
    {
        if (depth < 0) return new List<BoardState>();

        var states = new List<BoardState>();
        for (int location = 0; location < NumberOfPositions; location++)
        {
            if (board.GetState(location) == State.W)
            {
                var neighbors = BoardLayout.GetNeighbors(location);
                for (int position = 0; position < neighbors.Count; position++) 
                {
                    if (board.IsEmptyPosition(position))
                    {
                        var tempBoard = board.Copy();
                        tempBoard.SetState(location, State.x);
                        tempBoard.SetState(position, State.W);
                        if (CloseMill(position, tempBoard)) GenerateRemove(tempBoard, states, depth);
                        else states.Add(tempBoard); 
                    }
                }
            }
        }
        return new List<BoardState>();
    }

    private List<BoardState> GenerateHopping(BoardState board, int depth)
    {
        if (depth < 0) return new List<BoardState>();

        var states = new List<BoardState>();
        for (int location = 0; location < NumberOfPositions; location++) {
            if (board.GetState(location) == State.W)
            {
                for (int location2 = 0; location2 < NumberOfPositions; location2++)
                {
                    if (board.IsEmptyPosition(location2))
                    {
                        var tempBoard = board.Copy();
                        tempBoard.SetState(location, State.x);
                        tempBoard.SetState(location2, State.W);
                        if (CloseMill(location2, tempBoard)) GenerateRemove(tempBoard, states, depth);
                        else states.Add(tempBoard);
                    }
                }
            }
        }
        return new List<BoardState>();
    }

    private void GenerateRemove(BoardState board, List<BoardState> states, int depth)
    {
        if (depth < 0) return;

        var length = states.Count;
        for (int location = 0; location < NumberOfPositions; location++)
        {
            if (board.GetState(location) == State.B)
            {
                if (!CloseMill(location, board))
                {
                    var tempBoard = board.Copy();
                    tempBoard.SetState(location, State.x);
                    states.Add(tempBoard);
                }
            }
        }
        if (states.Count == length) 
        {
            for (int location = 0; location < NumberOfPositions; location++)
            {
                if (board.GetState(location) == State.B)
                {
                    var tempBoard = board.Copy();
                    tempBoard.SetState(location, State.x);
                    states.Add(tempBoard);
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