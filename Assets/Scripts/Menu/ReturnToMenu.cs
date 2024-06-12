using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Menu
{
    internal class ReturnToMenu : MonoBehaviour
    {
        private string loadingSceneName = "LoadingScene";
        private string menuSceneName = "MenuScene";

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneOrder.Instance.SetNextScene(menuSceneName);
                SceneManager.LoadScene(loadingSceneName);
            }
        }
    }
}