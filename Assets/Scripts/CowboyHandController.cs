using UnityEngine;
using UnityEngine.InputSystem;

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

        // Simplified input detection logic
        if (GameInfo.Instance.State == GameState.Ongoing)
        {
            // Touch input detection
            if (Touchscreen.current != null && Touchscreen.current.press.isPressed)
            {
                if (shotReady)
                {
                    Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
                    // Shoot only if the touch is on the right side and the shot is ready
                    if (touchPosition.x > Screen.width / 2)
                    {
                        ProcessShot(handTransform);
                        shotReady = false; // Prevent continuous shooting
                    }
                }
            }
            else if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                ProcessShot(handTransform);
            }
            else
            {
                shotReady = true; // Re-enable shooting when there's no touch or mouse press
            }
        }
    }

    private void ProcessTouchInput()
    {
        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        // Determines if the touch is on the right side of the screen for shooting
        if (touchPosition.x > Screen.width / 2f)
        {
            ProcessShot(transform);
        }
    }

    private void ProcessShot(Transform handTransform)
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