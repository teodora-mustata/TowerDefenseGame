using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [Header("Stats")]
    public string enemyName = "Slime";
    public float speed = 2f;
    public int health = 100;
    public string type = "Normal";

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    public void TakeDamage(int dmg, string dmgType)
    {
        if (type == "Plant" && dmgType == "Fire") dmg *= 2;
        if (type == "Fire" && dmgType == "Ice") dmg *= 2;
        if (type == "Fire" && dmgType == "Fire") dmg = Mathf.RoundToInt(dmg * 0.5f);

        health -= dmg;
        if (health <= 0) Destroy(gameObject);
    }
}
