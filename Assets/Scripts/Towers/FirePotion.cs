using UnityEngine;

public class FirePotion : MonoBehaviour
{
    private BaseEnemy target;
    private int damage;
    private string damageType;

    public float radius = 1f;
    public float speed = 10f;
    public void Init(BaseEnemy t, int dmg, string type)
    {
        target = t;
        damage = dmg;
        damageType = type;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.transform.position,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
        {
            Explode();
        }
    }

    void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider h in hits)
        {
            BaseEnemy e = h.GetComponent<BaseEnemy>();
            if (e != null)
            {
                e.TakeDamage(damage, damageType);
            }
        }

        Destroy(gameObject);
    }
}
