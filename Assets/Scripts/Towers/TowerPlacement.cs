//using UnityEngine;

//public class TowerPlacement : MonoBehaviour
//{
//    public GameObject[] towerPrefabs;
//    private GameObject towerToPlace;

//    void Update()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            if (Physics.Raycast(ray, out RaycastHit hit))
//            {
//                if (hit.collider.CompareTag("Cell"))
//                {
//                    if (towerToPlace != null)
//                        Instantiate(towerToPlace, hit.collider.transform.position, Quaternion.identity);
//                }
//            }
//        }
//    }

//    public void SelectTower(int index)
//    {
//        towerToPlace = towerPrefabs[index];
//    }
//}

//v1
//using UnityEngine;

//public class TowerPlacement : MonoBehaviour
//{
//    public static TowerPlacement Instance;

//    [Header("Tower Settings")]
//    public GameObject[] towerPrefabs;
//    public int[] towerCosts;

//    private GameObject selectedTower;
//    private int selectedCost;

//    void Awake()
//    {
//        Instance = this;
//    }

//    public void SelectTower(int towerIndex)
//    {
//        selectedTower = towerPrefabs[towerIndex];
//        selectedCost = towerCosts[towerIndex];
//    }

//    void Update()
//    {
//        if (Input.GetMouseButtonDown(0) && selectedTower != null)
//        {
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

//            if (Physics.Raycast(ray, out RaycastHit hit, 200f, LayerMask.GetMask("Default")))
//            {

//                GridTile tile = hit.collider.GetComponent<GridTile>();
//                Debug.Log("Raycast HIT: " + hit.collider.name);
//                Debug.Log("Has GridTile script? " + hit.collider.GetComponent<GridTile>());
//                Debug.Log("isEmpty = " + tile.isEmpty);

//                if (tile != null && tile.isEmpty)
//                {
//                    if (GameResources.Instance.SpendCoins(selectedCost))
//                    {
//                        Instantiate(selectedTower, tile.transform.position, Quaternion.identity);
//                        tile.isEmpty = false;
//                    }
//                    else
//                        Debug.Log("Not enough coins!");
//                }

//                selectedTower = null;
//            }

//        }
//    }

//}


//v2
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
                        Instantiate(selectedTower, tile.transform.position, Quaternion.identity);
                        tile.isEmpty = false;
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