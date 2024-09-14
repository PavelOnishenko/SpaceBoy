using Assets.Scripts;

public class SceneOrder
{
    public static SceneOrder Instance
    {
        get
        {
            instance ??= new SceneOrder();
            return instance;
        }
    }

    private static SceneOrder instance;

    private SceneNames nextScene = SceneNames.MenuScene;

    public SceneNames GetNextSceneName() => nextScene;

    public void SetNextScene(SceneNames scene) => nextScene = scene;
}