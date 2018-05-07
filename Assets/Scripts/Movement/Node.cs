using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 //Node needs to be a heapitem otherwise we couldnt store it in a heap because we would lack functionality that a heap item needs.
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

    //Because we are implementen IHeapItem we need to add its functionality. One is the heapIndex.
    //We want our nodes to keep track of their own index in the heap.
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

    //Compare method that compares the fcosts of items and returns a priority value. 1 is high 0 is same and -1 is low. This is some functionality that IheapItem has.
    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        //If the two fcosts are equal we want to start comparing the two hcosts.
        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        //We want to return a higher priority when our fcost is lower so we have to return -compare
        return -compare;
    }

}
