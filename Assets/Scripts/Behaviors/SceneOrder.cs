using UnityEngine;

public class SceneOrder : MonoBehaviour
{
    public static SceneOrder Instance { get; private set; }
    private string nextScene = "MenuScene"; // Default start scene

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

    public string GetNextScene()
    {
        return nextScene;
    }

    public void SetNextScene(string sceneName)
    {
        nextScene = sceneName;
    }
}