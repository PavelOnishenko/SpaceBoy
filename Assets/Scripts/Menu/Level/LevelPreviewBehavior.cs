using UnityEngine;
using UnityEngine.UI;

public class LevelPreviewBehavior : MonoBehaviour
{
    [SerializeField] Sprite[] levelSprites;

    private Image image;

    private void Start() => image = GetComponent<Image>();

    public void SetLevelPreview(int level) => image.sprite = levelSprites[level - 1];
}  