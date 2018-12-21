using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {

	public Text distanceText;
	public Text scoreText;

	// Use this for initialization
	void Start () {
		distanceText.text = GameManager.distance.ToString() + " m";
		scoreText.text = GameManager.currentScore.ToString() + " pollens";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
