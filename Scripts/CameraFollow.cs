using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;

	public float smooth = .2f;
	public float zOffset = -10;

    // void FixedUpdate()
    // {
    // 	float xDest = target.position.x;
    //     Vector3 destination = new Vector3(xDest, 0, zOffset);
    //     Vector3 smoothDestination = Vector3.Lerp(transform.position, destination, smooth);
    //     transform.position = smoothDestination;
    // }

    void LateUpdate(){
    	transform.position = new Vector3(target.position.x, 0, zOffset);
    }

}
