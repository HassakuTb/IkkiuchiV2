using UnityEngine;

namespace Ikkiuchi.TitleScenes {
    class NetworkSceneLoader : MonoBehaviour{

        ISceneManagerWrapper sceneManager = new SceneManagerWrapper();

        private void Start() {
            sceneManager.LoadSceneAdditiveOnce(SceneEnum.NetworkingScene);
        }
    }
}
