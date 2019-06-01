using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameScript : MonoBehaviour {

   
	// Use this for initialization
	void Start () {
        GameObject.Find("ScoreValue").GetComponent<Text>().text = "" + (10 - Crash.collisionCounter);
        GameObject.Find("CollisionValue").GetComponent<Text>().text = "" + Crash.collisionCounter;
        GameObject.Find("TurnsMissedValue").GetComponent<Text>().text = "" + (Crash.wentOfftrackCounter);


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
