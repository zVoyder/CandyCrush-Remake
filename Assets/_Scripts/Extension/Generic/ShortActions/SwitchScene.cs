namespace Extension.Generic.ShortActions
{
    using UnityEngine.SceneManagement;
    using UnityEngine;

    public class SwitchScene : MonoBehaviour
    {
        public void ChangeScene(string sceneToLoad)
        {
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }
    }
}