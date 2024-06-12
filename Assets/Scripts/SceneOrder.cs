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

    private string nextScene = "MenuScene";

    public string GetNextScene() => nextScene;

    public void SetNextScene(string sceneName) => nextScene = sceneName;
}