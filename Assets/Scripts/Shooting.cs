using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;

    private GameObject bulletInstance;

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame) 
            bulletInstance = Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);
    }
}
