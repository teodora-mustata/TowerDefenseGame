using UnityEngine;

public class EveTower : BaseTower
{
    public float explosionRadius = 2f;
    public Transform firePoint;

    protected override void Attack(BaseEnemy target)
    {
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        FirePotion projScript = proj.GetComponent<FirePotion>();

        projScript.Init(target, damage, damageType, explosionRadius);

        if (animator != null)
            animator.SetTrigger("ShootTrigger");
    }
}
