using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    public void Exit()
    {
        SceneOrder.Instance.SetNextScene(SceneNames.MenuScene);
        SceneManager.LoadScene(SceneNames.LoadingScene.ToString());
    }
}