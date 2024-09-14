using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour
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
        var nextScene = SceneOrder.Instance.GetNextSceneName();
        var asyncLoad = SceneManager.LoadSceneAsync(nextScene.ToString());
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
}