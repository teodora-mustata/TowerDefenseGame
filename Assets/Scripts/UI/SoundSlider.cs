using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        slider.value = MusicManager.instance.volume;

        slider.onValueChanged.AddListener((v) =>
        {
            MusicManager.instance.SetVolume(v);
        });
    }
}
