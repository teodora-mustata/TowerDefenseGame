using UnityEngine;

public class ErikaTower : BaseTower
{
    public Transform firePoint;
    protected override void Attack(BaseEnemy target)
    {
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        proj.GetComponent<Projectile>().Init(target, damage, damageType);

        if (animator != null)
            animator.SetTrigger("ShootTrigger");
    }
}
