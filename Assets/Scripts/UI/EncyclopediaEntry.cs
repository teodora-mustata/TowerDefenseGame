using UnityEngine;

public enum EncyclopediaType { Tower, Enemy }

public abstract class EncyclopediaEntry : ScriptableObject
{
    public string entryName;
    public Sprite icon;
    [TextArea] public string description;
    public EncyclopediaType type;

    public GameObject previewPrefab;
}
