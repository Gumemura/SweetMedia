﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other){
    	SceneManager.LoadScene("End");
 	}
}
