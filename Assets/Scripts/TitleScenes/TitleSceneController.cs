using UnityEngine;

namespace Ikkiuchi.TitleScenes {



    public class TitleSceneController : MonoBehaviour {

        ISceneManagerWrapper sceneManaer = new SceneManagerWrapper();
        
        /// <summary>
        /// 部屋へ遷移する
        /// </summary>
        public void TransitionToRoomScene() {
            sceneManaer.LoadScene(SceneEnum.LobbyScene);
        }
    }
}
