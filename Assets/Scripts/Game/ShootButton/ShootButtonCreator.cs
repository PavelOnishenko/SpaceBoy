using System.Collections;
using UnityEngine;

public class ShootButtonCreator : MonoBehaviour
{
    [SerializeField] private GameObject shootButtonPrefab;
    [SerializeField] private ShootButtonPlaceholder[] shootButtonPlaceholders;
    [SerializeField] private float buttonAppearancePeriod = 3.0f;

    private IEnumerator buttoneRoutine;

    private void OnValidate()
    {
        shootButtonPlaceholders = GetComponentsInChildren<ShootButtonPlaceholder>();
    }

    private void Start()
    {
        buttoneRoutine = ButtonRoutine();
        StartCoroutine(buttoneRoutine);
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
            yield return new WaitForSeconds(buttonAppearancePeriod);
            CreateButton();
        }
    }

}
