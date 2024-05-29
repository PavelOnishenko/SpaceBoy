using UnityEngine;

public class SceneOrder : MonoBehaviour
{
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