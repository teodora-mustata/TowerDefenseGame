using UnityEngine;

public class MariaTower : BaseTower
{
    protected override void Attack(BaseEnemy target)
    {
        Collider[] hits = Physics.OverlapBox(
            transform.position + transform.forward * range,
            new Vector3(1.5f, 1f, 0.5f),
            transform.rotation
        );

        foreach (var h in hits)
        {
            BaseEnemy enemy = h.GetComponent<BaseEnemy>();
            if (enemy != null)
                enemy.TakeDamage(damage, damageType);
        }

        if (animator != null)
            animator.SetTrigger("ShootTrigger");
    }
}
