using UnityEngine;

public class CoinDrop : MonoBehaviour
{
    public int coinValue = 10;
    public float lifeTime = 6f;           // cât timp st? pe hart?
    public float flickerStartTime = 4f;   // când începe s? pâlpâie

    private bool collected = false;
    private Renderer rend;

    void Start()
    {
        rend = GetComponentInChildren<Renderer>();

        // începe s? pâlpâie înainte s? dispar?
        Invoke(nameof(StartFlicker), flickerStartTime);

        // dispare singur? dac? nu e colectat?
        Destroy(gameObject, lifeTime);
    }

    void OnMouseDown()
    {
        if (collected) return;

        collected = true;

        // ad?ug?m monede prin GameResources
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
