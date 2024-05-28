using UnityEngine;

public class ShootButtonCreator : MonoBehaviour
{
    [SerializeField] private GameObject shootButtonPrefab;
    [SerializeField] private ShootButtonPlaceholder[] shootButtonPlaceholders;

    private void OnValidate()
    {
        shootButtonPlaceholders = GetComponentsInChildren<ShootButtonPlaceholder>();
    }

    private void Start()
    {
    }

    public void CreateButton()
    {
        var choice = Random.Range(0, shootButtonPlaceholders.Length);
        var buttonSpawnPoint = shootButtonPlaceholders[choice];
        Instantiate(shootButtonPrefab, buttonSpawnPoint.position, Quaternion.identity);
    }
}
