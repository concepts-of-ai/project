namespace board;

enum State
{ // 2 bits determine state of one possition (White, Black, Empty)
    x = 0b00,
    W = 0b01,
    B = 0b10,
    Check = 0b11,
}

class Board
{
    // frist 16 possitions in first block and remaining 8 in second block (24 total)
    const int BlockOne = 0;
    const int BlockOneCapacity = 16;
    const int BlockTwo = 1;
    const int BlockTwoCapacity = 8;
    const int TotalCapacity = 24;

    // array of two uint32 to hold board info
    uint[] board = new uint[2];

    public Board()
    {
        // initialize to empty board
        board[0] = 0;
        board[1] = 0;
    }

    public void Set(string input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            var state = ParseState(input[i].ToString());
            SetPosition(i, state);
        }
    }

    override public String ToString()
    {
        String result = "";
        for (int i = 0; i < TotalCapacity; i++)
        {
            result += GetPosition(i).ToString();
        }
        return result;
    }

    uint GetBlock(int block)
    {
        return board[block];
    }

    void SetBlock(int block, uint val)
    {
        board[block] = val;
    }

    State GetPosition(int pos)
    {
        var (offset, block) = FindBlockAndOffset(pos);

        // bitwise shift so that check bits line up with pos of block
        uint tempBlock = GetBlock(block);   // get a copy of the block
        tempBlock >>= offset;   // offset block so that position is lowest digits
        uint checkResult = tempBlock & (int)State.Check;   // logic AND to get result
        return (State)checkResult; // convert to State
    }

    void SetPosition(int pos, State state)
    {
        var (offset, block) = FindBlockAndOffset(pos);
        uint tempBlock = ClearPosition(offset, block);

        // if settings state to empty, return early
        if (state == State.x)
        {
            SetBlock(block, tempBlock);
            return;
        }

        // move the state you are setting to correct position
        uint s = (uint) state << offset;
        // use logical OR to add it tempblock
        tempBlock = tempBlock | s;
        SetBlock(block, tempBlock);
    }
    uint ClearPosition(int offset, int block)
    {   
        // magic 🪄
        uint checkBits = (uint)State.Check;
        checkBits <<= offset;
        uint mask = ~checkBits;
        uint tempBlock = GetBlock(block);
        return tempBlock & mask;
    }

    State ParseState(String item)
    {
        return (State)Enum.Parse(typeof(State), item, true);
    }

    (int offset, int block) FindBlockAndOffset(int pos)
    {

        var block = BlockOne;
        if (pos < 0 || pos > 23)
        {
            Console.Write("{0} invalid position", pos);
        }

        if (pos >= BlockOneCapacity)
        {
            block = BlockTwo;
            pos -= BlockOneCapacity;
        }

        // position * 2 (since one state is 2 bits per position)
        return (pos * 2, block);
    }
}
