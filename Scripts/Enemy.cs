using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[Header("Trigger")]
	public Hero hero; //the hero. we need him to check the distance between us
	[HideInInspector]public bool go = false; //teel when the enemy can move
	public float triggerDistance = 3f; //minimum distance between hero and enemy

	[Header("Movement")]
	public bool doesEnemyMoves;
	public float velocity;
	public Vector2 destinationOffset;
	[HideInInspector]public Vector2 destination;

	[Header("Rotation")]
	public float rotationVelocity;

	private GlobalParameters gp = new GlobalParameters(); 

	public void getHero(){
		hero = GameObject.Find("Hero").GetComponent<Hero>();
	}

    public void checkTrigger(){
        if(Vector2.Distance(hero.transform.position, transform.position) < triggerDistance && !go){
    		go = true;
    	}
    }

    public void destroyMe(){
    	if(transform.position.y <= gp.fallDeath || transform.position.x <= gp.xDeath){
        	Destroy(gameObject);
        }
    }

    public void rotate(float rotVel, int inversor = 1){
	    transform.Rotate (0, 0, 50 * Time.deltaTime * rotVel * inversor); //rotates 50 degrees per second around z axis
    }

    public void move(Vector3 destination, float velocity){
	  	 transform.position = Vector2.MoveTowards(transform.position, destination, Time.deltaTime * velocity);
    }
}
