using UnityEngine;

[System.Serializable]
public class TowerUpgrade
{
    public string newName;
    public int newDamage;
    public float newFireRate;
    public string newDamageType;
    [HideInInspector] public bool applied = false;
}
