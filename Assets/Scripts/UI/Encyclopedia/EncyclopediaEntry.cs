using UnityEngine;

[CreateAssetMenu(menuName = "Encyclopedia/Entry")]
public class EncyclopediaEntry : ScriptableObject
{
    public string entryName;
    public Sprite icon;
    [TextArea] public string description;

    public GameObject prefab; 
}
