using UniRx;
using UnityEngine.UI;
using UnityEngine;
using Ikkiuchi.LobbyScenes.Models;

namespace Ikkiuchi.LobbyScenes.Views {

    [RequireComponent(typeof(Text))]
    public class NetworkStatusText : MonoBehaviour {

        public LobbySceneModel model;

        private Text text;

        private void Awake() {
            text = GetComponent<Text>();
        }

        private void Start() {
            model.IsNetworkConnected.Subscribe(OnNetworkStatusChanged).AddTo(this);
        }

        private void OnNetworkStatusChanged(bool isConnected) {
            if (isConnected) {
                text.text = "接続済み";
            }
            else {
                text.text = "接続済みでない";
            }
        }
    }
}
