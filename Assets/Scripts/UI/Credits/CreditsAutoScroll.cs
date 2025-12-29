using UnityEngine;
using UnityEngine.UI;

public class CreditsAutoScroll : MonoBehaviour
{
    public float scrollSpeed = 20f;
    private ScrollRect scrollRect;

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        scrollRect.verticalNormalizedPosition = 1f;  
    }

    private void Update()
    {
        scrollRect.verticalNormalizedPosition -= scrollSpeed * Time.deltaTime / 100f;
    }
}
