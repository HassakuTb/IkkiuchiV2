using UniRx;
using UnityEngine;
using Ikkiuchi.Networkings;

namespace Ikkiuchi.LobbyScenes.Models {
    public class LobbySceneModel : MonoBehaviour{

        private Networking networking = null;
        private IReactiveProperty<string> roomName = new ReactiveProperty<string>("");

        private void Start() {
            networking = networking ?? GameObject.FindGameObjectWithTag("Networking").GetComponent<Networking>();
        }

        public IReadOnlyReactiveProperty<bool> IsNetworkConnected {
            get {
                return networking.IsConnected;
            }
        }

        public IReadOnlyReactiveProperty<string> RoomName {
            get {
                return roomName.ToReadOnlyReactiveProperty();
            }
        }

        public void SetRoomName(string roomName) {
            this.roomName.Value = roomName;
        }

    }
}
