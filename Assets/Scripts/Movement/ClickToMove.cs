using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToMove : MonoBehaviour {

    public bool displayPathFindingGizmos;
    public float Speed;
    public Animator CharacterAnimator;

    Vector3[] path;
    int targetIndex;

    private Vector3 position;

	void Awake ()
    {
    }
	
	private void Update ()
    {
        if (Input.GetMouseButton(0))
        {
            //Locate where the player clicks on the terrain.
            LocatePosition();
            //Debug.Log("Requesting...");
            targetIndex = 0;
            PathRequestManager.RequestPath(transform.position, position, OnPathFound);
        }
	}

    //Will locate the position on the terrain where the user clicked
    private void LocatePosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
   
        if (Physics.Raycast(ray, out hit, 1000))
        {
            position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccesfull)
    {
        if (pathSuccesfull)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];

        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, Speed * Time.deltaTime);
            yield return null;
        }
        
    }

    public void OnDrawGizmos()
    {
        if (path != null && displayPathFindingGizmos)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);
                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}
