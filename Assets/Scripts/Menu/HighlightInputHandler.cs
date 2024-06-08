using Assets.Scripts.Menu;
using UnityEngine;

public class HighlightInputHandler : MonoBehaviour
{
    [SerializeField] private GameObject characterPickerGo;
    [SerializeField] private CharacterType characterTypeToPick;

    private CharacterPicker characterPicker;

    private void Start()
    {
        characterPicker = characterPickerGo.GetComponent<CharacterPicker>();
    }

    public void OnClick()
    {
        characterPicker.Pick(characterTypeToPick);
    }
}
