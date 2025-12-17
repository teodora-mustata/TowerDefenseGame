using UnityEngine;

public class CoinDrop : MonoBehaviour
{
    public float lifeTime = 6f;
    public float flickerStartTime = 4f;

    private int coinValue;
    private bool collected = false;
    private Renderer rend;

    public void Init(int value)
    {
        coinValue = value;
    }

    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        Invoke(nameof(StartFlicker), flickerStartTime);
        Destroy(gameObject, lifeTime);
    }

    void OnMouseDown()
    {
        if (collected) return;

        collected = true;

        if (GameResources.Instance != null)
            GameResources.Instance.AddCoins(coinValue);

        Destroy(gameObject);
    }

    void StartFlicker()
    {
        if (rend != null)
            StartCoroutine(FlickerCoroutine());
    }

    System.Collections.IEnumerator FlickerCoroutine()
    {
        float endTime = Time.time + (lifeTime - flickerStartTime);
        while (Time.time < endTime)
        {
            rend.enabled = !rend.enabled;
            yield return new WaitForSeconds(0.15f);
        }
        rend.enabled = true;
    }
}
