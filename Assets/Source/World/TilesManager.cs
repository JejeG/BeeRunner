using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesManager : MonoBehaviour {

    public GameObject[] tilePrefabs;

    private Transform playerTransform;
    private float spawnZ = -10.0f;
    private float safeZone = 15.0f;

    [SerializeField]
    private float tileLength = 10.0f;
    [SerializeField]
    private int totalTilesOnScreen = 7;

    private List<GameObject> activeTiles;

	// Use this for initialization
	void Start () {
        activeTiles = new List<GameObject>();

		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        for(int i = 0; i < totalTilesOnScreen; i++)
        {
            SpawnTile();
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(playerTransform.position.z - safeZone > (spawnZ - totalTilesOnScreen * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
	}

    private void SpawnTile()
    {
        GameObject go;
        go = Instantiate(tilePrefabs[0]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(go);
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
