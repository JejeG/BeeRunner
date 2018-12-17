using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour {

    private CharacterController controller;

    private Vector3 moveVector;

    [SerializeField]
    private float speed = 5.0f;

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
}
