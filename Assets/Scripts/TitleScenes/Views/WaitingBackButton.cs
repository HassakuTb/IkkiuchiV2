using Ikkiuchi.TitleScenes.ViewModels;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ikkiuchi.TitleScenes.Views {

    [RequireComponent(typeof(Button))]
    public class WaitingBackButton : MonoBehaviour{

        [Inject]
        private WindowState windowState;

        private void Start() {
            GetComponent<Button>().onClick.AddListener(() => {
                PhotonNetwork.LeaveRoom();
                windowState.State = WindowStateEnum.BuildRoom;
            });
        }
    }
}
