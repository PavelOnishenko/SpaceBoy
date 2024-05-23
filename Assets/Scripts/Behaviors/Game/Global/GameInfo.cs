using System.Linq;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    [SerializeField] private GameObject labelYouDie;
    [SerializeField] private GameObject labelYouWon;
    [SerializeField] private GameObject cowboy;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject shootButtonPrefab;
    [SerializeField] private GameObject shootButtonPositionsContainer;

    private CowboyState cowboyState;
    private CowboyState enemyState;
    private AiBehaviour aiBehaviour;
    private Countdown countdown;
    private Transform[] shootButtonPositions;

    public static GameInfo Instance { get; private set; }

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
                    aiBehaviour.GenerateRotationAndAim();
                    var choice = Random.Range(0, 9);
                    var buttonSpawnPoint = shootButtonPositions[choice];
                    Instantiate(shootButtonPrefab, buttonSpawnPoint.position, Quaternion.identity);
                }
            }
        }
    }

    private void Start()
    {
        cowboyState = cowboy.GetComponent<CowboyState>();
        enemyState = enemy.GetComponent<CowboyState>();
        aiBehaviour = enemy.GetComponent<AiBehaviour>();
        countdown = GetComponent<Countdown>();
        shootButtonPositions = GetComponentsInChildren<Transform>().ToArray();

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