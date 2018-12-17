using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeCamera : MonoBehaviour {

    // Player transform to follow
    [SerializeField]
    private Transform lookAt;

    // Offset from player transform at start
    [SerializeField]
    private Vector3 startOffset;

    // Use this for initialization
    void Start () {
        lookAt = GameObject.FindGameObjectWithTag("Player").transform;
        startOffset = transform.position - lookAt.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, transform.position.y, lookAt.position.z + startOffset.z);
    }
}
