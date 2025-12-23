using UnityEngine;

public class BloodEffectController : MonoBehaviour
{
    [Header("Blood FX")]
    [Tooltip("Prefab-ul cu animatia de sange (BloodHitEffect).")]
    public GameObject bloodEffectPrefab;

    [Tooltip("Locul de unde iese sangele. Daca e null, se foloseste transformul acestui obiect.")]
    public Transform bloodSpawnPoint;

    public void PlayBloodEffect()
    {
        PlayBloodEffectInternal(false);
    }

    public void PlayBloodEffectFast()
    {
        PlayBloodEffectInternal(true);
    }

    private void PlayBloodEffectInternal(bool fast)
    {
        if (bloodEffectPrefab == null)
        {
            Debug.LogWarning($"[BloodEffectController] Nu e setat bloodEffectPrefab pe {name}");
            return;
        }

        Transform spawn = bloodSpawnPoint != null ? bloodSpawnPoint : transform;

        GameObject fxObj = Instantiate(bloodEffectPrefab, spawn.position, spawn.rotation);

    }
}