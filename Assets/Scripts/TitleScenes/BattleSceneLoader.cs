using Ikkiuchi.TitleScenes.ViewModels;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Ikkiuchi.TitleScenes {
    public class BattleSceneLoader : MonoBehaviour{

        [Inject] private WindowState windowState;

        public void OnJoinedRoom() {
            Debug.Log("OnJoined");
            if(PhotonNetwork.playerList.Length == 2) {
                windowState.State = WindowStateEnum.None;

                SceneManager.LoadScene("BattleScene");
            }
        }
    }
}
