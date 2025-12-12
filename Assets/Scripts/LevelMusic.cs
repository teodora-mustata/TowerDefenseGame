using UnityEngine;

public class LevelMusic : MonoBehaviour
{
    public AudioClip levelClip;

    void Start()
    {
        if (MusicManager.instance != null && levelClip != null)
        {
            if (MusicManager.instance.audioSource.clip == levelClip)
                return;

            MusicManager.instance.PlayMusic(levelClip);
        }
    }
}
