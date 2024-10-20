using Assets.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private Sprite offSprite; 
    [SerializeField] private Sprite onSprite; 
    [SerializeField] private SceneNames sceneAfterLoading = SceneNames.CombatScene;
    [SerializeField] private float delayBeforeLoadingInSeconds = 0.5f;

    private Image imageComponent;
    
    void Awake()
    {
        if (!TryGetComponent<Image>(out imageComponent))
            Debug.LogError("Image component not found on the PlayButton game object.");
        SceneOrder.Instance.SetNextScene(sceneAfterLoading);
    }
    
    public void Enter()
    {
        if (imageComponent != null) imageComponent.sprite = onSprite;
    }

    public void Exit()
    {
        if (imageComponent != null) imageComponent.sprite = offSprite;
    }

    public void Click()
    {
        StartCoroutine(GoToNextSceneAfterDelay());
    }

    private IEnumerator GoToNextSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeLoadingInSeconds);
        SceneOrder.Instance.SetNextScene(SceneNames.CombatScene);
        SceneManager.LoadScene(SceneNames.LoadingScene.ToString());
    }
}