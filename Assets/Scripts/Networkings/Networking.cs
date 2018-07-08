using UnityEngine;

namespace Ikkiuchi.Networkings {
    public class Networking : MonoBehaviour{

        IPhotonNetworkWrapper photon = new PhotonNetworkWrapper();

        private void Start() {
            DontDestroyOnLoad(gameObject);
            photon.ConnectUsingSettings();  //  photonに接続
        }

        private void OnJoinedLobby() {
            Debug.Log("joined to lobby");
        }
    }
}
