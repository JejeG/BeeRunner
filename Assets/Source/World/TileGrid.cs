using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour {

    [SerializeField]
    private float size = 1f;

    public float height = 50f;

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 result = new Vector3(
            (float)xCount * size,
            (float)yCount * size,
            (float)zCount * size);

        result += transform.position;

        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        
        Vector3 meshSize = GetComponent<MeshRenderer>().bounds.size;

        for (float x = transform.localPosition.x - meshSize.x / 2; x <= meshSize.x / 2; x += size)
        {
            for (float z = transform.localPosition.z - meshSize.z / 2; z <= meshSize.z / 2; z += size)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, height, z));
                Gizmos.DrawSphere(point, 0.1f);
            }

        }
    }
}
