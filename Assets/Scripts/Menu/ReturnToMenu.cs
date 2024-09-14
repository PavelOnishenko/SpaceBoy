using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Menu
{
    internal class ReturnToMenu : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneOrder.Instance.SetNextScene(SceneNames.MenuScene);
                SceneManager.LoadScene(SceneNames.LoadingScene.ToString());
            }
        }
    }
}