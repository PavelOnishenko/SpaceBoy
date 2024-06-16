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

    public CharacterType SelectedProtagonist => selectedProtagonist;
    public Level SelectedLevel => selectedLevel;

    private CharacterType selectedProtagonist = CharacterType.SpaceGirl;
    private Level selectedLevel = Level.Hallway;

    public void SelectProtagonist(CharacterType protagonist) => selectedProtagonist = protagonist;
    public void SelectLevel(Level level) => selectedLevel = level;
}