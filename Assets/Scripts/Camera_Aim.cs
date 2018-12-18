using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class Camera_Aim : MonoBehaviour {
    public GameObject _GOTarget;

    // Use this for initialization
    void Start () {
        _GOTarget = GameObject.Find("_targetCam");
    }
	
	// Update is called once per frame

    void Update()
    {
        // Rotate the camera every frame so it keeps looking at the target 
        transform.LookAt(_GOTarget.transform);
    }
}
