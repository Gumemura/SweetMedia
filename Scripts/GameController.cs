/*
KNOW BUGS
	if the user press the same 'walk button' a second time, the player will move with double speed
	sometimes the bool that activate the walk animation flips and the player moves when stoped
	halfWidthOfTheScreen and halfHeightOfTheScreen doesnt work properly on mobile only on editor
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; 


public class GameController : MonoBehaviour
{ 
	public Hero hero; //the controlable hero
	public Button retryButton;
	public GameObject tutorial;

	private float halfWidthOfTheScreen; //x coordinate of half of the screen
	private float halfHeightOfTheScreen; //x coordinate of half of the screen
	private string thisScene = "Game"; //used to realod the game in case of death
	private bool canPlay = true; //set to false if player die. used to prevent input while the death animation is going on
	private GlobalParameters gp; //class that stores all global variables

	private bool goingToJump; //used to prevent movement
	private bool isJumping = false; //hero is jumping

	void Start()
	{
		#if !UNITY_EDITOR//makes tutorial be displayed only at the first time its played
			if(PlayerPrefs.GetInt("firstPlay") != 1){
				tutorial.SetActive(true);
			}
			PlayerPrefs.SetInt("firstPlay", 1);
		#endif

		gp = new GlobalParameters();
		halfWidthOfTheScreen = Display.main.systemWidth / 2;
		halfHeightOfTheScreen = Display.main.systemHeight / 3;
	}

	void Update(){
		if(canPlay){
			foreach(Touch touch in Input.touches){
				switch (touch.phase){
					case TouchPhase.Began:
						tutorial.SetActive(false);
						//jump
						if(touch.position.y >= halfHeightOfTheScreen && !isJumping){
							hero.jump();
							isJumping = true;
						}else if(touch.position.y < halfHeightOfTheScreen){
							hero.turnOnOffMovementAnim();
						}
						break;

					case TouchPhase.Ended:
						if(touch.position.y <= halfHeightOfTheScreen){
							hero.turnOnOffMovementAnim();
						}
						break;   
				}

				//movement
				if(touch.position.y < halfHeightOfTheScreen){
					if(touch.position.x >= halfWidthOfTheScreen){
						hero.move(1);//moving right
					}else{
						hero.move(-1);//moving left
					}
				}
			}
			//this is the variable responsible for only one time jumping and not allowing jump when falling
			isJumping = hero.triggerJumpAnim();
		}

		//always checking if its gameover
		gameOverConditions();
	}

	void gameOverConditions(){
		if(hero.transform.position.y < gp.fallDeath){ //death by fall
			canPlay = false;
			retryButton.gameObject.SetActive(true);
		}

		if(hero.deathMessenger()){ //death by contact with monster
			StartCoroutine(hero.death());
			StartCoroutine(displayReplayButton(3));
			hero.imDeath = false;
			canPlay = false;
		}
	}

	IEnumerator displayReplayButton(int waitTime){
		yield return new WaitForSeconds(waitTime);
		retryButton.gameObject.SetActive(true);
    	yield break;
	}

	IEnumerator gameOver(){
		yield return new WaitForSeconds(1);
		hero.death();
		yield return new WaitForSeconds(2);
		retryButton.gameObject.SetActive(true);
	}

	//realod the game in case of detah. called by retry button
	public void reloadGameScene(){
		SceneManager.LoadScene(thisScene);
	}
}
