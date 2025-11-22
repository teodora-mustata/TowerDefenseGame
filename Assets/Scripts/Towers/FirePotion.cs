using UnityEngine;

public class FirePotion : MonoBehaviour
{
    [HideInInspector] private BaseEnemy target;

    [Header("Stats")]
    public int damage = 10;
    public string damageType = "Fire";
    public float radius = 2f;

    [Header("Movement")]
    public float speed = 10f;
    public void Init(BaseEnemy t, int dmg, string type, float rad)
    {
        target = t;
        damage = dmg;
        damageType = type;
        radius = rad;
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
        foreach (var h in hits)
        {
            BaseEnemy e = h.GetComponent<BaseEnemy>();
            if (e != null)
                e.TakeDamage(damage, damageType);
        }

        Destroy(gameObject);
    }
}
