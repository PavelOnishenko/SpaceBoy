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
        { Level.Mountains, CharacterType.Brainman },
        { Level.NightCity, CharacterType.Lizard },
        { Level.Desert, CharacterType.Octopus }
    };

    private static IntersceneState instance;

    public CharacterType SelectedProtagonist { get; set; } = CharacterType.SpaceGirl;
    public Level SelectedLevel { get; set; } = Level.Outside;

    public CharacterType SelectedEnemy => enemyNameByLevel[SelectedLevel];
}