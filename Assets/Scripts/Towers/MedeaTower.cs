using UnityEngine;

public class MedeaTower : BaseTower
{
    public float pushForce = 2f;
    public float slowAmount = 0.5f;
    public float slowDuration = 1f;

    protected override void Attack(BaseEnemy target)
    {
        Vector3 boxCenter = transform.position + transform.forward * (range / 2f);
        Vector3 boxSize = new Vector3(1f, 1f, range / 2f);

        Collider[] hits = Physics.OverlapBox(
            boxCenter,
            boxSize,
            transform.rotation
        );

        foreach (var h in hits)
        {
            BaseEnemy e = h.GetComponent<BaseEnemy>();
            if (e != null)
            {
                e.transform.position += transform.forward * pushForce;
                e.speed *= slowAmount;
            }
        }

        if (animator != null)
            animator.SetTrigger("ShootTrigger");
    }

}
