using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;


public class TextPresentation : MonoBehaviour
{
	public TextMeshProUGUI textDisplay;

	[Header("Text presentation")]
	public float timeToDisplay = 3f;
	public string[] displayTexts;

	[HideInInspector]
	public bool textPresentationEnd = false;

    void Start(){
		StartCoroutine(displayText(displayTexts));
    }

    void Update(){
    	if(textPresentationEnd){
			SceneManager.LoadScene("Game");
    	}
    }

    public IEnumerator displayText(string[] texts){
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
    	textPresentationEnd = true;
    }
}

