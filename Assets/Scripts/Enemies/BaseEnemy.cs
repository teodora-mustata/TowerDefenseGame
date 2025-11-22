using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [Header("Stats")]
    public string enemyName = "Slime";
    public float speed = 2f;
    public int health = 100;
    public float damageToTower = 10f;
    public string type = "Normal";

    [Header("References")]
    public Animator anim;

    [HideInInspector] public Transform laneTarget;

    private bool isDead = false;
    private bool isAttacking = false;
    void Update()
    {
        if (isDead || isAttacking) return;

        transform.position += Vector3.back * speed * Time.deltaTime;
    }

    public void TakeDamage(int dmg, string dmgType)
    {
        if (isDead) return;

        if (type == "Plant" && dmgType == "Fire") dmg *= 2;
        if (type == "Fire" && dmgType == "Ice") dmg *= 2;
        if (type == "Fire" && dmgType == "Fire") dmg = Mathf.RoundToInt(dmg * 0.5f);

        health -= dmg;

        if (health <= 0)
            Die();
    }

    void Die()
    {
        isDead = true;
        isAttacking = false;

        speed = 0;

        if (anim != null)
            anim.SetTrigger("DieTrigger");

        Destroy(gameObject, 5f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (isDead) return;

        BaseTower tower = other.GetComponent<BaseTower>();
        if (tower != null)
        {
            if (!isAttacking)
            {
                isAttacking = true;

                if (anim != null)
                    anim.SetBool("isAttacking", true);
            }

            tower.TakeDamage(damageToTower * Time.deltaTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        BaseTower tower = other.GetComponent<BaseTower>();
        if (tower != null && !isDead)
        {
            isAttacking = false;

            if (anim != null)
                anim.SetBool("isAttacking", false);
        }
    }

}
