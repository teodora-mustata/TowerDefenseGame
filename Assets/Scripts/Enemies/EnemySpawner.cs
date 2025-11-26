using UnityEngine;

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

    private float timer;

    void Update()
    {
        SpawnPhase phase = GetPhaseForTime(Time.time);

        timer += Time.deltaTime;
        if (timer >= phase.spawnInterval)
        {
            SpawnEnemy();
            timer = 0;
        }
    }

    void SpawnEnemy()
    {
        Transform lane = lanes[Random.Range(0, lanes.Length)];
        EnemySpawnEntry chosen = GetEnemyForCurrentTime(Time.time);

        Vector3 spawnPos = lane.position;
        spawnPos.y += chosen.enemyPrefab.transform.position.y;

        GameObject enemy = Instantiate(chosen.enemyPrefab, spawnPos, chosen.enemyPrefab.transform.rotation);

        BaseEnemy e = enemy.GetComponent<BaseEnemy>();
        e.laneTarget = lane;
    }

    SpawnPhase GetPhaseForTime(float time)
    {
        SpawnPhase active = phases[0];

        foreach (var p in phases)
            if (time >= p.startTime)
                active = p;

        return active;
    }

    EnemySpawnEntry GetEnemyForCurrentTime(float time)
    {
        SpawnPhase active = GetPhaseForTime(time);

        float total = 0;
        foreach (var e in active.enemies)
            total += e.probability;

        float rand = Random.value * total;
        float cumulative = 0;

        foreach (var e in active.enemies)
        {
            cumulative += e.probability;
            if (rand <= cumulative)
                return e;
        }

        return active.enemies[0];
    }
}

