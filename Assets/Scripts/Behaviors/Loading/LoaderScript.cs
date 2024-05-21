using System;
using System.Collections;
using Unity.VisualScripting;
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
        var sceneNameToLoad = SceneOrder.Instance.GetNextScene();
        var asyncLoad = SceneManager.LoadSceneAsync(sceneNameToLoad);
        asyncLoad.allowSceneActivation = false;

        while (true)
        {
            if (asyncLoad.progress - progressBar.value > step)
                progressBar.value += step;
            else
                progressBar.value = asyncLoad.progress;
            Debug.Log(progressBar.value);

            if (progressBar.value >= 0.9f && !asyncLoad.allowSceneActivation)
                asyncLoad.allowSceneActivation = true;

            if (Math.Abs(progressBar.value - 1f) < 0.01f)
            {
                if(!loadingCanvas.IsDestroyed()) 
                    loadingCanvas.gameObject.SetActive(false);
                break;
            }

            Debug.Log("Retuning");
            yield return null;
            Debug.Log("AFTER");
        }
    }
}