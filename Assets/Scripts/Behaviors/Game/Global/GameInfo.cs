using UnityEngine;

public class GameInfo : MonoBehaviour
{
    public GameObject protagonist;

    [SerializeField] private GameObject labelYouDie;
    [SerializeField] private GameObject labelYouWon;
    [SerializeField] private GameObject enemy;
    [SerializeField] private ShootingButtonCreator shootingButtonCreator;
    
    private BaseStateController cowboyState;
    private BaseStateController enemyState;
    private AiBehaviour aiBehaviour;
    private Countdown countdown;

    public static GameInfo Instance { get; private set; }

    private void Start()
    {
        cowboyState = protagonist.GetComponent<BaseStateController>();
        enemyState = enemy.GetComponent<BaseStateController>();
        aiBehaviour = enemy.GetComponent<AiBehaviour>();
        countdown = GetComponent<Countdown>();

        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public GameState State
    {
        get => state;
        set
        {
            state = value;
            if (state is GameState.PlayerWon or GameState.PlayerDead)
            {
                HandleGameOver(state);
            }
            else
            {
                if (state == GameState.NotStarted) Restart();
                labelYouDie.SetActive(false);
                labelYouWon.SetActive(false);
                if (state == GameState.Ongoing)
                {
                    aiBehaviour.StartAiming();
                    shootingButtonCreator.CreateButton();
                }
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
}