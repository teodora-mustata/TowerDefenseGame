using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [Header("Stats")]
    public string enemyName = "Slime";
    public float speed = 2f;
    public int health = 100;
    public float damageToTower = 10f;
    public string type = "Normal";

    public GameObject coinDropPrefab;
    public int coinReward = 10;


    [Header("References")]
    public Animator anim;

    [HideInInspector] public Transform laneTarget;

    private bool isDead = false;
    private bool isAttacking = false;

    private bool isFrozen = false;
    private bool enteringBase = false;

    private bool beingPulled = false;
    private Vector3 pullTarget;
    public float pullSpeed = 4f;


    public void StartPullToCenter(Vector3 target)
    {
        beingPulled = true;
        pullTarget = target;
        isFrozen = false;
        isAttacking = false;
    }

    public void Freeze()
    {
        if (beingPulled || enteringBase) return;
        isFrozen = true;
    }

    public void StartEnteringBase()
    {
        enteringBase = true;
        isFrozen = false;
        isAttacking = false;
    }

    void Update()
    {

        if (isFrozen)
            return;

       
        if (beingPulled)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                pullTarget,
                pullSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, pullTarget) < 0.1f)
            {
                beingPulled = false;
                StartEnteringBase(); 
            }

            return; 
        }

        if (enteringBase)
        {
            transform.Translate(transform.forward * Time.deltaTime * 1f, Space.World);

            if (Vector3.Distance(transform.position, GameFlow.Instance.transform.position) < 1.5f)
            {
                GameFlow.Instance.TriggerGameOver();
            }

            return;
        }

        if ((isDead || isAttacking) && !beingPulled && !enteringBase)
            return;

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

        if (coinDropPrefab != null)
        {
            Vector3 dropPos = transform.position + Vector3.up * 0.3f;
            GameObject drop = Instantiate(coinDropPrefab, dropPos, Quaternion.identity);
            drop.GetComponent<CoinDrop>().coinValue = coinReward;
        }

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
