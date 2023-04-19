using System;
using System.Collections.Generic;

public static class BoardLayout
{
    public static List<int> GetNeighbors(int idx)
    {
        return adjacencyMatrix[idx];
    }

    public static bool AreNeighbors(int a, int b)
    {
        return adjacencyMatrix[a].Contains(b);
    }

    public static (int, int, int) GetMill(int idx) 
    {
        return (mills[idx][0], mills[idx][1],  mills[idx][2]);
    }

    public static int NumberOfMills()
    {
        return mills.Count;
    }

    private static List<List<int>> mills = new List<List<int>>{
        new List<int>{0,  1,  2},
        new List<int>{3,  4,  5},
        new List<int>{6,  7,  8},
        new List<int>{9,  10, 11},
        new List<int>{12, 13, 14},
        new List<int>{15, 16, 17},
        new List<int>{18, 19, 20},
        new List<int>{21, 22, 23},
        new List<int>{0,  9,  21},
        new List<int>{3,  10, 18},
        new List<int>{6,  11, 15},
        new List<int>{1,  4,  7},
        new List<int>{16, 19, 22},
        new List<int>{8,  12, 17},
        new List<int>{5,  13, 20},
        new List<int>{2,  14, 23},
        new List<int>{0,  3,  6},
        new List<int>{2,  5,  8},
    };

    private static SortedDictionary<int, List<int>> adjacencyMatrix = new SortedDictionary<int, List<int>> {
        {0,  new List<int> {1 ,3, 9}},
        {1,  new List<int> {0 ,4, 2}},
        {2,  new List<int> {1, 5, 14}},
        {3,  new List<int> {0, 4, 6, 10}},
        {4,  new List<int> {1, 3, 5, 7}},
        {5,  new List<int> {2, 4, 8, 13}},
        {6,  new List<int> {3, 7, 11}},
        {7,  new List<int> {4, 6, 8}},
        {8,  new List<int> {5, 7, 12}},
        {9,  new List<int> {0, 10, 21}},
        {10, new List<int> {3, 9, 11, 18}},
        {11, new List<int> {6, 10, 15}},
        {12, new List<int> {8, 13, 17}},
        {13, new List<int> {5, 12, 14, 20}},
        {14, new List<int> {2, 13, 23}},
        {15, new List<int> {11, 16}},
        {16, new List<int> {15, 17, 19}},
        {17, new List<int> {12, 16}},
        {18, new List<int> {10, 19}},
        {19, new List<int> {16, 18, 20, 22}},
        {20, new List<int> {13, 19}},
        {21, new List<int> {9, 22}},
        {22, new List<int> {19, 21, 23}},
        {23, new List<int> {14, 22}},
    };

}
