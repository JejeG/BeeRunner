using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Button b = gameObject.GetComponent<Button>();
        b.onClick.AddListener(delegate () { GameManager.Instance.RestartGame(); });
    }
}
