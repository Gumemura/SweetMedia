using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XEnemy : Enemy
{
    void Start()
    {
        destination = (Vector2)transform.position - destinationOffset;
    }

    void Update()
    {
    	checkTrigger();
    	rotate(rotationVelocity);

    	if(go && doesEnemyMoves){
    		move(destination, velocity);
    	}
    	destroyMe();
    }
}
