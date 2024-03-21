using UnityEngine;
using UnityEngine.InputSystem;

public class CowboyHandController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private Transform joint;
    [SerializeField] private float lowerAimingBound = -40;
    [SerializeField] private float upperAimingBound = 40;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;

    private bool isAiming;
    private bool goingUp = true;
    private Quaternion initialRotation;

    void Start() => initialRotation = transform.rotation;

    private void Update()
    {
        var handTransform = transform;
        if (isAiming) RotateHand(handTransform);
        if (Mouse.current.leftButton.wasPressedThisFrame && GameInfo.Instance.GameStarted)
            if (isAiming)
            {
                Instantiate(bulletPrefab, bulletSpawnPoint.position, handTransform.rotation);
                isAiming = false;
            }
            else
            {
                isAiming = true;
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