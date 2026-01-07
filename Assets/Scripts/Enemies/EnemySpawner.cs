using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EnemySpawnEntry
{
    public GameObject enemyPrefab;
    public float probability;
}

[System.Serializable]
public class SpawnPhase
{
    public float startTime;
    public float spawnInterval;
    public EnemySpawnEntry[] enemies;
}


public class EnemySpawner : MonoBehaviour
{
    public SpawnPhase[] phases;
    public Transform[] lanes;

    public float levelDuration = 60f;

    private float levelTime;
    private float timer;

    public UnityEvent<float> OnProgressChanged;

    void OnEnable()
    {
        levelTime = 0f;
        timer = 0f;
    }

    void Update()
    {
        levelTime += Time.deltaTime;

        float progress = Mathf.Clamp01(levelTime / levelDuration);
        OnProgressChanged?.Invoke(progress);

        SpawnPhase phase = GetPhaseForTime(levelTime);

        timer += Time.deltaTime;
        if (timer >= phase.spawnInterval)
        {
            SpawnEnemy(phase);
            timer = 0f;
        }
    }

    void SpawnEnemy(SpawnPhase phase)
    {
        if (phase.enemies.Length == 0 || lanes.Length == 0)
            return;

        Transform lane = lanes[Random.Range(0, lanes.Length)];
        EnemySpawnEntry chosen = GetEnemyForPhase(phase);

        Vector3 spawnPos = lane.position;
        spawnPos.y += chosen.enemyPrefab.transform.position.y;

        GameObject enemy = Instantiate(
            chosen.enemyPrefab,
            spawnPos,
            chosen.enemyPrefab.transform.rotation
        );

        BaseEnemy e = enemy.GetComponent<BaseEnemy>();
        if (e != null)
            e.laneTarget = lane;
    }

    SpawnPhase GetPhaseForTime(float time)
    {
        SpawnPhase active = phases[0];

        foreach (var p in phases)
        {
            if (time >= p.startTime)
                active = p;
        }

        return active;
    }

    EnemySpawnEntry GetEnemyForPhase(SpawnPhase phase)
    {
        float total = 0f;
        foreach (var e in phase.enemies)
            total += e.probability;

        float rand = Random.value * total;
        float cumulative = 0f;

        foreach (var e in phase.enemies)
        {
            cumulative += e.probability;
            if (rand <= cumulative)
                return e;
        }

        return phase.enemies[0];
    }
}
