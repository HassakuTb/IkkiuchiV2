using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ikkiuchi.TitleScenes {
    public class NetworkingSceneLoader : MonoBehaviour{

        private const string networkingSceneName = "Networking";

        private void Start() {
            Scene target = SceneManager.GetSceneByName(networkingSceneName);
            if (target.isLoaded) return;
            SceneManager.LoadScene(networkingSceneName, LoadSceneMode.Additive);
        }
    }
}
