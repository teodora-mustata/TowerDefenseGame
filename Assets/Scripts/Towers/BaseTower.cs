using UnityEngine;

public class BaseTower : MonoBehaviour
{
    [Header("Stats")]
    public string towerName = "Basic Tower";
    public float range = 5f;
    public float fireRate = 1f;
    public int damage = 10;

    [Header("Damage Type")]
    public string damageType = "Normal";

    //[Header("Upgrade")]
    //public int upgradeLevel = 0;
    //public TowerUpgrade[] upgrades;
    //public int maxUpgrades = 2;
    //public int upgradesApplied = 0;

    public GameObject projectilePrefab;

    private float nextFireTime;

    public Animator animator;

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            BaseEnemy target = FindClosestEnemy();
            if (target != null)
            {
                Shoot(target);
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    BaseEnemy FindClosestEnemy()
    {
        BaseEnemy[] enemies = FindObjectsOfType<BaseEnemy>();
        float closestDist = range;
        BaseEnemy closest = null;

        foreach (BaseEnemy e in enemies)
        {
            float dist = Vector3.Distance(transform.position, e.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closest = e;
            }
        }
        return closest;
    }

    void Shoot(BaseEnemy target)
    {
        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        proj.GetComponent<Projectile>().Init(target, damage, damageType);

        if (animator != null)
            animator.SetTrigger("ShootTrigger");
    }

    public void Die()
    {
        if (animator != null)
            animator.SetTrigger("DieTrigger");

        Destroy(gameObject, 5f);
    }


    //public void ApplyUpgrade(int upgradeIndex)
    //{
    //    if (upgradesApplied >= maxUpgrades) return;
    //    if (upgradeIndex < 0 || upgradeIndex >= upgrades.Length) return;
    //    TowerUpgrade up = upgrades[upgradeIndex];

    //    if (up.applied) return;
    //    damage = up.newDamage != 0 ? up.newDamage : damage;
    //    fireRate = up.newFireRate != 0 ? up.newFireRate : fireRate;
    //    damageType = !string.IsNullOrEmpty(up.newDamageType) ? up.newDamageType : damageType;
    //    towerName = !string.IsNullOrEmpty(up.newName) ? up.newName : towerName;

    //    up.applied = true;
    //    upgradesApplied++;
    //}
}
