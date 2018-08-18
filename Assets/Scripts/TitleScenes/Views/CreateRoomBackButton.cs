using Ikkiuchi.TitleScenes.ViewModels;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ikkiuchi.TitleScenes.Views {

    [RequireComponent(typeof(Button))]
    public class CreateRoomBackButton : MonoBehaviour{

        [Inject]
        private WindowState windowState;

        private void Start() {
            GetComponent<Button>().onClick.AddListener(() => {
                windowState.State = WindowStateEnum.None;
            });
        }
    }
}
