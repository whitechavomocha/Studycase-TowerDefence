using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilities
{
    public class RestartScene : MonoBehaviour
    {
        public static void RestartSceneFunc()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}