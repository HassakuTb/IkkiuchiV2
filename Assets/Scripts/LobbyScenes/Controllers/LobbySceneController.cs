using UnityEngine;
using Ikkiuchi.LobbyScenes.Models;

namespace Ikkiuchi.LobbyScenes.Controllers {

    public class LobbySceneController : MonoBehaviour{

        public LobbySceneModel model;

        public void ChangeRoomName(string newName) {
            model.SetRoomName(newName);
        }
    }
}
