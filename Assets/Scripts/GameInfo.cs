using TMPro;
using UnityEngine;
using UnityEngine.Timeline;

public class GameInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI GameOverLabel;
    
    public static GameInfo Instance { get; private set; }

    public GameState State
    {
        get => state;
        set
        {
            state = value;
            if (value == GameState.Finished)
            {
                GameOverLabel.gameObject.SetActive(true);
            }
            else
            {
                GameOverLabel.gameObject.SetActive(false);
            }
        }
    }

    private GameState state;
    
    public enum GameState { NotStarted, Ongoing, Finished }

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
