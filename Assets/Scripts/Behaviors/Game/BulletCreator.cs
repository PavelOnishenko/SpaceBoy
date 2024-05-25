using UnityEngine;

public class BulletCreator : MonoBehaviour
{
    void Start()
    {
        
    }

    public void CreateBullet()
    {
        Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }
}
