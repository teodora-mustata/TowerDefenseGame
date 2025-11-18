using UnityEngine;

public class LaneGridManager : MonoBehaviour
{
    public Transform[] lanes;       
    public GameObject tilePrefab;
    public int columns = 8;
    public float spacing = 2f;

    public Vector3 buildDirection = new Vector3(0, 0, -1);  
    public float startOffset = 4f;  

    void Start()
    {
        Generate();
    }

    void Generate()
    {
        foreach (Transform lane in lanes)
        {
            Vector3 lanePos = lane.position;

            Vector3 firstTilePos = lanePos + buildDirection * startOffset;

            for (int i = 0; i < columns; i++)
            {
                Vector3 pos = firstTilePos + buildDirection * (i * spacing);

                pos.y = Terrain.activeTerrain.SampleHeight(pos)+0.2f;

                Instantiate(tilePrefab, pos, Quaternion.identity, transform);
            }
        }
    }
}
