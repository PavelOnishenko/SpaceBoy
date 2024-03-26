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
            if (value == GameState.PlayerWon || value == GameState.PlayerDead)
            {
                GameOverLabel.gameObject.SetActive(true);
                if (value == GameState.PlayerDead)
                {
                    GameOverLabel.text = "You died";
                }

                if (value == GameState.PlayerWon)
                {
                    GameOverLabel.text = "You won";
                }
            }
            else
            {
                GameOverLabel.gameObject.SetActive(false);
            }
        }
    }

    private GameState state;
    
    public enum GameState { NotStarted, Ongoing, PlayerWon, PlayerDead }

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
