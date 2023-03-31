using board;

public class MorrisF
{
    private const int NumberOfPositions = 24;

    public List<BoardState> GenerateMovesOpening(BoardState board)
    {
        return GenerateAdd(board);
    }

    public List<BoardState> GenerateMovesMidgameEndgame(BoardState board)
    {
        if (board.StateCount(State.W) == 3) return GenerateHopping(board);
        else return GenerateMove(board);
    }

    private List<BoardState> GenerateAdd(BoardState board)
    {
        var states = new List<BoardState>();
        for (int location = 0; location < NumberOfPositions; location++)
        {
            if (board.IsEmptyPosition(location))
            {
                var tempBoard = board.Copy();
                tempBoard.SetState(location, State.W);
                if (CloseMill(location, tempBoard)) GenerateRemove(tempBoard, states);
                else states.Add(tempBoard);
            }
        }
        return new List<BoardState>();
    }

    private List<BoardState> GenerateMove(BoardState board)
    {
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
                        if (CloseMill(position, tempBoard)) GenerateRemove(tempBoard, states);
                        else states.Add(tempBoard); 
                    }
                }
            }
        }
        return new List<BoardState>();
    }

    private List<BoardState> GenerateHopping(BoardState board)
    {
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
                        if (CloseMill(location2, tempBoard)) GenerateRemove(tempBoard, states);
                        else states.Add(tempBoard);
                    }
                }
            }
        }
        return new List<BoardState>();
    }

    private void GenerateRemove(BoardState board, List<BoardState> states)
    {
        var length = states.Count;
        for (int location = 0; location < NumberOfPositions; location++)
        {
            if (board.GetState(location) == State.B)
            {
                if (!CloseMill(location, board))
                {
                    var tempBoard = board.Copy();
                    board.SetState(location, State.x);
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