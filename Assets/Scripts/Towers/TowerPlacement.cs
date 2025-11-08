using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    public GameObject[] towerPrefabs;
    private GameObject towerToPlace;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Cell"))
                {
                    if (towerToPlace != null)
                        Instantiate(towerToPlace, hit.collider.transform.position, Quaternion.identity);
                }
            }
        }
    }

    public void SelectTower(int index)
    {
        towerToPlace = towerPrefabs[index];
    }
}
