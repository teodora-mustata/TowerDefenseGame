using UnityEngine;

public class EndPointTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        BaseEnemy enemy = other.GetComponent<BaseEnemy>();

        if (enemy != null)
        {
            GameFlow.Instance.FreezeEverything();

            BoxCollider box = GetComponent<BoxCollider>();
            Vector3 center = transform.TransformPoint(box.center);

            center.y = enemy.transform.position.y;

            enemy.StartPullToCenter(center);
        }
    }
}
