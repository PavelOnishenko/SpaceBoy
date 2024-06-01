using UnityEngine;

public class SceneOrder : MonoBehaviour
{
    public static SceneOrder Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SceneOrder>();
                if (_instance == null)
                {
                    var gameObject = new GameObject("SceneOrderContainer");
                    _instance = gameObject.AddComponent<SceneOrder>();
                    DontDestroyOnLoad(gameObject);
                }
            }
            return _instance;
        }
    }

    public SceneOrder()
    {
        
    }

    private static  SceneOrder _instance;

    private string nextScene = "MenuScene";

    public string GetNextScene() => nextScene;

    public void SetNextScene(string sceneName) => nextScene = sceneName;
}