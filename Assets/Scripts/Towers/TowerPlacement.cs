using UnityEngine;
public class TowerPlacement : MonoBehaviour
{
    public static TowerPlacement Instance;

    [Header("Tower Settings")]
    public GameObject[] towerPrefabs;
    public int[] towerCosts;

    private GameObject selectedTower;
    private int selectedCost;

    void Awake()
    {
        Instance = this;
    }

    public void SelectTower(int towerIndex)
    {
        selectedTower = towerPrefabs[towerIndex];
        selectedCost = towerCosts[towerIndex];
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            selectedTower = null;
            return;
        }

        if (Input.GetMouseButtonDown(0) && selectedTower != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 200f))
            {
                GridTile tile = hit.collider.GetComponent<GridTile>();

                if (tile != null && tile.isEmpty)
                {
                    if (GameResources.Instance.SpendCoins(selectedCost))
                    {
                        GameObject newTower = Instantiate(selectedTower, tile.transform.position, Quaternion.identity);

                        BaseTower tower = newTower.GetComponent<BaseTower>();
                        tower.placedTile = tile;

                        tile.isEmpty = false;
                        tile.currentTower = tower;

                    }
                    else
                    {
                        Debug.Log("Not enough coins!");
                    }
                }

                selectedTower = null;
            }
        }
    }

    public bool CanPlaceOnTile(GridTile tile)
    {
        if (selectedTower == null) return false;
        if (tile == null || !tile.isEmpty) return false;

        if (GameResources.Instance != null &&
            GameResources.Instance.CurrentCoins < selectedCost)
            return false;

        return true;
    }

}