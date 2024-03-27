using TMPro;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameOverLabel;
    [SerializeField] private GameObject cowboy;
    [SerializeField] private GameObject enemy;

    private CowboyState cowboyState;
    private CowboyState enemyState;
    private Countdown countdown;

    public static GameInfo Instance { get; private set; }

    public GameState State
    {
        get => state;
        set
        {
            state = value;
            if (value == GameState.PlayerWon || value == GameState.PlayerDead)
            {
                gameOverLabel.text = value == GameState.PlayerDead ? "You died" : "You won";
                gameOverLabel.gameObject.SetActive(true);
            }
            else
            {
                if (value == GameState.NotStarted) Restart();
                gameOverLabel.gameObject.SetActive(false);
            }
        }
    }

    private void Restart()
    {
        countdown.Restart();
        cowboyState.Revive();
        enemyState.Revive();
    }

    private GameState state = GameState.NotStarted;

    private void Start()
    {
        cowboyState = cowboy.GetComponent<CowboyState>();
        enemyState = enemy.GetComponent<CowboyState>();
        countdown = GetComponent<Countdown>();
        
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