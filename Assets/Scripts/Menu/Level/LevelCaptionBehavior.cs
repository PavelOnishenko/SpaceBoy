using UnityEngine;
using UnityEngine.UI;

public class LevelCaptionBehavior : MonoBehaviour
{
    [SerializeField] Sprite spriteLevel1;
    [SerializeField] Sprite spriteLevel2;

    private Image image;

    private void Start() => image = GetComponent<Image>();

    public void SetLevelCaption(int level) => image.sprite = level == 1 ? spriteLevel1 : spriteLevel2;
}
