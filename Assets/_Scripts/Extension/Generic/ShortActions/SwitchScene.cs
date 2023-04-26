namespace Extension.Generic.ShortActions
{
    using UnityEngine.SceneManagement;
    using UnityEngine;

    public class SwitchScene : MonoBehaviour
    {
        /// <summary>
        /// Switches to a scene.
        /// </summary>
        /// <param name="sceneToLoad">Scene to load in a string format.</param>
        public void ChangeScene(string sceneToLoad)
        {
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }

        /// <summary>
        /// Switches to a scene.
        /// </summary>
        /// <param name="sceneToLoad">Build index of the scene to load.</param>
        public void ChangeScene(int sceneToLoadBuildIndex)
        {
            SceneManager.LoadScene(sceneToLoadBuildIndex, LoadSceneMode.Single);
        }
    }
}