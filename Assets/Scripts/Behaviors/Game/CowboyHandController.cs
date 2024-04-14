using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class CowboyHandController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;

    private CowboyState state;
    private bool shotReady = true; // Flag to control shot triggering

    void Start()
    {
        state = GetComponent<CowboyState>();
    }

    private void Update()
    {
        if (state.IsDead) return;
        
        var touchFound = CheckForTouchInput();
        var mouseActionFound = CheckForMouseInput();
        if(!touchFound && !mouseActionFound) shotReady = true; // Re-enable shooting for next touch
    }

    private bool CheckForMouseInput()
    {
        if (Input.GetMouseButtonDown(0)) ProcessTouchPress(Input.mousePosition);
        else return false;
        return true;
    }

    private bool CheckForTouchInput()
    {
        if (Touchscreen.current == null) return false;

        TouchControl touch = Touchscreen.current.primaryTouch;
        if (touch.press.isPressed) ProcessTouchPress(touch.position.ReadValue());
        return true;
    }

    private void ProcessTouchPress(Vector2 touchLocation)
    {
        if (GameInfo.Instance.State == GameState.Ongoing && shotReady && touchLocation.x > Screen.width / 2f)
        {
            ShootOrAim();
            shotReady = false; // Prevent continuous shooting
        }
    }

    private void ShootOrAim()
    {
        if (state.IsAiming)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, -90); // Creating a quaternion for -90 degrees rotation around Z-axis
            Quaternion result = bulletSpawnPoint.rotation * rotation; 
            Instantiate(bulletPrefab, bulletSpawnPoint.position, result);
            state.PauseAiming();
        }
        else
        {
            state.StartAiming();
        }
    }
}