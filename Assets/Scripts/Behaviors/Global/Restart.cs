using UnityEngine;
using UnityEngine.InputSystem;

public class Restart : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame) GameInfo.Instance.State = GameState.NotStarted;
    }
}
