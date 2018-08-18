using UnityEngine.UI;
using UnityEngine;
using Zenject;
using Ikkiuchi.TitleScenes.ViewModels;
using UniRx;

namespace Ikkiuchi.TitleScenes.Views {

    [RequireComponent(typeof(Text))]
    public class RoomNameText : MonoBehaviour{

        [Inject] private CreateRoomModel model;

        private void Start() {
            this.ObserveEveryValueChanged(_ => model.RoomName)
                .Subscribe(name => {
                    GetComponent<Text>().text = model.RoomName;
                })
                .AddTo(this);
        }
    }
}
