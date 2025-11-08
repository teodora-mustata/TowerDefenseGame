using UnityEngine;

public class TowerTest : MonoBehaviour
{
    public BaseTower tower;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            tower.animator.SetTrigger("ShootTrigger");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            tower.Die();
        }
    }
}
