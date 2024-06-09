using System.Collections;
using UnityEngine;

public class ShootButtonCreator : MonoBehaviour
{
    [SerializeField] private GameObject shootButtonPrefab;
    [SerializeField] private ShootButtonPlaceholder[] shootButtonPlaceholders;
    [SerializeField] private float buttonAppearancePeriod = 3.0f;

    private IEnumerator buttonRoutine;
    private GameObject currentlyShownButton;

    private void OnValidate()
    {
        shootButtonPlaceholders = GetComponentsInChildren<ShootButtonPlaceholder>();
    }

    private void Start()
    {
        buttonRoutine = ButtonRoutine();
        StartCoroutine(buttonRoutine);
    }

    public void CreateButton() =>
        currentlyShownButton = Instantiate(shootButtonPrefab,
            shootButtonPlaceholders[Random.Range(0, shootButtonPlaceholders.Length)].position,
            Quaternion.identity);

    public void DestroyButton() => Destroy(currentlyShownButton);

    IEnumerator ButtonRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(buttonAppearancePeriod);
            while(GameInfo.Instance.State is not GameState.Ongoing)
            {
                yield return null;
            }
            CreateButton();
        }
    }
}
