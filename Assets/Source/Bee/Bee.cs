using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
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
        }
    }
}
