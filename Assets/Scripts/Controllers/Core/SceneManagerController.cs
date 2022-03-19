using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers.Core
{
    /// <summary>
    /// Controller for Scene Manager
    /// </summary>
    public class SceneManagerController
    {
        public enum SceneNames
        {
            Classroom, Logo
        }

        /// <summary>
        /// Calls a scene by name
        /// </summary>
        /// <param name="sceneName"> Valid scene name </param>
        public static void CallScene(SceneNames sceneName) => SceneManager.LoadScene(sceneName.ToString());

        /// <summary>
        /// Calls a scene asynchronously by name
        /// </summary>
        /// <param name="sceneName"> Valid scene name </param>
        public static AsyncOperation CallSceneAsync(SceneNames sceneName) => SceneManager.LoadSceneAsync(sceneName.ToString());

        /// <summary>
        /// Quits the application
        /// </summary>
        public static void QuitGame() => Application.Quit();
    }
}