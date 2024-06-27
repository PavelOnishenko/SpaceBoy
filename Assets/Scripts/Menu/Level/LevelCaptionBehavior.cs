using UnityEngine;
using UnityEngine.UI;

public class LevelCaptionBehavior : MonoBehaviour
{
    [SerializeField] Sprite[] levelSprites;

    private Image image;

    private void Start() => image = GetComponent<Image>();

    public void SetLevelCaption(int level)
    {
        image.sprite = levelSprites[level - 1];
    }
}