using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    public GameObject settingsPanel;
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        Time.timeScale = 0f; 
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        Time.timeScale = 1f; 
    }
}
