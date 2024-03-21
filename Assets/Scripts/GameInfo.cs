using UnityEngine;

public class GameInfo : MonoBehaviour
{
    public static GameInfo Instance { get; private set; }

    public bool GameStarted { get; set; }

    private void Awake()
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
}
