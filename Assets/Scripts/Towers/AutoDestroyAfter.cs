using UnityEngine;

public class AutoDestroyAfter : MonoBehaviour
{
    public float lifetime = 0.1f; // ~ putin mai mare decat Duration + Lifetime

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}