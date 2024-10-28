using Assets.Analytics;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour
{
    [SerializeField] private Sprite offSprite;
    [SerializeField] private Sprite onSprite;

    private bool isOn = true;
    private Image imageComponent;

    void Awake()
    {
        imageComponent = GetComponent<Image>();
        isOn = !AudioListener.pause;
        imageComponent.sprite = isOn ? onSprite : offSprite;
    }

    public void Click()
    {
        isOn = !isOn;
        imageComponent.sprite = isOn ? onSprite : offSprite;
        AudioListener.pause = !isOn;
        var toggleEvent = new SoundToggleEvent
        {
            SoundOn = isOn
        };
        AnalyticsService.Instance.RecordEvent(toggleEvent);
    }
}
