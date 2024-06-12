using UnityEngine;
using System.Linq;
using Assets.Scripts.Menu;

public class ProtagonistPlacer : MonoBehaviour
{
    private GameObject spaceGirl;
    private GameObject greenGirl;

    private void Awake()
    {
        spaceGirl = transform.Cast<Transform>().Single(x => x.gameObject.name == "SpaceGirl").gameObject;
        spaceGirl.SetActive(false);
        greenGirl = transform.Cast<Transform>().Single(x => x.gameObject.name == "GreenGirl").gameObject;
        var selectedProtagonist = IntersceneState.Instance.SelectedProtagonist;
        if (selectedProtagonist == CharacterType.SpaceGirl)
        {
            spaceGirl.SetActive(true);
            greenGirl.SetActive(false);
        }
        else
        {
            spaceGirl.SetActive(false);
            greenGirl.SetActive(true);
        }
    }
}
