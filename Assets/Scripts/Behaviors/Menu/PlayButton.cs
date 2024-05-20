using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private Sprite offSprite; // Assign in the Inspector
    [SerializeField] private  Sprite onSprite; // Assign in the Inspector
    [SerializeField] private  string sceneToLoad = "LoadingScene"; // Specify the scene name

    private Image imageComponent;
    
    void Awake()
    {
        imageComponent = GetComponent<Image>();
        if (imageComponent == null)
        {
            Debug.LogError("Image component not found on the GameObject.");
        }
        SceneOrder.Instance.SetNextScene("SampleScene");
    }
    
    public void Enter()
    {
        Debug.Log("Mouse enter");
        if (imageComponent != null)
        {
            imageComponent.sprite = onSprite;
        }
    }

    public void Exit()
    {
        Debug.Log("Mouse exit");
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
