using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Button b = gameObject.GetComponent<Button>();

        if (SceneManager.GetActiveScene().name == "menu") {
        	b.onClick.AddListener(delegate () { GameManager.Instance.RestartGame(); });
        } else {
            if (GameManager.isEnd == false)
            {
                Text buttonText = b.GetComponentInChildren<Text>();
                buttonText.text = "Next level";
            }

            b.onClick.AddListener(delegate () {
        		if(GameManager.isEnd == true) {
        			GameManager.Instance.RestartGame(); 	
        		} else {
        			GameManager.Instance.IncreaseLevel();
        		}
        	});
        }
    }
}
