using UnityEngine;

public class GameInfo : MonoBehaviour
{
    public GameObject protagonist;

    [SerializeField] private GameObject labelYouDie;
    [SerializeField] private GameObject labelYouWon;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject shootingButtonContainer;
    [SerializeField] private GameObject countdownContainer;
    
    private CharacterState cowboyState;
    private CharacterState enemyState;
    private Ai ai;
    private Countdown countdown;
    private ShootButtonCreator shootButtonCreator;

    public static GameInfo Instance { get; private set; }

    private void Start()
    {
        cowboyState = protagonist.GetComponent<CharacterState>();
        enemyState = enemy.GetComponent<CharacterState>();
        ai = enemy.GetComponent<Ai>();
        countdown = countdownContainer.GetComponent<Countdown>();
        shootButtonCreator = shootingButtonContainer.GetComponent<ShootButtonCreator>();

        if (Instance != null)
            Destroy(gameObject);
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // todo look at Unity Timeline tutorial and Unity Playable
    public GameState State
    {
        get => _state;
        set
        {
            _state = value;
            if (_state is GameState.PlayerWon or GameState.PlayerDead) HandleGameOver(value);
            else HandleInGameEvents(value);
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

    private void HandleInGameEvents(GameState state)
    {
        if (state == GameState.NotStarted) Restart();
        labelYouDie.SetActive(false);
        labelYouWon.SetActive(false);
        if (state == GameState.Ongoing)
        {
            ai.AttackAfterDelay();
        }
    }

    private void Restart()
    {
        countdown.Restart();
        cowboyState.Revive();
        enemyState.Revive();
    }

    private GameState _state = GameState.NotStarted;
}