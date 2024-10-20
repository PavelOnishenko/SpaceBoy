using UnityEngine;

public class GameSpeedAdjustment : MonoBehaviour
{
    [Range(0, 5)]
    public float animationSpeed = 2.0f;

    void Start()
    {
        Time.timeScale = animationSpeed;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}