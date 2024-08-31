using Assets.Scripts.EditorShared;
using System.Collections;
using UnityEngine;

public class ShootButtonCreator : MonoBehaviour, IDesignerConfigurable
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

    public void CreateButton()
    {
        var position = shootButtonPlaceholders[Random.Range(0, shootButtonPlaceholders.Length)].position;
        currentlyShownButton = Instantiate(shootButtonPrefab, position, Quaternion.identity);
    }

    public void DestroyButton() => Destroy(currentlyShownButton);

    IEnumerator ButtonRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(buttonAppearancePeriod);
            while(GameInfo.Instance.State is not GameState.Ongoing) yield return null;
            CreateButton();
        }
    }

    #region FOR EDITOR

    public void ApplyParameters()
    {
        var gameParameters = GameParametersManager.Instance.gameParameters;
        if (gameParameters != null) buttonAppearancePeriod = gameParameters.buttonAppearancePeriod;
    }

    #endregion
}