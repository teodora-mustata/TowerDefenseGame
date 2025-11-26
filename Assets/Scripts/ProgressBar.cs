using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image fillImage;
    public EnemySpawner spawner;

    private float elapsedTime = 0f;

    void Update()
    {
        if (spawner == null) return;

        elapsedTime += Time.deltaTime;
        float progress = Mathf.Clamp01(elapsedTime / spawner.levelDuration);
        fillImage.fillAmount = progress;
    }
}
