using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject Player;

    public float height = 5.0f;
    public float distance = 5.0f;

    Vector3 offset;

	private void Start ()
    {
        offset = new Vector3(0, 0 + height, 0 - distance);
	}
	
	private void LateUpdate ()
    {
        transform.position = Player.transform.position + offset;
    }
}
