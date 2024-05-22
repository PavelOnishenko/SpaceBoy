using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private Sprite offSprite; 
    [SerializeField] private Sprite onSprite; 
    [SerializeField] private string loadingScene = "LoadingScene";
    [SerializeField] private string sceneAfterLoading = "CombatScene";

    private Image imageComponent;
    
    void Awake()
    {
        if (!TryGetComponent<Image>(out imageComponent))
            Debug.LogError("Image component not found on the PLayButton GameObject.");
        SceneOrder.Instance.SetNextScene(sceneAfterLoading);
    }
    
    public void Enter()
    {
        if (imageComponent != null)
            imageComponent.sprite = onSprite;
    }

    public void Exit()
    {
        if (imageComponent != null)
            imageComponent.sprite = offSprite;
    }

    public void Click() => SceneManager.LoadScene(loadingScene);
}