using UnityEngine;
using UnityEngine.InputSystem;

public class RestartButton : MonoBehaviour
{
    public void Restart()
    {
        GameInfo.Instance.State = GameState.NotStarted;
    }
}
