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

    private float _deadZone = 0.001f;

    private float _delay = 3.0f;

    // Use this for initialization
    void Start () {
        startOffset = transform.position - lookAt.position;
        transform.LookAt(lookAt.transform);
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    private void LateUpdate()
    {
        // x cam = x bee - x cam * time
        float newPositionX = (lookAt.transform.position.x - transform.position.x) * Time.deltaTime * _delay;
        float newPositionY = (lookAt.transform.position.y + startOffset.y - transform.position.y) * Time.deltaTime * _delay;

        if(Mathf.Abs(transform.position.x - newPositionX) < _deadZone || Mathf.Abs(transform.position.y - newPositionY) < _deadZone)
        {
            newPositionX = 0;
            newPositionY = 0;
        }

        transform.position = new Vector3(transform.position.x + newPositionX, transform.position.y + newPositionY, lookAt.position.z + startOffset.z);
        transform.LookAt(lookAt.transform);
    }
}
