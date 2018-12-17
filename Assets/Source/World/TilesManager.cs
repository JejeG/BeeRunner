using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesManager : MonoBehaviour {

    // Array of available tiles prefabs
    public GameObject[] tilePrefabs;

    // Player transform
    private Transform playerTransform;

    // Z Spawn position (starting behind the player)
    private float spawnZ = -10.0f;

    // Safezone behind the player where tiles won't be deleted
    private float safeZone = 15.0f;

    // Tile length
    [SerializeField]
    private float tileLength = 10.0f;

    // Max tiles to display/keep alive
    [SerializeField]
    private int totalTilesOnScreen = 7;

    [SerializeField]
    private int lastTileIndex = -1;

    // List of active tiles
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

    // Create a new tile above the player
    private void SpawnTile()
    {

        int randomIndex = Random.Range(0, tilePrefabs.Length);
        Debug.Log(randomIndex);
        lastTileIndex = randomIndex;

        // /!\ TODO : Avoid to spawn the same tile as previous tile /!\

        GameObject go;

        go = Instantiate(tilePrefabs[randomIndex]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(go);
    }

    // Destroy the oldest tile
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
