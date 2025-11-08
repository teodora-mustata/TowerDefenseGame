using UnityEngine;

public class Projectile : MonoBehaviour
{
    private BaseEnemy target;
    private int damage;
    private string damageType;

    public float speed = 10f;

    public void Init(BaseEnemy enemy, int dmg, string dmgT)
    {
        target = enemy;
        damage = dmg;
        damageType = dmgT;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.transform.position) < 0.2f)
        {
            target.TakeDamage(damage, damageType);
            Destroy(gameObject);
        }
    }
}
