using UnityEngine;

public class GridTile : MonoBehaviour
{
    public bool isEmpty = true;

    [Header("Highlight colors")]
    public Color normalColor = new Color(1f, 1f, 1f, 0.2f);
    public Color canPlaceColor = new Color(0.7f, 1f, 0.7f, 0.2f);
    public Color cannotPlaceColor = new Color(1f, 0.6f, 0.6f, 0.2f);

    private Renderer rend;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material.SetFloat("_Mode", 3); // 3 = Transparent
            rend.material.EnableKeyword("_ALPHABLEND_ON");
            rend.material.renderQueue = 3000;

            rend.material.color = normalColor;
        }
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
