using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class DisableAdsNoButton : MonoBehaviour
{
    [SerializeField] private Sprite offSprite;
    [SerializeField] private Sprite onSprite;
    [SerializeField] private AdDisplay adDisplay;

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
        adDisplay.ShowAd();
    }
}
