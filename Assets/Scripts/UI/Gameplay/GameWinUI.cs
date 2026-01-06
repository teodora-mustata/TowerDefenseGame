using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWinUI : MonoBehaviour
{
    public static GameWinUI Instance;

    public GameObject panel;

    private void Awake()
    {
        Instance = this;
        //panel.SetActive(false);
    }

    public void Show()
    {
        
        panel.SetActive(true);
        Debug.Log("GameWinUI.Show() called!");
    }

    public void NextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
