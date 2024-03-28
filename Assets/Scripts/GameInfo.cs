using TMPro;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    [SerializeField] private GameObject labelYouDie;
    [SerializeField] private GameObject labelYouWon;
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
            else
            {
                if (value == GameState.NotStarted) Restart();
                labelYouDie.SetActive(false);
                labelYouWon.SetActive(false);
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