using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    [SerializeField] private string sceneNameToLoad;
    [SerializeField] private Slider progressBar;
    [SerializeField] private Canvas loadingCanvas;
    [SerializeField] private float step = 0.001f;
    
    void Start() => StartCoroutine(LoadSceneAsync());

    IEnumerator LoadSceneAsync()
    {
        var asyncLoad = SceneManager.LoadSceneAsync(sceneNameToLoad);
        asyncLoad.allowSceneActivation = false;
        var progressBarIsFinished = false;
        while (!progressBarIsFinished)
        {
            if (asyncLoad.progress - progressBar.value > step) progressBar.value += step;
            else progressBar.value = asyncLoad.progress;
            if (progressBar.value >= 0.9f && !asyncLoad.allowSceneActivation) asyncLoad.allowSceneActivation = true;
            if (Math.Abs(progressBar.value - 1f) < 0.01f)
            {
                loadingCanvas.gameObject.SetActive(false);
                progressBarIsFinished = true;
            }
            
            yield return null;
        }
    }
}