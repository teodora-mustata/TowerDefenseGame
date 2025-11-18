//using UnityEngine;

//public class GridTile : MonoBehaviour
//{
//    public bool isEmpty = true;
//}


using UnityEngine;

public class GridTile : MonoBehaviour
{
    public bool isEmpty = true;

    [Header("Culori highlight")]
    public Color normalColor = Color.white;
    public Color canPlaceColor = new Color(0.7f, 1f, 0.7f);  
    public Color cannotPlaceColor = new Color(1f, 0.6f, 0.6f);  

    private Renderer rend;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
            rend.material.color = normalColor;
    }

    void OnMouseEnter()
    {
        if (rend == null) return;

        if (TowerPlacement.Instance != null &&
            TowerPlacement.Instance.CanPlaceOnTile(this))
        {
            rend.material.color = canPlaceColor;
        }
        else
        {
            rend.material.color = cannotPlaceColor;
        }
    }

    void OnMouseExit()
    {
        if (rend == null) return;
        rend.material.color = normalColor;
    }
}
