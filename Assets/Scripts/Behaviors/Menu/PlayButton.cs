using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private Sprite offSprite; 
    [SerializeField] private  Sprite onSprite; 
    [SerializeField] private  string sceneToLoad = "LoadingScene";

    private Image imageComponent;
    
    void Awake()
    {
        imageComponent = GetComponent<Image>();
        if (imageComponent == null)
        {
            Debug.LogError("Image component not found on the PLayButton GameObject.");
        }
        SceneOrder.Instance.SetNextScene("SampleScene");
    }
    
    public void Enter()
    {
        if (imageComponent != null)
        {
            imageComponent.sprite = onSprite;
        }
    }

    public void Exit()
    {
        if (imageComponent != null)
        {
            imageComponent.sprite = offSprite;
        }
    }

    public void Click()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
