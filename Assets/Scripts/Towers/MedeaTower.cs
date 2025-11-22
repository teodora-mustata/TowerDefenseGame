using UnityEngine;

public class MedeaTower : BaseTower
{
    public float pushForce = 2f;
    public float slowAmount = 0.5f;
    public float slowDuration = 1f;

    protected override void Attack(BaseEnemy target)
    {
        RaycastHit[] hits = Physics.RaycastAll(
            transform.position,
            transform.forward,
            range
        );

        foreach (var h in hits)
        {
            BaseEnemy e = h.collider.GetComponent<BaseEnemy>();
            if (e != null)
            {
                e.transform.position += Vector3.right * pushForce;
                e.speed *= slowAmount;
            }
        }

        if (animator != null)
            animator.SetTrigger("ShootTrigger");
    }

}
