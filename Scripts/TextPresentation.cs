using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;


public class TextPresentation : MonoBehaviour
{
	public TextMeshProUGUI textDisplay;

	public float timeToDisplay = 3f;
	public string[] displayTexts;


	private bool changeScene = false;

    void Start(){
		StartCoroutine(displayText(displayTexts));
    }

    void Update(){
    	if(changeScene){
			SceneManager.LoadScene("Game");
    	}
    }

    IEnumerator displayText(string[] texts){
    	Color tempColor = textDisplay.color;
    	tempColor.a = 0;
    	tempColor.r = 100;

    	for (int i = 0; i < texts.Length; i++){
	    	textDisplay.text = texts[i];

	    	while(tempColor.a < 1){
		    	textDisplay.color = tempColor;
	    		tempColor.a += .01f;
	    		yield return null;
	    	}

			yield return new WaitForSeconds(timeToDisplay);

			while(tempColor.a > 0){
		    	textDisplay.color = tempColor;
	    		tempColor.a -= .01f;
	    		yield return null;
	    	}
    	}
    	changeScene = true;
    }
}

