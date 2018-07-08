using UnityEngine;
using UniRx;

namespace Ikkiuchi.Networkings {
    public class Networking : MonoBehaviour{

        IPhotonNetworkWrapper photon = new PhotonNetworkWrapper();
        
        private IReactiveProperty<bool> isConnected = new ReactiveProperty<bool>(false);

        /// <summary>
        /// ロビーに接続できているか
        /// </summary>
        public IReadOnlyReactiveProperty<bool> IsConnected {
            get {
                return isConnected.ToReadOnlyReactiveProperty();
            }
        }

        private void Start() {
            DontDestroyOnLoad(gameObject);
            photon.ConnectUsingSettings();  //  photonに接続
        }

        //  Auto接続なのでphotonに繋いだらLobbyに即時はいる
        private void OnJoinedLobby() {
            Debug.Log("OnJoinedLobby");
            isConnected.Value = true;
        }

        //  photonとの接続が切れたとき
        private void OnDisconnectedFromServer(NetworkDisconnection info) {
            Debug.Log("OnDisconnectedFromServer");
            isConnected.Value = false;
        }
    }
}
