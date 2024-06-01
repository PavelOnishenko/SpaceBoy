using UnityEngine;

public class BulletCreator : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject bulletPrefab;

    public void CreateBullet() => 
        Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
}