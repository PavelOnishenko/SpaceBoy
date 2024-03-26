using TMPro;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameOverLabel;
    
    public static GameInfo Instance { get; private set; }

    public GameState State
    {
        get => state;
        set
        {
            state = value;
            if (value == GameState.PlayerWon || value == GameState.PlayerDead)
            {
                gameOverLabel.gameObject.SetActive(true);
                gameOverLabel.text = value == GameState.PlayerDead ? "You died" : "You won";
            }
            else
            {
                gameOverLabel.gameObject.SetActive(false);
            }
        }
    }

    private GameState state = GameState.NotStarted;

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