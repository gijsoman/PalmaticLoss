using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node> {
    public bool Walkable;
    public Vector3 WorldPosition;

    //We use these variables so the node can keep track of it's position in the grid.
    public int gridX;
    public int gridY;

    //gCost is distance from the starting node.
    public int gCost;
    //hCost is distance from the endingNode.
    public int hCost;
    public Node parent;

    int heapIndex;

    //a node is one square on the grid. It has a worldposition and the info that can tell is if we can walk on it or not.
    public Node(bool _walkable, Vector3 _WorldPosition, int _gridX, int _gridY)
    {
        Walkable = _walkable;
        WorldPosition = _WorldPosition;
        gridX = _gridX;
        gridY = _gridY;
    }

    //Adds up the distance from the starting node and the distance from the ending node.
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }

}
