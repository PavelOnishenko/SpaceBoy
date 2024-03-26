using System;
using System.Collections;
using UnityEngine;

public class AiBehaviour : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 0.3f;
    [SerializeField] private float lowerAimingBound = -40;
    [SerializeField] private float upperAimingBound = 40;
    [SerializeField] private Transform joint;
    [SerializeField] private float aimingDelayMs = 500;
    [SerializeField] private float epsilon = 0.5f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float accuracy;

    private bool isAiming;
    private float? desiredRotation;
    private bool goingUp = true;
    private Quaternion initialRotation;
    private CowboyState state;

    void Start()
    {
        initialRotation = transform.rotation;
        StartCoroutine(AimingDelayCoroutine());
        state = gameObject.GetComponentInParent<CowboyState>();
    }

    void Update()
    {
        if (!desiredRotation.HasValue) desiredRotation = GenerateDesiredRotation();

        if (GameInfo.Instance.State != GameInfo.GameState.Ongoing) return;
        
        if(state.IsDead) return;

        var handTransform = transform;
        var currentRotation = handTransform.eulerAngles.z - initialRotation.eulerAngles.z;
        if (desiredRotation.HasValue && Math.Abs(currentRotation - desiredRotation.Value) < epsilon) ProcessShooting(handTransform.rotation);
        
        if (isAiming)
        {
            handTransform.RotateAround(joint.position, Vector3.forward, (goingUp ? 1 : -1) * rotationSpeed);
            if (currentRotation > upperAimingBound && goingUp) goingUp = false;
            else if (currentRotation < lowerAimingBound && !goingUp) goingUp = true;
        }
    }

    private float GenerateDesiredRotation()
    {
        var result = GenerateGaussianRandom(0, 100 / accuracy);
        if (result > upperAimingBound) result = upperAimingBound;
        else if (result < lowerAimingBound) result = lowerAimingBound;
        return result;
    }

    private void ProcessShooting(Quaternion handRotation)
    {
        desiredRotation = null;
        isAiming = false;
        Instantiate(bulletPrefab, bulletSpawnPoint.position, handRotation);
        StartCoroutine(AimingDelayCoroutine());
    }

    private static float GenerateGaussianRandom(float mean = 0f, float stdDev = 1f) =>
        mean + stdDev *
        (Mathf.Sqrt(-2.0f * Mathf.Log(1.0f - UnityEngine.Random.value)) * Mathf.Sin(2.0f * Mathf.PI * (1.0f - UnityEngine.Random.value)));

    private IEnumerator AimingDelayCoroutine()
    {
        yield return new WaitForSeconds(aimingDelayMs / 1000);
        isAiming = true;
    }
}