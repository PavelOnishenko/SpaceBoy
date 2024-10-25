using Assets.Scripts;
using Assets.Scripts.Menu;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInfo : MonoBehaviour
{
    public CharacterState protagonistState;

    [SerializeField] private GameObject labelYouDie;
    [SerializeField] private GameObject labelYouWon;
    [SerializeField] private GameObject countdownContainer;
    [SerializeField] private float delayAfterVictorySeconds = 3f;
    [SerializeField] private GameObject popupOverlay;
    [SerializeField] private GameObject labelGameCompleted;
    [SerializeField] private GameObject shootButton;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject restartButton;

    private Ai ai;
    private CharacterState enemyState;
    private Countdown countdown;

    public static GameInfo Instance { get; private set; }

    private void Start()
    {
        enemyState = enemy.GetComponent<CharacterState>();
        var specificEnemy = IntersceneState.GetCharacterDependentTransform(enemy.transform, false);
        ai = specificEnemy.GetComponent<Ai>();
        countdown = countdownContainer.GetComponent<Countdown>();
        if (Instance != null)
            Destroy(gameObject);
        Instance = this;
        var selectedProtagonist = IntersceneState.Instance.SelectedProtagonist;
        PlayerPrefs.SetString(PlayerPrefNames.LastSelectedProtagonist.ToString(), selectedProtagonist.ToString());
    }

    public GameState State
    {
        get => _state;
        set
        {
            _state = value;
            restartButton.SetActive(false);
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
            restartButton.SetActive(true);
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
            //ai.AttackAfterDelay();
        }
    }

    private void Restart()
    {
        countdown.Restart();
        protagonistState.Revive();
        enemyState.Revive();
        //ai.AttackAfterDelay();
        shootButton.SetActive(false);
    }

    private GameState _state = GameState.NotStarted;
}