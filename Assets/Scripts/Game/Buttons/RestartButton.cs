using Assets.Analytics;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.InputSystem;

public class RestartButton : MonoBehaviour
{
    public void Restart()
    {
        GameInfo.Instance.State = GameState.NotStarted;
        var restartEvent = new RestartEvent
        {
            LevelIndex = (int)IntersceneState.Instance.SelectedLevel,
            LevelName = IntersceneState.Instance.SelectedLevel.ToString(),
            ProtagonistCharacterName = IntersceneState.Instance.SelectedProtagonist.ToString(),
            EnemyCharacterName = IntersceneState.Instance.SelectedEnemy.ToString()
        };
        AnalyticsService.Instance.RecordEvent(restartEvent);
    }
}
