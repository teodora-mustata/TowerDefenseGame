using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance;

    public GameObject panel;

    private AudioSource audioSource;
    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    public void Show()
    {
        panel.SetActive(true);

        audioSource.Play(); 
    }

    
   
}
