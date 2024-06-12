using Assets.Scripts.Menu;
using UnityEngine;

public class CharacterPicker : MonoBehaviour
{
    [SerializeField] private GameObject SpaceGirlHighlight;
    [SerializeField] private GameObject GreenGirlHighlight;

    private CharacterType pickedCharacter;

    private void Start()
    {
        Pick(CharacterType.SpaceGirl);
    }

    public void Pick(CharacterType who)
    {
        pickedCharacter = who;
        if(pickedCharacter is CharacterType.GreenGirl)
        {
            GreenGirlHighlight.SetActive(true);
            SpaceGirlHighlight.SetActive(false);
            IntersceneState.Instance.SelectProtagonist(CharacterType.GreenGirl);
        }
        else
        {
            GreenGirlHighlight.SetActive(false);
            SpaceGirlHighlight.SetActive(true);
            IntersceneState.Instance.SelectProtagonist(CharacterType.SpaceGirl);
        }
    }
}