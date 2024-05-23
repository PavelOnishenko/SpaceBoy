using System;
using System.Collections;
using UnityEngine;

public class AiBehaviour : MonoBehaviour
{
    [SerializeField] private float aimingDelayMs = 500;
    [SerializeField] private float epsilon = 0.5f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float accuracy;
    [SerializeField] private float lowerAimingBound;
    [SerializeField] private float upperAimingBound;
    [SerializeField] private Transform shootingForearm;

    private float? desiredRotation;
    private EnemyState state;

    public void GenerateRotationAndAim()
    {
        desiredRotation ??= GenerateDesiredRotation();
        state.StartAiming();
    }

    void Start() => state = gameObject.GetComponent<EnemyState>();

    void Update()
    {
        if (state.IsDead) return;
        
        if (!state.IsAiming) return;
        
        if (GameInfo.Instance.State != GameState.Ongoing) return;
        var currentRotation = shootingForearm.eulerAngles.z;
        if (state.IsAiming)
        {
            if (desiredRotation.HasValue && Math.Abs(currentRotation - desiredRotation.Value) < epsilon) 
                ProcessShooting();
        }
    }

    private float GenerateDesiredRotation()
    {
        var result = GenerateGaussianRandom((upperAimingBound - lowerAimingBound) / 2f, 100 / accuracy) + lowerAimingBound;
        if (result > upperAimingBound) result = upperAimingBound;
        else if (result < lowerAimingBound) result = lowerAimingBound;
        return result;
    }

    private void ProcessShooting()
    {
        desiredRotation = null;
        Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        StartCoroutine(AimingDelayCoroutine());
    }

    private static float GenerateGaussianRandom(float mean = 0f, float stdDev = 1f) =>
        mean + stdDev *
        (
            Mathf.Sqrt(-2.0f * Mathf.Log(1.0f - UnityEngine.Random.value)) * 
            Mathf.Sin(2.0f * Mathf.PI * (1.0f - UnityEngine.Random.value))
        );

    private IEnumerator AimingDelayCoroutine()
    {
        yield return new WaitForSeconds(aimingDelayMs / 1000);
        GenerateRotationAndAim();
    }
}