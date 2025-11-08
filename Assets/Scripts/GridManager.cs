using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int rows = 5;
    public int columns = 10;
    public float cellSize = 2f;

    public Vector3 startPosition = new Vector3(-9, 0, 0);

    public GameObject cellPrefab;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                Vector3 pos = startPosition + new Vector3(c * cellSize, 0, r * -cellSize);
                if (cellPrefab != null)
                    Instantiate(cellPrefab, pos, Quaternion.identity, transform);
            }
        }
    }
}
