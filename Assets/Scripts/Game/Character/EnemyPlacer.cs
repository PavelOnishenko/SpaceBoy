using UnityEngine;
using System.Linq;
using Assets.Scripts.Menu;
using System.Collections.Generic;

public class EnemyPlacer : MonoBehaviour
{
    

    private GameObject brainMan;
    private GameObject lizard;

    private void Awake()
    {
        brainMan = transform.Cast<Transform>().Single(x => x.gameObject.name == CharacterType.Brainman.ToString()).gameObject;
        lizard = transform.Cast<Transform>().Single(x => x.gameObject.name == CharacterType.Lizard.ToString()).gameObject;
        brainMan.SetActive(false);
        lizard.SetActive(false);
        var selectedLevel = IntersceneState.Instance.SelectedLevel;
        if (selectedLevel == Level.Hallway) brainMan.SetActive(true);
        else lizard.SetActive(true);
    }
}