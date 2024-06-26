using System.Linq;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    [SerializeField] private GameObject labelYouDie;
    [SerializeField] private GameObject labelYouWon;
    [SerializeField] private GameObject enemyContainer;
    [SerializeField] private GameObject shootingButtonContainer;
    [SerializeField] private GameObject countdownContainer;
    [SerializeField] private GameObject protagonistContainer;

    public GameObject Protagonist => protagonist;
    public GameObject Enemy => enemy;

    private CharacterState protagonistState;
    private CharacterState enemyState;
    private Ai ai;
    private Countdown countdown;
    private ShootButtonCreator shootButtonCreator;
    private GameObject protagonist;
    private GameObject enemy;

    public static GameInfo Instance { get; private set; }

    private void Start()
    {
        SetCharacterRelatedVariables();
        countdown = countdownContainer.GetComponent<Countdown>();
        shootButtonCreator = shootingButtonContainer.GetComponent<ShootButtonCreator>();
        if (Instance != null)
            Destroy(gameObject);
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void SetCharacterRelatedVariables()
    {
        protagonist = protagonistContainer.transform.Cast<Transform>()
            .Single(x => x.gameObject.name == IntersceneState.Instance.SelectedProtagonist.ToString()).gameObject;
        protagonistState = protagonist.GetComponent<CharacterState>();
        var enemyName = IntersceneState.Instance.SelectedEnemy.ToString();
        enemy = enemyContainer.transform.Cast<Transform>().Single(x => x.gameObject.name.Contains(enemyName)).gameObject;
        enemyState = enemy.GetComponent<CharacterState>();
        ai = enemy.GetComponent<Ai>();
    }

    public GameState State
    {
        get => _state;
        set
        {
            _state = value;
            if (_state is GameState.PlayerWon or GameState.PlayerDead) HandleGameOverStateChange(value);
            else HandleInGameStateChange(value);
        }
    }

    private void HandleGameOverStateChange(GameState value)
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

    private void HandleInGameStateChange(GameState state)
    {
        if (state == GameState.NotStarted) Restart();
        labelYouDie.SetActive(false);
        labelYouWon.SetActive(false);
        if (state == GameState.Ongoing) ai.AttackAfterDelay();
    }

    private void Restart()
    {
        countdown.Restart();
        protagonistState.Revive();
        enemyState.Revive();
        ai.AttackAfterDelay();
        shootButtonCreator.DestroyButton();
    }

    private GameState _state = GameState.NotStarted;
}