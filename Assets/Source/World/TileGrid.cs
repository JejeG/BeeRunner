using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour {

    [SerializeField]
    private float size = 5f;

    public float height = 20f;

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

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;

    //    //Vector3 meshSize = GetComponent<MeshRenderer>().bounds.size;
    //    Vector3 meshSize = new Vector3(100.0f, 50.0f, 100.0f);
    //    //for (float x = transform.localPosition.x - meshSize.x; x <= meshSize.x; x += size)
    //    for (float x = meshSize.x * -1.0f; x <= meshSize.x; x += size)
    //    {
    //        //for (float z = transform.localPosition.z - meshSize.z; z <= meshSize.z; z += size)
    //        for (float z = meshSize.z * -1.0f; z <= meshSize.z; z += size)
    //        {
    //            var point = GetNearestPointOnGrid(new Vector3(x, height, z));
    //            Gizmos.DrawSphere(point, 0.5f);
    //        }

    //    }
    //}
}
