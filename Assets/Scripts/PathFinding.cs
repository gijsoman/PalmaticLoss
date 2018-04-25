using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour {

    public Transform seeker, target;

    //Offcourse we need our grid to calculate everything
    GridControl grid;

    private void Awake()
    {
        grid = GetComponent<GridControl>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            FindPath(seeker.position, target.position);
        }
    }

    //This method finds a path based on 2 vector3's. A startposition and a target position.
    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        //First we convert the worldpositions to the node positions on the grid by using our converting function.
        Node startNode = grid.GetNodeFromWorldPoint(startPos);
        Node targetNode = grid.GetNodeFromWorldPoint(targetPos);

        //We need 2 lists. 1 List with open nodes (nodes that need to be checked for possible next node). 2 List with closed nodes (nodes that are already checked)
        Heap<Node> openSet = new Heap<Node>(grid.maxSize);
        HashSet<Node> closedSet = new HashSet<Node>();
        //We need to add our starting node to the openset.
        openSet.Add(startNode);

        //We loop as long as there are open nodes to check.
        while (openSet.Count > 0)
        {
            //Our current node is equal to the first node in the open set at start. 
            Node currentNode = openSet.RemoveFirst();
            closedSet.Add(currentNode);

            //When we reached our endNode(targetnode) we can start retracing the path and return.
            if (currentNode == targetNode)
            {
                sw.Stop();
                print("Path found: " + sw.ElapsedMilliseconds + " ms");
                RetracePath(startNode, targetNode);
                return;
            }

            //check each neighbour in the neighbours list based on the current Node.
            foreach (Node neighbour in grid.getNeighbours(currentNode))
            {
                //When our neighbour isnt walkable or already is in the closedSet we skip it.
                if (!neighbour.Walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                //The new movement cost of the neighbour is equal to the current nodes gcost + the cost it takes to move to the new node.
                int newMovementCostToNeighbour = currentNode.gCost + getDistance(currentNode, neighbour);
                //if the openSet doesn't contain the neighbour we check it and set it's costs. If one of our neighbours has a gcost obtained by another node we want to check if this
                //gcost is bigger than the newmovementcost(calculated by the current nodes gcost + the distance). It could happen that the node we are coming from now has lower traveling cost than the node our neighbour got it's gcost from. 
                //therefor we need to update this neighbours gcost because it will be smaller than it's current gcost.
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = getDistance(neighbour, targetNode);
                    //We set the parent of the neighbour to be the current node so we can backtrace the path afterwards.
                    neighbour.parent = currentNode;

                    //We only add our neighbour to the openSet if it doesn't already contain it.
                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                    else
                    {
                        openSet.UpdateItem(neighbour);
                    }
                }
            }
        }
    }

    //Here we create a new list and add all the nodes retraced by the parents of the nodes from the startnode to the endnode.
    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        //We now have a list which starts at the and node and ends at the startnode so we want to reverse this list.
        path.Reverse();

        grid.path = path;
    }

    //Gets the distance between 2 nodes.
    int getDistance(Node a, Node b)
    {
        //First we count the distance between the nodes on both axis on our grid.
        int dstX = Mathf.Abs(a.gridX - b.gridX);
        int dstY = Mathf.Abs(a.gridY - b.gridY);

        //Diagonal is 14 and horizontal or vertical is 10. Depending on which axis counts the most nodes until the b node We calculate the distance.
        if (dstX > dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);
        }
        else
        {
            return 14 * dstX + 10 * (dstY - dstX);
        }
    }
}
