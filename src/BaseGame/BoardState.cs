using System;
using System.Collections.Generic;

public enum State
{ 
    // 2 bits determine state of one possition (White, Black, Empty)
    x = 0b00,
    W = 0b01,
    B = 0b10,
    Check = 0b11,
}

public class BoardState
{
    // frist 16 possitions in first block and remaining 8 in second block (24 total)
    const int BlockOne = 0;
    const int BlockOneCapacity = 16;
    const int BlockTwo = 1;
    const int BlockTwoCapacity = 8;
    const int TotalCapacity = 24;

    // array of two uint32 to hold board info
    uint[] board = new uint[2];

    public BoardState(string input)
    {
        board[0] = 0;
        board[1] = 0;
        SetBoard(input);
    }

    // copy contructor
    private BoardState(BoardState input)  
    {
        board[0] = input.GetBlock(0);
        board[1] = input.GetBlock(1);
    }

    public BoardState Copy()
    {
        return new BoardState(this);
    }

    public void SetBoard(string input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            var state = ParseState(input[i].ToString());
            SetState(i, state);
        }
    }

    public String GetBoard()
    {
        String result = "";
        for (int i = 0; i < TotalCapacity; i++)
        {
            result += GetState(i).ToString();
        }
        return result;
    }

    public override string ToString()
    {
        return this.GetBoard();
    }

    public void SetState(int pos, State state)
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
        uint s = (uint)state << offset;
        // use logical OR to add it tempblock
        tempBlock = tempBlock | s;
        SetBlock(block, tempBlock);
    }

    public State GetState(int pos)
    {
        var (offset, block) = FindBlockAndOffset(pos);

        // bitwise shift so that check bits line up with pos of block
        uint tempBlock = GetBlock(block);   // get a copy of the block
        tempBlock >>= offset;   // offset block so that position is lowest digits
        uint checkResult = tempBlock & (int)State.Check;   // logic AND to get result
        return (State)checkResult; // convert to State
    }

    public bool IsEmptyPosition(int pos) 
    {
        return GetState(pos) == State.x;
    }

    public int StateCount(State state)
    {
        int count = 0;
        for (int location = 0; location < TotalCapacity; location++)
        {
            if (GetState(location) == state) count++;
        }
        return count;
    }

    uint GetBlock(int block)
    {
        return board[block];
    }

    void SetBlock(int block, uint val)
    {
        board[block] = val;
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

    public void FlipBoard(){
        for(int i = 0; i < TotalCapacity; i++){
            if(GetState(i) == State.W){
                SetState(i, State.B);
            }else if(GetState(i) == State.B){
                SetState(i, State.W);
            }
        }
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
