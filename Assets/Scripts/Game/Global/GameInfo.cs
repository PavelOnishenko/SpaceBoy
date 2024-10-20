using Assets.Scripts;
using Assets.Scripts.Menu;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInfo : MonoBehaviour
{
    [SerializeField] private GameObject labelYouDie;
    [SerializeField] private GameObject labelYouWon;
    [SerializeField] private GameObject enemyContainer;
    [SerializeField] private GameObject shootingButtonContainer;
    [SerializeField] private GameObject countdownContainer;
    [SerializeField] private GameObject protagonistContainer;
    [SerializeField] private float delayAfterVictorySeconds = 3f;
    [SerializeField] private GameObject popupOverlay;
    [SerializeField] private GameObject labelGameCompleted;
    [SerializeField] private GameObject shootButton;

    public GameObject Protagonist => protagonist;
    public GameObject Enemy => enemy;

    private CharacterState protagonistState;
    private CharacterState enemyState;
    private Ai ai;
    private Countdown countdown;
    private GameObject protagonist;
    private GameObject enemy;

    public static GameInfo Instance { get; private set; }

    private void Start()
    {
        SetCharacterRelatedVariables();
        countdown = countdownContainer.GetComponent<Countdown>();
        if (Instance != null)
            Destroy(gameObject);
        Instance = this;
        var selectedProtagonist = IntersceneState.Instance.SelectedProtagonist;
        PlayerPrefs.SetString(PlayerPrefNames.LastSelectedProtagonist.ToString(), selectedProtagonist.ToString());
    }

    private void SetCharacterRelatedVariables()
    {
        protagonist = protagonistContainer.transform.Cast<Transform>()
            .Single(x => x.gameObject.name.Contains(IntersceneState.Instance.SelectedProtagonist.ToString())).gameObject;
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
            if (_state is GameState.PlayerWon or GameState.PlayerDead) 
                HandleGameOverStateChange(value);
            else 
                HandleInGameStateChange(value);
        }
    }

    private void HandleGameOverStateChange(GameState value)
    {
        if (value == GameState.PlayerDead)
        {
            AudioManager.Instance.PlaySound("Lose");
            labelYouDie.SetActive(true);
            labelYouWon.SetActive(false);
            popupOverlay.SetActive(true);
        }
        else
        {
            AudioManager.Instance.PlaySound("Win");
            Win();
        }
        shootButton.SetActive(false);
    }

    private void Win()
    {
        var selectedLevel = (int)IntersceneState.Instance.SelectedLevel;
        var lastCompletedLevelPrefName = PlayerPrefNames.LastCompletedLevel.ToString();
        var victoryPopup = labelYouWon;
        if (selectedLevel > PlayerPrefs.GetInt(lastCompletedLevelPrefName))
        {
            PlayerPrefs.SetInt(lastCompletedLevelPrefName, selectedLevel);
            Debug.Log($"Last completed level set to [{selectedLevel}].");
            if (selectedLevel == Enum.GetValues(typeof(Level)).Length)
                victoryPopup = labelGameCompleted;
        }
        labelYouDie.SetActive(false);
        victoryPopup.SetActive(true);
        popupOverlay.SetActive(true);
        StartCoroutine(GoToMenuAfterDelayCoroutine());
    }

    private IEnumerator GoToMenuAfterDelayCoroutine()
    {
        yield return new WaitForSeconds(delayAfterVictorySeconds);
        SceneOrder.Instance.SetNextScene(SceneNames.MenuScene);
        SceneManager.LoadScene(SceneNames.LoadingScene.ToString());
    }

    private void HandleInGameStateChange(GameState state)
    {
        if (state == GameState.NotStarted) 
            Restart();
        labelYouDie.SetActive(false);
        labelYouWon.SetActive(false);
        popupOverlay.SetActive(false);
        if (state == GameState.Ongoing)
        {
            shootButton.SetActive(true);
            ai.AttackAfterDelay();
        }
    }

    private void Restart()
    {
        countdown.Restart();
        protagonistState.Revive();
        enemyState.Revive();
        ai.AttackAfterDelay();
        shootButton.SetActive(false);
    }

    private GameState _state = GameState.NotStarted;
}