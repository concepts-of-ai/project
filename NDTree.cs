using board;
namespace tree;

class Node
{
    List<Node> children;
    BoardState state;

    public Node(BoardState state)
    { 
        this.state = state; 
        children = new List<Node>();
    }
    public void Add(Node n) {
        children.Add(n);
    }
    public bool IsLeafNode()
    {
        return this.children.Count() == 0;
    }
}

class NDTree
{
    Node root;

    public NDTree(Node n) 
    { 
        this.root = n; 
    }

}