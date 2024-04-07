using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class CowboyHandController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 0.3f;
    [SerializeField] private Transform joint;
    [SerializeField] private float lowerAimingBound = -40;
    [SerializeField] private float upperAimingBound = 40;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;

    private bool goingUp = true;
    private Quaternion initialRotation;
    private CowboyState state;
    private bool shotReady = true; // Flag to control shot triggering

    void Start()
    {
        state = GetComponentInParent<CowboyState>();
        initialRotation = transform.rotation;
    }

    private void Update()
    {
        if (state.IsDead) return;

        var handTransform = transform;
        if (state.IsAiming) RotateHand(handTransform);
        CheckForTouchInput();
    }

    private void CheckForTouchInput()
    {
        if (Touchscreen.current == null) return;

        TouchControl touch = Touchscreen.current.primaryTouch;
        if (touch.press.isPressed) ProcessTouchPress(touch);
        else shotReady = true; // Re-enable shooting for next touch
    }

    private void ProcessTouchPress(TouchControl touch)
    {
        var touchPosition = touch.position.ReadValue();
        if (GameInfo.Instance.State == GameState.Ongoing && shotReady && touchPosition.x > Screen.width / 2f)
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

    private void RotateHand(Transform handTransform)
    {
        handTransform.RotateAround(joint.position, Vector3.forward, (goingUp ? 1 : -1) * rotationSpeed);
        var currentRotation = handTransform.eulerAngles.z - initialRotation.eulerAngles.z;
        if (currentRotation > upperAimingBound && goingUp) goingUp = false;
        else if (currentRotation < lowerAimingBound && !goingUp) goingUp = true;
    }
}