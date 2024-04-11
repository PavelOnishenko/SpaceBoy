using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class RestartByTouch : MonoBehaviour
{
    private Vector2 touchStart; 
    private bool isSwipeStartLeftSide; 

    void Update()
    {
        var touch = Touchscreen.current?.primaryTouch;
        if(touch is null) return;
        
        if (touch.press.isPressed)
        {
            var touchPosition = touch.position.ReadValue();
            if (touchPosition.x < Screen.width / 2f)
            {
                touchStart = touch.position.ReadValue();
                isSwipeStartLeftSide = true;
            }
        }
        
        CheckForLeftSwipe(touch); 
    }
    
    private void CheckForLeftSwipe(TouchControl touch)
    {
        if (isSwipeStartLeftSide && touch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Ended)
        {
            Vector2 touchEnd = touch.position.ReadValue();
            if (IsSwipeLeft(touchStart, touchEnd)) RestartGame();
            isSwipeStartLeftSide = false;
        }
    }
    
    private bool IsSwipeLeft(Vector2 start, Vector2 end) => 
        (start - end).magnitude > 10 && Mathf.Abs(start.x - end.x) < Mathf.Abs(start.y - end.y) && (start.y < end.y);

    private void RestartGame() => GameInfo.Instance.State = GameState.NotStarted;
}
