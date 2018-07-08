using UnityEngine.SceneManagement;

namespace Ikkiuchi {
    public interface ISceneManagerWrapper {

        void LoadScene(SceneEnum nextScene);
    }

    public class SceneManagerWrapper : ISceneManagerWrapper {
        public void LoadScene(SceneEnum nextScene) {
            SceneManager.LoadScene(nextScene.ToString());
        }
    }
}
