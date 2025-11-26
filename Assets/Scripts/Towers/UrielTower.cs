using UnityEngine;

public class UrielTower : BaseTower
{
    public float explosionRadius = 2f;
    public int explosionDamage = 50;

    protected override void Attack(BaseEnemy target)
    {
        target.TakeDamage(damage, damageType);

        if (animator != null)
            animator.SetTrigger("ShootTrigger");
    }

    public override void Die()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (var c in enemies)
        {
            BaseEnemy e = c.GetComponent<BaseEnemy>();
            if (e != null)
                e.TakeDamage(explosionDamage, damageType);
        }

        if (animator != null)
            animator.SetTrigger("DieTrigger");

        if (placedTile != null)
        {
            placedTile.isEmpty = true;
            placedTile.currentTower = null;
        }

        Destroy(gameObject, 3f);
    }
}
