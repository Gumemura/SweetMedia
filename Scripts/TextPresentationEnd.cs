using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 
using UnityEngine.Networking;

public class TextPresentationEnd : TextPresentation
{
	//better enum
	[HideInInspector]
	public string userName;
	[HideInInspector]
	public string userEmail;
	[HideInInspector]
	public string userBirthDay;

	[Header("Forms and Button")]
	public GameObject forms;
	public GameObject retryButton;

	[Header("Input fields")]
	public TMP_InputField nameInputField;
	public TMP_InputField emailInputField;
	public TMP_InputField birthInputField;

	private TouchScreenKeyboard keyboard;
	private string meTheAutor = "guilherme";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(displayText(displayTexts));
    }

    // Update is called once per frame
    void Update()
    {
        if(textPresentationEnd){
        	textDisplay.text = "";
        	forms.SetActive(true);
        	textPresentationEnd = false;
        }
    }

    public void openKeyboard(){
    	keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);	
    }

    public void closeKeyboard(){
    	keyboard.active = false;
    }

    public void setName(){
    	userName = nameInputField.text;
    }

    public void setEmail(){
    	userEmail = emailInputField.text;
    }

    public void setBirth(){
    	userBirthDay = birthInputField.text;
    }

    public void done(){
    	forms.SetActive(false);
    	StartCoroutine(Upload());
    	//send to api
    	retryButton.SetActive(true);
    	StartCoroutine(Upload());
    }

    public void retry(){
		SceneManager.LoadScene("Game");
    }

    IEnumerator Upload(){
        WWWForm form = new WWWForm();
        form.AddField("candidate", meTheAutor);
        form.AddField("fullname", userName);
        form.AddField("email", userEmail);
        form.AddField("birthdate", userBirthDay);

        using (UnityWebRequest www = UnityWebRequest.Post("https://sweetbonus.com.br/sweet-juice/trainee-test/submit?", form)){
            yield return www.SendWebRequest();
        }
    }
}

