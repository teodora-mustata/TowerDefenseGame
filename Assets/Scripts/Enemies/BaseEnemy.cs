using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [Header("Stats")]
    public string enemyName = "Slime";
    public float speed = 2f;
    public int health = 100;
    public float damageToTower = 10f;
    public string type = "Normal";

    [HideInInspector] public Transform laneTarget;

    void Update()
    {
        if (laneTarget == null) return;

        Vector3 targetPos = new Vector3(
            transform.position.x - speed * Time.deltaTime,
            laneTarget.position.y,
            transform.position.z
        );

        transform.position = targetPos;
    }

    public void TakeDamage(int dmg, string dmgType)
    {
        if (type == "Plant" && dmgType == "Fire") dmg *= 2;
        if (type == "Fire" && dmgType == "Ice") dmg *= 2;
        if (type == "Fire" && dmgType == "Fire") dmg = Mathf.RoundToInt(dmg * 0.5f);

        health -= dmg;
        if (health <= 0) Destroy(gameObject);
    }
    private void OnTriggerStay(Collider other)
    {
        BaseTower tower = other.GetComponent<BaseTower>();
        if (tower != null)
        {
            tower.TakeDamage(damageToTower * Time.deltaTime);
        }
    }
}
