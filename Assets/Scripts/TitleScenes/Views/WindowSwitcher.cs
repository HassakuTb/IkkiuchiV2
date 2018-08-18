using Ikkiuchi.TitleScenes.ViewModels;
using UnityEngine;
using Zenject;

namespace Ikkiuchi.TitleScenes.Views {
    public class WindowSwitcher : MonoBehaviour{

        public GameObject createRoomWindow;
        public GameObject joinRoomWindow;
        public GameObject waitingWindow;

        [Inject]
        private WindowState windowState;

        private void Update() {
            createRoomWindow.SetActive(windowState.State == WindowStateEnum.BuildRoom);
            joinRoomWindow.SetActive(windowState.State == WindowStateEnum.SelectRoom);
            waitingWindow.SetActive(windowState.State == WindowStateEnum.WaitJoin);
        }
    }
}
