using UnityEngine.SceneManagement;

namespace Ikkiuchi {
    public interface ISceneManagerWrapper {

        void LoadScene(SceneEnum nextScene);

        bool IsLoadedScene(SceneEnum scene);

        void LoadSceneAdditiveOnce(SceneEnum additiveScene);
    }

    public class SceneManagerWrapper : ISceneManagerWrapper {

        public void LoadScene(SceneEnum nextScene) {
            SceneManager.LoadScene(nextScene.ToString());
        }

        public void LoadSceneAdditiveOnce(SceneEnum additiveScene) {
            if (IsLoadedScene(additiveScene)) return;
            SceneManager.LoadScene(additiveScene.ToString(), LoadSceneMode.Additive);
        }

        public bool IsLoadedScene(SceneEnum scene) {
            //  HACK:   一度参照したシーンはキャッシュするようにする
            Scene target = SceneManager.GetSceneByName(scene.ToString());
            return target.isLoaded;
        }
    }
}
