using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScenePresentation : MonoBehaviour
{
	public float stripeVelocity = 1;
	public float yRaise = 2.2f;
	public float yDown = 2.2f;


    void Start()
    {
        StartCoroutine(raiseStripe(transform.GetChild(0), yRaise));
    }

    IEnumerator raiseStripe(Transform stripe, float destY){
    	Vector2 destination = new Vector3(stripe.localPosition.x, destY, 1);
    	while(stripe.localPosition.y < destY){
    		stripe.localPosition = Vector3.MoveTowards(stripe.localPosition, destination, stripeVelocity * Time.deltaTime);
    		yield return null;
    	}
    }
}
