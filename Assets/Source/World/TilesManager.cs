using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesManager : MonoBehaviour {

    // Array of available tiles prefabs
    public GameObject[] tilePrefabs;
    public GameObject[] tileProps;

    public int maxProps = 20;

    // Player transform
    private Transform playerTransform;

    // Z Spawn position (starting behind the player)
    private float spawnZ = -200.0f;

    // Safezone behind the player where tiles won't be deleted
    private float safeZone = 350.0f;

    // Tile length
    [SerializeField]
    private float tileLength = 200.0f;

    // Max tiles to display/keep alive
    [SerializeField]
    private int totalTilesOnScreen = 3;

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

        int randomIndex = 0;

        for (int i = 0; i < tilePrefabs.Length; i++)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);

            if (randomIndex != lastTileIndex)
            {
                lastTileIndex = randomIndex;
                break;
            }
        }

        GameObject ground;
        ground = Instantiate(tilePrefabs[randomIndex]) as GameObject;
        ground.transform.SetParent(transform);
        ground.transform.position = new Vector3(-100.0f, 0.0f, spawnZ);

        for(int i = 0; i < maxProps; i++)
        {
            // 100 * 100 * 50
            Vector3 meshSize = new Vector3(100.0f, 100.0f, tileLength);

            TileGrid grid = ground.GetComponent<TileGrid>();

            Vector3 randomPosition = new Vector3(Random.Range(meshSize.x * -1, meshSize.x), grid.height, Random.Range(spawnZ, spawnZ + meshSize.z));
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

                    Vector3 calculatedPosition = new Vector3(randomPosition.x, hitInfo.transform.GetComponent<Terrain>().SampleHeight(randomPosition), randomPosition.z);
                    var finalPosition = grid.GetNearestPointOnGrid(calculatedPosition);
                    randomProp.transform.position = finalPosition;
                    randomProp.transform.Rotate(new Vector3(0,1,0) * Random.Range(0.0f, 360.0f));
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
