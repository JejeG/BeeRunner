using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesManager : MonoBehaviour {

    // Array of available tiles prefabs
    public GameObject[] tilePrefabs;
    public GameObject[] tileProps;

    public int maxProps = 3;

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
        lastTileIndex = randomIndex;

        // /!\ TODO : Avoid to spawn the same tile as previous tile /!\

        GameObject ground;

        ground = Instantiate(tilePrefabs[randomIndex]) as GameObject;
        ground.transform.SetParent(transform);
        ground.transform.position = Vector3.forward * spawnZ;

        for(int i = 0; i < maxProps; i++)
        {

            Vector3 meshSize = ground.GetComponent<MeshRenderer>().bounds.size;

            TileGrid grid = ground.GetComponent<TileGrid>();

            Vector3 randomPosition = new Vector3(Random.Range(meshSize.x / 2 * -1, meshSize.x / 2), grid.height, Random.Range(spawnZ - meshSize.z / 2, spawnZ + meshSize.z / 2));

            RaycastHit hitInfo;
            Ray ray = new Ray();
            ray.origin = randomPosition;
            ray.direction = ground.transform.up * -1;

            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform.tag == "Ground") {
                    GameObject randomProp;
                    int randomPropsIndex = Random.Range(0, tileProps.Length);

                    randomProp = Instantiate(tileProps[randomPropsIndex]) as GameObject;

                    randomProp.transform.SetParent(ground.transform);

                   Vector3 calculatedPosition = new Vector3(randomPosition.x, ground.transform.position.y + randomProp.GetComponent<MeshRenderer>().bounds.size.y / 2, randomPosition.z);

                    var finalPosition = grid.GetNearestPointOnGrid(calculatedPosition);
                    randomProp.transform.position = finalPosition;
                }
            }
        }

        spawnZ += tileLength;
        activeTiles.Add(ground);
    }

    // Destroy the oldest tile
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
