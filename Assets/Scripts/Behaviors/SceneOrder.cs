using UnityEngine;

public class SceneOrder : MonoBehaviour
{
    // todo когда будем расширяться:
    // service locator - шаблон проектирования вместо static Instance,
    // там будем регать всякие штуки с помощью DontDestroyOnLoad

    public static SceneOrder Instance { get; private set; }

    private string nextScene = "MenuScene";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public string GetNextScene() => nextScene;

    public void SetNextScene(string sceneName) => nextScene = sceneName;
}