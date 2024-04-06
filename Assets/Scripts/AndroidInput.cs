using UnityEngine;
using UnityEngine.InputSystem;

public class AndroidInput : MonoBehaviour
{
    [SerializeField] private InputAction touchInput;
    private Vector2 touchStartPosition;
    private Vector2 touchEndPosition;
    private bool touchStartedInsideLeftSide;

    private void Awake()
    {
        touchInput.Enable();
    }

    private void OnDisable()
    {
        touchInput.Disable();
    }

    private void Update()
    {
        if (Touchscreen.current.press.isPressed)
        {
            if (touchInput.triggered)
            {
                touchStartPosition = Touchscreen.current.position.ReadValue();
                // Check if the touch start is on the left side of the screen
                touchStartedInsideLeftSide = touchStartPosition.x < Screen.width / 2f;
            }
            else
            {
                touchEndPosition = Touchscreen.current.position.ReadValue();
                if (touchStartedInsideLeftSide && IsSwipeLeft(touchStartPosition, touchEndPosition))
                {
                    Restart();
                }
            }
        }
        else if (touchInput.WasReleasedThisFrame() && touchStartPosition.x > Screen.width / 2f)
        {
            // If the touch was released and started on the right side, consider it a shoot action
            Shoot();
        }
    }

    private bool IsSwipeLeft(Vector2 start, Vector2 end)
    {
        // Determine if the swipe was predominantly horizontal and to the left
        return (start - end).magnitude > 100 && Mathf.Abs(start.x - end.x) > Mathf.Abs(start.y - end.y) && (start.y > end.y);
    }

    private void Shoot()
    {
        // Add your shooting logic here
    }

    private void Restart()
    {
        // Add your restarting logic here
    }
}