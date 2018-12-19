using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bee : MonoBehaviour {

    private CharacterController controller;

    private Vector3 moveVector;

    // Move speed
    [SerializeField]
    private float speed = 0f;

    private float maxSpeed = 24.0f;
    private float acceleration = 2.0f;


    // Pollen collected
    [SerializeField]
    private int _pollen = 0;
    public int pollen { get { return _pollen; } }

    [SerializeField]
    private bool _isDead = false;
    public bool isDead { get { return _isDead; } }

    public Text scoreText;


    /*******
     * MOBILE INPUTS
     *******/
    public Swipe swipeControls;

    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
        scoreText.text = "0 / " + GameManager.Instance.getScoreToReach().ToString();
	}
	
	// Update is called once per frame
	void FixedUpdate() {
        if (isDead == true)
        {
            swipeControls.canSwipe = false;
            GameManager.Instance.RestartGame();
            return;
        }

        swipeControls.canSwipe = true;

        if (speed < maxSpeed)
        {
            speed += acceleration * Time.deltaTime;
            if(speed > maxSpeed)
            {
                speed = maxSpeed;
            }
        }

        moveVector = Vector3.zero;

        if(swipeControls.swipeLeft || swipeControls.swipeRight)
        {
            moveVector.x = swipeControls.swipeDelta.x / speed;
            if(transform.position.x <= -10 && moveVector.x < 0 || transform.position.x >= 10 && moveVector.x > 0)
            {
                moveVector.x = 0;
            }
        }
       
        if(swipeControls.swipeUp || swipeControls.swipeDown)
        {
            moveVector.y = swipeControls.swipeDelta.y / speed;

            if (transform.position.y <= 1 && moveVector.y < 0 || transform.position.y >= 10 && moveVector.y > 0)
            {
                moveVector.y = 0;
            }
        }

        //moveVector.y = Mathf.SmoothDamp(transform.position.y, Mathf.Clamp(transform.position.y + currentdY, (_NEAR_GROUND_ALT / 10f), _Y_LIMIT_MAX), ref velocityY, 1f, 30f);

        //if ((transform.position.x <= -10 && Input.GetAxis("Horizontal") < 0) || (transform.position.x >= 10 && Input.GetAxis("Horizontal") > 0))
        //{
        //    moveVector.x = 0;
        //}
        //else
        //{
        //    moveVector.x = Input.GetAxis("Horizontal") * speed;
        //}

        //if ((transform.position.y <= 0 && Input.GetAxis("Vertical") < 0) || (transform.position.y >= 10 && Input.GetAxis("Vertical") > 0))
        //{
        //    moveVector.y = 0;
        //}
        //else
        //{
        //    moveVector.y = Input.GetAxis("Vertical") * speed;
        //}


        moveVector.z = speed;

        controller.Move(moveVector * Time.deltaTime);
	}

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Flower")
        {
            _pollen++;

            if(_pollen >= GameManager.Instance.getScoreToReach())
            {
                Debug.Log("YOU WIN ! NEXT LEVEL !");
                swipeControls.canSwipe = false;
                GameManager.instance.IncreaseLevel();
            }

        } else if(other.tag == "Obstacle")
        {
            _pollen--;

            speed = speed / 2;

            if(_pollen < 0)
            {
                Died();
                return;
            }
        }

        scoreText.text = (_pollen).ToString()+" / " + GameManager.Instance.getScoreToReach().ToString();;
    }

    public void Died()
    {
        Debug.Log("DIED !");
        _isDead = true;
        swipeControls.canSwipe = false;
    }
}
