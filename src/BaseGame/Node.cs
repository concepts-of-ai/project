using System;
using System.Collections.Generic;

public class Node
{
    List<Node> children;
    BoardState state;
    double value = 0;

    public Node(BoardState state)
    { 
        this.state = state; 
        children = new List<Node>();
    }
    public void AddChild(Node n) {
        children.Add(n);
    }
    public bool IsLeafNode()
    {
        return this.children.Count == 0;
    }
    public int Count()
    {
        if (IsLeafNode()) return 1;

        var count = 0;
        foreach (var node in children)
        {
            count = 1 + node.Count();
        }
        return count;
    }
    public BoardState GetBoard() 
    {
        return this.state;
    }
    public List<Node> GetChildren()
    {
        return this.children;
    }
    public int SetValue(int value)
    {
        this.value = value;
        return value;
    }
    public double GetValue()
    {
        return this.value;
    }
    public Node findChildNode()
    {
        foreach (var child in this.GetChildren())
        {
            if(this.value == child.value){
                return child;
            }
        }
        return this;
    }
}