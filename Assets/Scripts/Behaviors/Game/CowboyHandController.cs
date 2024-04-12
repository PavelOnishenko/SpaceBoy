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
            ShootOrAim(transform);
            shotReady = false; // Prevent continuous shooting
        }
    }

    private void ShootOrAim(Transform handTransform)
    {
        if (state.IsAiming)
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.position, handTransform.rotation);
            state.StopAiming();
        }
        else
        {
            state.StartAiming();
        }
    }
}