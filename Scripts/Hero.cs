using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{ 
	public float heroVelocity = 1;//movement velocity
	public float jumpForce; //jump aceleration
	public Sprite deathSprite;
	public bool imDeath = false; //notify gamecontroler that hero has died

	private Animator heroAnimator;
	private SpriteRenderer heroSpriteRenderer;
	private Rigidbody2D heroRB;
	private Collider2D heroCl2D;


    // Start is called before the first frame update
    void Start()
    {
        heroAnimator = gameObject.GetComponent<Animator>();
        heroSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        heroRB = gameObject.GetComponent<Rigidbody2D>();
        heroCl2D = gameObject.GetComponent<Collider2D>();
    }

    public void turnOnOffMovementAnim(){
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

    public IEnumerator death(){
    	heroRB.velocity = Vector3.zero;//makes him stop
    	heroRB.constraints = RigidbodyConstraints2D.FreezeAll;//avoid movement
    	heroAnimator.enabled = false; //avoid aniamtion transition
    	heroSpriteRenderer.sprite = deathSprite;//force a sprite

    	yield return new WaitForSeconds(2);//force the user to see the dead player
    	deathFall();
    	yield break;
    }

    private void deathFall(){
    	heroRB.constraints = RigidbodyConstraints2D.FreezeRotation;//allow player to move but no to rotate, as usual
    	heroCl2D.enabled = false;//turning player collider off so he can move frely
    	heroRB.AddForce(new Vector2(0, 100));
    }

    public bool deathMessenger(){
    	return imDeath;
    }

    void OnCollisionEnter2D(Collision2D other){
     	if (other.transform.tag == "Enemy") {
            imDeath = true;
     	}
 	}
}
