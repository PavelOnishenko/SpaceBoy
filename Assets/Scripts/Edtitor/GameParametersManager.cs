using UnityEngine;

public class GameParametersManager : MonoBehaviour
{
    private static GameParametersManager _instance;

    public static GameParametersManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameParametersManager>();
                if (_instance == null)
                {
                    var singleton = new GameObject(typeof(GameParametersManager).Name);
                    _instance = singleton.AddComponent<GameParametersManager>();
                }
            }
            return _instance;
        }
    }

    public GameParameters gameParameters;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else if (_instance != this) Destroy(gameObject);
    }
}