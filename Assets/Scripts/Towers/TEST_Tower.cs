using UnityEngine;

public class TowerTest : MonoBehaviour
{
    public BaseTower tower;

    public KeyCode shootKey = KeyCode.Alpha1;
    public KeyCode dieKey = KeyCode.Alpha2;

    void Update()
    {
        if (Input.GetKeyDown(shootKey))
            tower.animator.SetTrigger("ShootTrigger");

        if (Input.GetKeyDown(dieKey))
            tower.Die();
    }

}
