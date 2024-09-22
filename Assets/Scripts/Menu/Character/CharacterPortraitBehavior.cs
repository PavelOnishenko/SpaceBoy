using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// todo EXTRACT 3 times - LevelPreviewBehavior & LevelCaptionBehavior
public class CharacterPortraitBehavior : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;

    private Image image;

    private void Start() => image = GetComponent<Image>();

    public void SetPreview(int index) => image.sprite = sprites[SpriteIndexByCharacterIndex[index]];

    private Dictionary<int, int> SpriteIndexByCharacterIndex = new Dictionary<int, int>
    {
        { 0, 1 },
        { 1, 2 },
        { 2, 0 }
    };
}