using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleverOEnemy : Enemy
{
	public Vector2[] allDestinations;
	[Header("Movement")]
	public float miniumDistance = .5f;
	private int index = 0;
	private int inversor = 1;

	void Start(){
		destination = allDestinations[index++];
	}

	void Update()
	{
		rotate(rotationVelocity, inversor);
		if(Vector2.Distance((Vector2)transform.position, destination) > miniumDistance){
			move(destination, velocity);
		}else{
			destination = allDestinations[index++];
			inversor *= -1;
			if(index == allDestinations.Length){
				index = 0;
			}
		}
	}
}
