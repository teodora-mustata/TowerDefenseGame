using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void OnPlay()
    {
        SceneManager.LoadScene("LevelsMenu");
    }

    public void OnCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void OnExit()
    {
        Application.Quit();
    }

}
