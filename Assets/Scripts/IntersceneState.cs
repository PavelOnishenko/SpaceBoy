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

    public static readonly Dictionary<Level, CharacterType> enemyNameByLevel = new Dictionary<Level, CharacterType>()
    {
        { Level.Hallway, CharacterType.Octopus },
        { Level.Window, CharacterType.Lizard }
    };

    private static IntersceneState instance;

    public CharacterType SelectedProtagonist { get; set; } = CharacterType.SpaceGirl;
    public Level SelectedLevel { get; set; } = Level.Hallway;
}