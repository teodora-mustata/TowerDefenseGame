using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public AudioSource audioSource;
    public float volume = 1f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadVolume();
        ApplyVolume();
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;
        if (audioSource == null) return;

        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
        ApplyVolume();
    }

    public void SetVolume(float v)
    {
        volume = v;
        ApplyVolume();
        SaveVolume();
    }

    void ApplyVolume()
    {
        if (audioSource != null)
            audioSource.volume = volume;
    }

    void SaveVolume()
    {
        PlayerPrefs.SetFloat("global_volume", volume);
    }

    void LoadVolume()
    {
        volume = PlayerPrefs.GetFloat("global_volume", 1f);
    }
}
