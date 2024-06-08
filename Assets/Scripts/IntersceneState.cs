using Assets.Scripts.Menu;
using UnityEngine;

public class IntersceneState
{
    public static IntersceneState Instance
    {
        get
        {
            instance ??= new IntersceneState();
            return instance;
        }
    }

    private static IntersceneState instance;

    public CharacterType SelectedProtagonist => selectedProtagonist;

    private CharacterType selectedProtagonist;

    public void SelectProtagonist(CharacterType protagonist)
    {
        selectedProtagonist = protagonist;
    }
}
