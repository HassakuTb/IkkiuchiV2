using UnityEngine;
using Zenject;

namespace Ikkiuchi.Networkings {
    public class Networking : MonoBehaviour{

        [Inject]
        private NetworkingModel model;

        private void Start() {
            DontDestroyOnLoad(gameObject);
            PhotonNetwork.ConnectUsingSettings(null);  //  photonに接続
        }

        private void OnConnectedToMaster() {
            Debug.Log("OnConnectedToMaster");
            PhotonNetwork.JoinLobby();
        }

        //  Auto接続なのでphotonに繋いだらLobbyに即時はいる
        private void OnJoinedLobby() {
            Debug.Log("OnJoinedLobby");
            model.IsServerConnected = true;
        }

        //  Auto接続なのでphotonに繋いだらLobbyに即時はいる
        private void OnFailedToConnectToPhoton(DisconnectCause cause) {
            Debug.LogWarning(cause.ToString());
            model.IsServerConnected = false;
        }

        //  photonとの接続が切れたとき
        private void OnDisconnectedFromPhoton() {
            Debug.Log("OnDisconnectedFromPhoton");
            model.IsServerConnected = false;
        }
    }
}
