using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{ 
	public float heroVelocity = 1;//movement velocity
	public float jumpForce; //jump aceleration

	private Animator heroAnimator;
	private SpriteRenderer heroSpriteRenderer;
	private Rigidbody2D heroRB;

    // Start is called before the first frame update
    void Start()
    {
        heroAnimator = gameObject.GetComponent<Animator>();
        heroSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        heroRB = gameObject.GetComponent<Rigidbody2D>();
    }

    public void turnOnOffMovementAnim(bool onOff){
    	/*
    	activate and deactivate moviment animation
    	RECEIVES
    		onOff: bool
    	*/
    	heroAnimator.SetBool("isMoving", !heroAnimator.GetBool("isMoving"));
    }

    public void move(int destination){
    	/*
    	RECEIVES
    		destination: int
    		1 is for right
    		-1 is for left
    	*/
    	heroSpriteRenderer.flipX = (destination == -1);
    	Vector2 direction = new Vector2(destination, 0);
    	Vector2 currentPosition = (Vector2)transform.position;
    	transform.position = Vector2.MoveTowards(currentPosition, currentPosition + direction, Time.deltaTime * heroVelocity);
    }

    public void jump(){
    	heroAnimator.SetBool("isJumping", true);
    	heroRB.AddForce(new Vector2(0, jumpForce));
    }

    public bool triggerJumpAnim(){
    	if(heroRB.velocity.y != 0){
			heroAnimator.SetBool("isJumping", true);
			return true;
    	}else{
			heroAnimator.SetBool("isJumping", false);
			return false;
    	}
    }
}
