using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bee : MonoBehaviour {

    private CharacterController controller;

    private Vector3 moveVector;

    // Move speed
    [SerializeField]
    private float speed = 12.0f;

    // Pollen collected
    [SerializeField]
    private int _pollen = 0;
    public int pollen { get { return _pollen; } }

    [SerializeField]
    private bool _isDead = false;
    public bool isDead { get { return _isDead; } }

    public Text scoreText;

    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
        scoreText.text = "0";
	}
	
	// Update is called once per frame
	void Update () {
        if (isDead == true)
        {
            Restart();
            return;
        }

        moveVector = Vector3.zero;

        moveVector.x = Input.GetAxis("Horizontal") * speed;
        moveVector.y = Input.GetAxis("Vertical") * speed;
        moveVector.z = speed;

        controller.Move(moveVector * Time.deltaTime);
	}

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Flower")
        {
            _pollen++;
        } else if(other.tag == "Obstacle")
        {
            _pollen--;

            if(_pollen < 0)
            {
                Died();
                return;
            }
        }

        scoreText.text = (_pollen).ToString();
    }

    public void Died()
    {
        Debug.Log("DIED !");
        _isDead = true;
    }

    // /!\ TODO : MOVE IT SOMEWHERE ELSE + CLEAN IMPORT ABOVE /!\
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
