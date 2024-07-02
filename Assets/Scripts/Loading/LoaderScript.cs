using Assets.Scripts.Edtitor;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour, IDesignerConfigurable
{
    [SerializeField] private Slider progressBar;
    [SerializeField] private Canvas loadingCanvas;
    [SerializeField] private float step = 0.002f;

    void Start()
    {
        progressBar.value = 0;
        loadingCanvas.gameObject.SetActive(true);
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        var nextSceneName = SceneOrder.Instance.GetNextSceneName();
        var asyncLoad = SceneManager.LoadSceneAsync(nextSceneName);
        asyncLoad.allowSceneActivation = false;

        while (true)
        {
            if (asyncLoad.progress - progressBar.value > step)
                progressBar.value += step;
            else
                progressBar.value = asyncLoad.progress;

            if (progressBar.value >= 0.9f && !asyncLoad.allowSceneActivation)
                asyncLoad.allowSceneActivation = true;

            yield return null;
        }
    }

    public void ApplyParameters()
    {
        var gameParameters = GameParametersManager.Instance.gameParameters;
        if (gameParameters != null) step = gameParameters.loadingStep;
    }
}