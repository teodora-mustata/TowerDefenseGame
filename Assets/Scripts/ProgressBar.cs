
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image fillImage;
    public EnemySpawner spawner;

    private float elapsedTime = 0f;
    private bool triggeredWin = false;

    void Update()
    {
        if (spawner == null || triggeredWin) return;

        elapsedTime += Time.deltaTime;
        float progress = Mathf.Clamp01(elapsedTime / spawner.levelDuration);
        fillImage.fillAmount = progress;

        if (progress >= 0.1f)
        {
            triggeredWin = true;
            GameFlow.Instance.TriggerGameWin();
        }


    }
}
