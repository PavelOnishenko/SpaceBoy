using UnityEngine;
using System.Linq;
using Assets.Scripts.Menu;

public class LevelSelector : MonoBehaviour
{
    private GameObject levelHallway;
    private GameObject levelWindow;


    private void Awake()
    {
        levelHallway = transform.Cast<Transform>().Single(x => x.gameObject.name.Contains(Level.Hallway.ToString())).gameObject;
        levelWindow = transform.Cast<Transform>().Single(x => x.gameObject.name.Contains(Level.Window.ToString())).gameObject;
        levelHallway.SetActive(false);
        levelWindow.SetActive(false);
        var selectedLevel = IntersceneState.Instance.SelectedLevel;
        if (selectedLevel == Level.Hallway) levelHallway.SetActive(true);
        else levelWindow.SetActive(true);

        
    }
}