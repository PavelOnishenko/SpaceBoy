using Assets.Scripts.Menu;
using System.Collections.Generic;
using System.Linq;
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

    private static readonly Dictionary<Level, CharacterType> enemyNameByLevel = new()
    {
        { Level.Hallway, CharacterType.Brainman },
        { Level.Window, CharacterType.Lizard },
        { Level.Outside, CharacterType.Octopus },
        { Level.Mountains, CharacterType.AstronautGirl },
        { Level.NightCity, CharacterType.CyberPunk },
        { Level.Desert, CharacterType.RobotGirl },
        { Level.Bar, CharacterType.Raccoon },
        { Level.Shrine, CharacterType.Spider }
    };

    private static IntersceneState instance;

    public CharacterType SelectedProtagonist { get; set; } = CharacterType.GreenGirl;

    public Level SelectedLevel { get; set; } = Level.NightCity; 

    public CharacterType SelectedEnemy => enemyNameByLevel[SelectedLevel];

    public static Transform GetCharacterDependentTransform(Transform parentCharacterTrannsform, bool isProtagonist)
    {
        var selectedCharacterType = isProtagonist ? Instance.SelectedProtagonist : Instance.SelectedEnemy;
        var characterTransform = parentCharacterTrannsform.Cast<Transform>().Single(transform => transform.gameObject.name.Contains(selectedCharacterType.ToString()));
        return characterTransform;
    }
}