using UnityEngine;

public class GameInfo : MonoBehaviour
{
    [SerializeField] private GameObject labelYouDie;
    [SerializeField] private GameObject labelYouWon;
    [SerializeField] private GameObject cowboy;
    [SerializeField] private GameObject enemy;

    private CowboyState cowboyState;
    private CowboyState enemyState;
    private AiBehaviour aiBehaviour;
    private Countdown countdown;

    public static GameInfo Instance { get; private set; }

    public GameState State
    {
        get => state;
        set
        {
            state = value;
            if (value is GameState.PlayerWon or GameState.PlayerDead)
            {
                HandleGameOver(value);
            }
            else
            {
                if (value == GameState.NotStarted) Restart();
                labelYouDie.SetActive(false);
                labelYouWon.SetActive(false);
                if (value == GameState.Ongoing) aiBehaviour.GenerateRotationAndAim();
            }
        }
    }

    private void HandleGameOver(GameState value)
    {
        if (value == GameState.PlayerDead)
        {
            labelYouDie.SetActive(true);
            labelYouWon.SetActive(false);
        }
        else
        {
            labelYouDie.SetActive(false);
            labelYouWon.SetActive(true);
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
        aiBehaviour = enemy.GetComponent<AiBehaviour>();
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