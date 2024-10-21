using System.Linq;
using UnityEngine;

public class BulletCreator : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private bool isProtagonist;

    public void CreateBullet()
    {
        var characterTransform = IntersceneState.GetCharacterDependentTransform(transform, isProtagonist);
        var spawnPoint = FindTransformInChildren(characterTransform, "BulletSpawnPoint");
        var spawnPointTranform = spawnPoint.transform;
        Instantiate(bulletPrefab, spawnPointTranform.position, spawnPointTranform.rotation);
    }

    public Transform FindTransformInChildren(Transform parent, string name)
    {
        if (parent.name == name)
            return parent;

        foreach (Transform child in parent)
        {
            Transform result = FindTransformInChildren(child, name);
            if (result != null)
                return result;
        }

        return null;
    }
}