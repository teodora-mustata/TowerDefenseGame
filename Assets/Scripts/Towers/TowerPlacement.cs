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
//        if (Input.GetMouseButtonDown(1))
//        {
//            selectedTower = null;
//            return;
//        }

//        if (Input.GetMouseButtonDown(0) && selectedTower != null)
//        {
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

//            if (Physics.Raycast(ray, out RaycastHit hit, 200f))
//            {
//                GridTile tile = hit.collider.GetComponent<GridTile>();

//                if (tile != null && tile.isEmpty)
//                {
//                    if (GameResources.Instance.SpendCoins(selectedCost))
//                    {
//                        GameObject newTower = Instantiate(selectedTower, tile.transform.position, Quaternion.identity);

//                        BaseTower tower = newTower.GetComponent<BaseTower>();
//                        tower.placedTile = tile;

//                        tile.isEmpty = false;
//                        tile.currentTower = tower;

//                    }
//                    else
//                    {
//                        Debug.Log("Not enough coins!");
//                    }
//                }

//                selectedTower = null;
//            }
//        }
//    }

//    public bool CanPlaceOnTile(GridTile tile)
//    {
//        if (selectedTower == null) return false;
//        if (tile == null || !tile.isEmpty) return false;

//        if (GameResources.Instance != null &&
//            GameResources.Instance.CurrentCoins < selectedCost)
//            return false;

//        return true;
//    }

//}


//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.EventSystems;

//public class TowerPlacement : MonoBehaviour
//{
//    public static TowerPlacement Instance;

//    [Header("Tower Settings")]
//    public GameObject[] towerPrefabs;
//    public int[] towerCosts;

//    [Header("Ghost Settings (UI Overlay)")]
//    public GameObject ghost2DPrefab;        
//    public Canvas targetCanvas;             
//    public float ghostScreenScale = 1.0f;   
//    public Vector2 ghostImageSize = new Vector2(100, 100);
//    public bool ignoreUIBlocking = true;    

//    private GameObject currentGhost;
//    private RectTransform ghostRect;
//    private Image ghostImage;
//    private RectTransform ghostImageRect;

//    private GameObject selectedTower;
//    private int selectedCost;

//    void Awake() => Instance = this;

//    public void SelectTower(int towerIndex)
//    {
//        selectedTower = towerPrefabs[towerIndex];
//        selectedCost = towerCosts[towerIndex];
//        CreateGhost();
//    }

//    void Update()
//    {
//        if (selectedTower == null)
//        {
//            DestroyGhost();
//            return;
//        }

//        UpdateGhostPosition();

//        if (Input.GetMouseButtonDown(1))
//        {
//            selectedTower = null;
//            DestroyGhost();
//        }

//        if (Input.GetMouseButtonDown(0))
//            TryPlaceTower();
//    }

//    void CreateGhost()
//    {
//        DestroyGhost();
//        if (ghost2DPrefab == null) return;


//        if (targetCanvas == null)
//            targetCanvas = FindObjectOfType<Canvas>();


//        if (targetCanvas != null)
//            currentGhost = Instantiate(ghost2DPrefab, targetCanvas.transform);
//        else
//            currentGhost = Instantiate(ghost2DPrefab);  

//        currentGhost.SetActive(true);

//        ghostRect = currentGhost.GetComponent<RectTransform>();
//        if (ghostRect == null)
//            ghostRect = currentGhost.GetComponentInChildren<RectTransform>();

//        ghostImage = currentGhost.GetComponentInChildren<Image>();
//        ghostImageRect = ghostImage ? ghostImage.GetComponent<RectTransform>() : null;

//        ResetRect(ghostRect);
//        ResetRect(ghostImageRect);

//        if (ghostImageRect != null)
//            ghostImageRect.sizeDelta = ghostImageSize;
//        if (ghostImage != null)
//            ghostImage.raycastTarget = false; 

//        float s = Mathf.Clamp(ghostScreenScale, 0.01f, 10f);
//        currentGhost.transform.localScale = Vector3.one * s;

//        UpdateGhostPosition(); 
//    }

//    void ResetRect(RectTransform rt)
//    {
//        if (rt == null) return;
//        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
//        rt.pivot = new Vector2(0.5f, 0.5f);
//        rt.anchoredPosition = Vector2.zero;
//        rt.localPosition = Vector3.zero;
//        rt.localRotation = Quaternion.identity;
//        rt.localScale = Vector3.one;
//    }

//    void DestroyGhost()
//    {
//        if (currentGhost != null) Destroy(currentGhost);
//        currentGhost = null;
//        ghostRect = null;
//        ghostImage = null;
//        ghostImageRect = null;
//    }

//    void UpdateGhostPosition()
//    {
//        if (ghostRect == null)
//            return;

//        if (targetCanvas != null)
//        {
//            RectTransform canvasRect = targetCanvas.GetComponent<RectTransform>();
//            if (canvasRect != null)
//            {
//                Vector2 localPoint;
//                bool isOverlay = targetCanvas.renderMode == RenderMode.ScreenSpaceOverlay;
//                Camera cam = isOverlay ? null : targetCanvas.worldCamera;
//                RectTransformUtility.ScreenPointToLocalPointInRectangle(
//                    canvasRect, Input.mousePosition, cam, out localPoint);
//                ghostRect.anchoredPosition = localPoint;
//                return;
//            }
//        }

//        ghostRect.position = Input.mousePosition;
//    }

//    void TryPlaceTower()
//    {
//        if (!ignoreUIBlocking && EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
//            return;

//        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//        if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
//        {
//            GridTile tile = hit.collider.GetComponent<GridTile>();
//            if (tile != null && CanPlaceOnTile(tile))
//            {
//                if (GameResources.Instance.SpendCoins(selectedCost))
//                {
//                    var tower = Instantiate(selectedTower, tile.transform.position, Quaternion.identity);
//                    var bt = tower.GetComponent<BaseTower>();
//                    bt.placedTile = tile;

//                    tile.isEmpty = false;
//                    tile.currentTower = bt;

//                    DestroyGhost();
//                    selectedTower = null;
//                }
//            }
//        }
//    }

//    public bool CanPlaceOnTile(GridTile tile)
//    {
//        if (selectedTower == null || tile == null || !tile.isEmpty) return false;
//        if (GameResources.Instance.CurrentCoins < selectedCost) return false;
//        return true;
//    }
//}

using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TowerPlacement : MonoBehaviour
{
    public static TowerPlacement Instance;

    [Header("Tower Settings")]
    public GameObject[] towerPrefabs;
    public int[] towerCosts;

    [Header("Ghost Settings (UI Overlay)")]
    public GameObject ghost2DPrefab;
    public Canvas targetCanvas;
    public float ghostScreenScale = 1.0f;
    public Vector2 ghostImageSize = new Vector2(100, 100);
    public bool ignoreUIBlocking = true;
    [Tooltip("Sprite-urile pentru ghost, în aceea?i ordine ca towerPrefabs.")]
    public Sprite[] towerGhostSprites;

    private GameObject currentGhost;
    private RectTransform ghostRect;
    private Image ghostImage;
    private RectTransform ghostImageRect;

    private GameObject selectedTower;
    private int selectedCost;
    private int selectedIndex = -1;

    void Awake() => Instance = this;

    public void SelectTower(int towerIndex)
    {
        selectedIndex = towerIndex;
        selectedTower = towerPrefabs[towerIndex];
        selectedCost = towerCosts[towerIndex];
        CreateGhost();
    }

    void Update()
    {
        if (selectedTower == null)
        {
            DestroyGhost();
            return;
        }

        UpdateGhostPosition();

        if (Input.GetMouseButtonDown(1))
        {
            selectedTower = null;
            selectedIndex = -1;
            DestroyGhost();
        }

        if (Input.GetMouseButtonDown(0))
            TryPlaceTower();
    }

    void CreateGhost()
    {
        DestroyGhost();
        if (ghost2DPrefab == null) return;

        // Alege un canvas valid din scen? (nu persistent)
        if (targetCanvas == null || !targetCanvas.gameObject.scene.IsValid())
        {
            targetCanvas = FindObjectsOfType<Canvas>()
                .FirstOrDefault(c => c.isActiveAndEnabled && c.gameObject.scene.IsValid());
        }

        // Instan?iaz? f?r? p?rinte persistent ?i apoi seteaz?-l pe canvasul g?sit
        currentGhost = Instantiate(ghost2DPrefab);
        if (targetCanvas != null)
            currentGhost.transform.SetParent(targetCanvas.transform, false);

        currentGhost.SetActive(true);

        ghostRect = currentGhost.GetComponent<RectTransform>();
        if (ghostRect == null)
            ghostRect = currentGhost.GetComponentInChildren<RectTransform>();

        ghostImage = currentGhost.GetComponentInChildren<Image>();
        ghostImageRect = ghostImage ? ghostImage.GetComponent<RectTransform>() : null;

        ResetRect(ghostRect);
        ResetRect(ghostImageRect);

        // Seteaz? sprite-ul corect pentru turnul selectat
        if (ghostImage != null && towerGhostSprites != null &&
            selectedIndex >= 0 && selectedIndex < towerGhostSprites.Length &&
            towerGhostSprites[selectedIndex] != null)
        {
            ghostImage.sprite = towerGhostSprites[selectedIndex];
            ghostImage.preserveAspect = true;
        }

        if (ghostImageRect != null)
            ghostImageRect.sizeDelta = ghostImageSize;
        if (ghostImage != null)
            ghostImage.raycastTarget = false;

        float s = Mathf.Clamp(ghostScreenScale, 0.01f, 10f);
        currentGhost.transform.localScale = Vector3.one * s;

        UpdateGhostPosition();
    }

    void ResetRect(RectTransform rt)
    {
        if (rt == null) return;
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = Vector2.zero;
        rt.localPosition = Vector3.zero;
        rt.localRotation = Quaternion.identity;
        rt.localScale = Vector3.one;
    }

    void DestroyGhost()
    {
        if (currentGhost != null) Destroy(currentGhost);
        currentGhost = null;
        ghostRect = null;
        ghostImage = null;
        ghostImageRect = null;
    }

    void UpdateGhostPosition()
    {
        if (ghostRect == null) return;

        if (targetCanvas != null)
        {
            RectTransform canvasRect = targetCanvas.GetComponent<RectTransform>();
            if (canvasRect != null)
            {
                Vector2 localPoint;
                bool isOverlay = targetCanvas.renderMode == RenderMode.ScreenSpaceOverlay;
                Camera cam = isOverlay ? null : targetCanvas.worldCamera;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    canvasRect, Input.mousePosition, cam, out localPoint);
                ghostRect.anchoredPosition = localPoint;
                return;
            }
        }

        ghostRect.position = Input.mousePosition;
    }

    void TryPlaceTower()
    {
        if (!ignoreUIBlocking && EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
        {
            GridTile tile = hit.collider.GetComponent<GridTile>();
            if (tile != null && CanPlaceOnTile(tile))
            {
                if (GameResources.Instance.SpendCoins(selectedCost))
                {
                    var tower = Instantiate(selectedTower, tile.transform.position, Quaternion.identity);
                    var bt = tower.GetComponent<BaseTower>();
                    bt.placedTile = tile;

                    tile.isEmpty = false;
                    tile.currentTower = bt;

                    DestroyGhost();
                    selectedTower = null;
                    selectedIndex = -1;
                }
            }
        }
    }

    public bool CanPlaceOnTile(GridTile tile)
    {
        if (selectedTower == null || tile == null || !tile.isEmpty) return false;
        if (GameResources.Instance.CurrentCoins < selectedCost) return false;
        return true;
    }
}