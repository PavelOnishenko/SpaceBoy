using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    [SerializeField] private Slider progressBar;
    [SerializeField] private Canvas loadingCanvas;
    [SerializeField] private float step = 0.01f;
    [SerializeField] private float updateDelay = 0.1f; // Update delay to control frequency of UI updates

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

        // Use WaitForSeconds to limit the update frequency and reduce overhead
        while (!asyncLoad.isDone)
        {
            // Smoothly update the progress bar value over time
            if (progressBar.value < asyncLoad.progress)
            {
                progressBar.value += step;
            }

            // Once the scene is almost loaded, activate it
            if (progressBar.value >= 0.9f && !asyncLoad.allowSceneActivation)
            {
                asyncLoad.allowSceneActivation = true;
            }

            // Add a small delay between updates to reduce the load on lower-powered devices
            yield return new WaitForSeconds(updateDelay);
        }
    }
}