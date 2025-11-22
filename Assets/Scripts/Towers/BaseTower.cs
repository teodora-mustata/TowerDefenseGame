using UnityEngine;

public abstract class BaseTower : MonoBehaviour
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

    [Header("Health")]
    public float maxHealth = 100f;
    private float currentHealth;

    public GameObject projectilePrefab;

    private float nextFireTime;

    public Animator animator;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            BaseEnemy target = FindEnemyInFront();
            if (target != null)
            {
                Attack(target);
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    protected BaseEnemy FindEnemyInFront()
    {
        BaseEnemy[] enemies = FindObjectsOfType<BaseEnemy>();
        BaseEnemy best = null;
        float bestZ = Mathf.Infinity;

        foreach (var e in enemies)
        {
            if (Mathf.Abs(e.transform.position.x - transform.position.x) > 0.5f)
                continue;

            float dz = e.transform.position.z - transform.position.z;

            if (dz < 0)
                continue;

            if (dz > range)
                continue;

            if (dz < bestZ)
            {
                bestZ = dz;
                best = e;
            }
        }

        return best;
    }

    protected abstract void Attack(BaseEnemy target);
    public virtual void Die()
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
