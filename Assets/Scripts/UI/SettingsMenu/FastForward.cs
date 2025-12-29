using UnityEngine;

public class FastForward : MonoBehaviour
{
    public float fastSpeed = 2f;
    private bool isFast = false;

    public void ToggleFastForward()
    {
        isFast = !isFast;

        if (isFast)
            Time.timeScale = fastSpeed;
        else
            Time.timeScale = 1f;
    }
}
