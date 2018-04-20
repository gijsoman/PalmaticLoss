using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToMove : MonoBehaviour {

    public float Speed;
    public CharacterController CharacterControl;
    public Animator CharacterAnimator;

    private Vector3 position;

	void Start ()
    {
        position = transform.position;
	}
	
	private void Update ()
    {
        if (Input.GetMouseButton(0))
        {
            //Locate where the player clicks on the terrain.
            LocatePosition();
        }

        //Move the player to the position
        MoveToPosition();
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

    private void MoveToPosition()
    {
        if (Vector3.Distance(transform.position, position) > 1f)
        {
            Quaternion newRotation = Quaternion.LookRotation(position - transform.position);

            newRotation.x = 0f;
            newRotation.z = 0f;

            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10);
            CharacterControl.SimpleMove(transform.forward * Speed);

            CharacterAnimator.SetFloat("Forward", Speed);
        }
        else
        {
            CharacterAnimator.SetFloat("Forward", 0);
        }
    }
}
