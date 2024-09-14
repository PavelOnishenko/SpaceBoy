using Assets.Scripts.Monetization;
using UnityEngine;
using UnityEngine.UI;

public class DisableAdsYesButton : MonoBehaviour
{
    [SerializeField] private Sprite offSprite;
    [SerializeField] private Sprite onSprite;

    private Image imageComponent;

    void Awake()
    {
        if (!TryGetComponent<Image>(out imageComponent))
            Debug.LogError("Image component not found on the PlayButton game object.");
    }

    public void Enter()
    {
        if (imageComponent != null) imageComponent.sprite = onSprite;
    }

    public void Exit()
    {
        if (imageComponent != null) imageComponent.sprite = offSprite;
    }

    public void Click()
    {
        
    }
}