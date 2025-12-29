using UnityEngine;
public class EncyclopediaButton : MonoBehaviour
{
    public EncyclopediaEntry entry;
    public EncyclopediaUI ui;

    public void OnClick()
    {
        ui.ShowEntry(entry);
    }
}
