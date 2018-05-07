using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridControl : MonoBehaviour {
    public bool displayGridGizmos;

    //We need a layer mask to check if the object in the node is walkable or not.
    public LayerMask UnwalkableMask;
    public Vector2 GridWorldSize;
    public float NodeRadius;

    //A two-dimensional array of nodes that is going to contain x and y values.
    Node[,] grid;

    private float nodeDiameter;
    private int gridSizeX, gridSizeY;

    //On start we determine the node diameter (size of the node) And we set the grid size for x and y. We also create the grid.
    private void Awake()
    {
        nodeDiameter = NodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(GridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(GridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    //returns the grid size by multiplying gridSizeX with gridSizeY;
    public int maxSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }

    //This function creates the grid with the nodes.
    private void CreateGrid()
    {
        //First we say how much X and how much Y values should be reserved in the array.
        grid = new Node[gridSizeX, gridSizeY];
        //We create a new vector3 for the bottemleft corner of our grid. We start in the middle and work our way to the bottom left.
        Vector3 worldBottomLeft = transform.position - Vector3.right * GridWorldSize.x / 2 - Vector3.forward * GridWorldSize.y / 2;

        //Here we are going to fill our node array with new nodes based on the next calculated position in the world. We also check here if we collide with an unwalkable object.
        //if this is the case this node is unwalkable.
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + NodeRadius) + Vector3.forward * (y * nodeDiameter + NodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, NodeRadius, UnwalkableMask));
                grid[x, y] = new Node(walkable, worldPoint,x,y);
            }
        }
    }

    //Draw our gizmo's so we can actually see our grid.
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(GridWorldSize.x, 1, GridWorldSize.y));

            if (grid != null && displayGridGizmos)
            {
                //Node playerNode = GetNodeFromWorldPoint(player.position);
                foreach (Node n in grid)
                {
                    Gizmos.color = (n.Walkable) ? Color.white : Color.red;
                    Gizmos.DrawCube(n.WorldPosition, Vector3.one * (nodeDiameter - .1f));
                }
            }
    }

    //Returns a list of neighbour nodes based on the inputted node.
    public List<Node> getNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        //Search in a 3x3 block around the node relative to the nodes position.
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                //Skip the node itself
                if (x == 0 && y == 0)
                {
                    continue;
                }

                //We need to set checkX and checkY to find the position of the neighbours on the grid.
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                //Only if checkX or checkY is on the grid we add it to our neighbours list.
                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }

    //With this function we can get a node from our grid array based on a vector3. Convert world position to node on the grid.
    public Node GetNodeFromWorldPoint(Vector3 worldposition)
    {
        //First we create 2 percentages (between 0 and 1) so we can check on which half of our grid this position is. 0 - 0.5 is the left half for x and 0.5 - 1 is the right half.
        //this also counts for Y.
        float percentX = (worldposition.x + GridWorldSize.x / 2) / GridWorldSize.x;
        float percentY = (worldposition.z + GridWorldSize.y / 2) / GridWorldSize.y;

        //We round the numbers so we wont calculate anything that's off of our grid.
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        //Last but not least we make 2 variables that are going to contain the values for x and y of the asked node. We round them to an int cause we cant search for decimals in an array (our grid is an array);
        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        //Return the node as asked for.
        return grid[x, y];
    }
}
