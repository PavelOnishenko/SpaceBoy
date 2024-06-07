using System.Collections;
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
        StartCoroutine(ButtonRoutine());
    }

    public void CreateButton()
    {
        var choice = Random.Range(0, shootButtonPlaceholders.Length);
        var buttonSpawnPoint = shootButtonPlaceholders[choice];
        Instantiate(shootButtonPrefab, buttonSpawnPoint.position, Quaternion.identity);
    }

    IEnumerator ButtonRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(4f);
            CreateButton();
        }
    }

}
