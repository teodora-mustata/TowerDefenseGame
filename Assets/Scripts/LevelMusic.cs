using UnityEngine;

public class LevelMusic : MonoBehaviour
{
    public AudioClip levelClip;

    void Start()
    {
        if (MusicManager.instance != null && levelClip != null)
        {
            MusicManager.instance.PlayMusic(levelClip);
        }
    }
}
