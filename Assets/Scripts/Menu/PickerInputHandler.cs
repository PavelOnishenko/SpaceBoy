using Assets.Scripts.Menu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickerInputHandler : MonoBehaviour
{
    [SerializeField] private GameObject picker;
    [SerializeField] private CharacterType characterTypeToPick;

    private GameObject global;
    private CharacterPicker characterPicker;

    private void Start()
    {
        global = GameObject.Find("Global");
        characterPicker = global.GetComponent<CharacterPicker>();
    }

    public void OnClick()
    {
        characterPicker.Pick(characterTypeToPick);
    }
}
