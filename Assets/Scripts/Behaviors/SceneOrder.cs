using UnityEngine;

public class SceneOrder : MonoBehaviour
{
    // todo ����� ����� �����������:
    // service locator - ������ �������������� ������ static Instance,
    // ��� ����� ������ ������ ����� � ������� DontDestroyOnLoad

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