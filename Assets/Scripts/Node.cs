using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node{
    public bool Walkable;
    public Vector3 WorldPosition;

    public Node(bool _walkable, Vector3 _WorldPosition)
    {
        Walkable = _walkable;
        WorldPosition = _WorldPosition;
    }

}
