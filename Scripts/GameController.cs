using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{ 
	public Hero hero; //the controlable hero

	private float halfWidthOfTheScreen; //x coordinate of half of the screen
	private float halfHeightOfTheScreen; //x coordinate of half of the screen

	private bool goingToJump; //used to prevent movement
	private bool isJumping = false; //hero is jumping

	void Start()
	{
		halfWidthOfTheScreen = Display.main.systemWidth / 2;
		halfHeightOfTheScreen = Display.main.systemHeight / 2;
	}

	void Update()
	{
		foreach(Touch touch in Input.touches){
			switch (touch.phase){
				case TouchPhase.Began:
					//jump
					if(touch.position.y > halfHeightOfTheScreen && !isJumping){
						hero.jump();
						isJumping = true;
					}else if(touch.position.y < halfHeightOfTheScreen){
						hero.turnOnOffMovementAnim(true);
					}
					break;

				case TouchPhase.Ended:
					if(touch.position.y < halfHeightOfTheScreen){
						hero.turnOnOffMovementAnim(false);
					}
					break;   
			}

			//movement
			if(touch.position.y < halfHeightOfTheScreen){
				if(touch.position.x > halfWidthOfTheScreen){
					hero.move(1);//moving right
				}else{
					hero.move(-1);//moving left
				}
			}
		}


		isJumping = hero.triggerJumpAnim();
		
	}
}
