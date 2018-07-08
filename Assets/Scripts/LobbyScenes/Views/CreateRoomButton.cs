using Ikkiuchi.LobbyScenes.Models;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Ikkiuchi.LobbyScenes.Views {
    [RequireComponent(typeof(Button))]
    public class CreateRoomButton : MonoBehaviour {

        public LobbySceneModel model;

        private Button button;

        private void Awake() {
            button = GetComponent<Button>();
        }

        private void Start() {
            model.IsNetworkConnected.Subscribe(_=>UpdateInteractable()).AddTo(this);
            model.RoomName.Subscribe(_ => UpdateInteractable()).AddTo(this);
        }

        private void UpdateInteractable() {
            if(model.IsNetworkConnected.Value && model.RoomName.Value.Length > 0) {
                button.interactable = true;
            }
            else {
                button.interactable = false;
            }
        }

    }
}
