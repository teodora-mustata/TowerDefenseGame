using UnityEngine;
using UnityEngine.UI;

public class BrightnessSlider : MonoBehaviour
{
    public Slider slider;
    public Image overlay;

    [Range(0f, 1f)]
    public float minDarkness = 0.3f;
    public float maxDarkness = 0f;

    void Start()
    {
        float savedValue = PlayerPrefs.GetFloat("brightness", 1f);
        slider.value = savedValue;
        ApplyBrightness(savedValue);

        slider.onValueChanged.AddListener((v) =>
        {
            ApplyBrightness(v);
            PlayerPrefs.SetFloat("brightness", v);
        });
    }

    void ApplyBrightness(float value)
    {
        float alpha = Mathf.Lerp(minDarkness, maxDarkness, value);

        Color c = overlay.color;
        c.a = alpha;
        overlay.color = c;
    }
}
