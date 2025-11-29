using UnityEngine;

public class GameFlow : MonoBehaviour
{
    public static GameFlow Instance;

    public bool isFrozen = false;

    private void Awake()
    {
        Instance = this;
    }

    public void FreezeEverything()
    {
        isFrozen = true;

        EnemySpawner spawner = FindFirstObjectByType<EnemySpawner>();
        if (spawner != null)
            spawner.enabled = false;

        BaseEnemy[] enemies = FindObjectsByType<BaseEnemy>(FindObjectsSortMode.None);
        foreach (var e in enemies)
        {
            e.Freeze();
        }

        BaseTower[] towers = FindObjectsByType<BaseTower>(FindObjectsSortMode.None);
        foreach (var t in towers)
        {
            t.enabled = false;
        }
    }

    public void TriggerGameOver()
    {
        Debug.Log("GAME OVER!");
        GameOverUI.Instance.Show();
    }
}
