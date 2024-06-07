using Assets.Scripts.Menu;
using UnityEngine;

public class CharacterPicker : MonoBehaviour
{
    [SerializeField] private GameObject SpaceGirlPicker;
    [SerializeField] private GameObject GreenGirlPicker;

    private CharacterType pickedCharacter;

    public void Pick(CharacterType who)
    {
        pickedCharacter = who;
        if(pickedCharacter is CharacterType.GreenGirl)
        {
            GreenGirlPicker.SetActive(true);
            SpaceGirlPicker.SetActive(false);
        }
        else
        {
            GreenGirlPicker.SetActive(false);
            SpaceGirlPicker.SetActive(true);
        }
    }
}