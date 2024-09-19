using Assets.Scripts.Menu;
using System.Collections.Generic;

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
        { Level.Bar, CharacterType.Raccoon }
    };

    private static IntersceneState instance;

    public CharacterType SelectedProtagonist { get; set; } = CharacterType.SpaceGirl;

    public Level SelectedLevel { get; set; } = Level.Outside;

    public CharacterType SelectedEnemy => enemyNameByLevel[SelectedLevel];
}